using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamScript : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody2D kinetics;

    //state machine
    public enum STATE {idle, isHeld, isSticking, attemptToStick}
    //idle = ice cream is not on any surface or held
    //isHeld ice cream being held in mouse
    //isSticking ice cream is sticking on to a 
    public STATE state = STATE.idle;

    //other variables
    private float timer = 0f;
    private Vector2 minBounds;  // Bottom-left corner of the camera
    private Vector2 maxBounds;
    private Vector3 newPosition;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        ClampObjectWithinCameraBounds();

        switch(state)
        {
            case STATE.isHeld:
                kinetics.velocity = Vector2.zero;
                kinetics.angularVelocity = 0f;
                
                newPosition = MouseWorldPosition() + offset;

                kinetics.MovePosition(newPosition);
                break;
            case STATE.attemptToStick:
                kinetics.velocity *= 0.995f;
                kinetics.angularVelocity *= 0.995f;
                break;
            case STATE.isSticking:
                break;
            default:
                break;

        }
        
        //physics correction methods
        if (kinetics.velocity.magnitude < 0.01f && state != STATE.isSticking)
        {      
            kinetics.velocity = new Vector2(0.2f, 0f);
            kinetics.angularVelocity = 0.1f;
        }

        
    }

    void Awake()
    {
        kinetics = GetComponent<Rigidbody2D>();
    }

    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    void OnMouseDrag()
    {
        
    }

    void OnMouseDown()
    {
        if(state == STATE.idle)
        {
            print("held!");
            state = STATE.isHeld;
            offset = transform.position - MouseWorldPosition();
        }
        
    }

    void OnMouseUp()
    {
        if(state == STATE.isHeld)
        {
            state = STATE.idle;
            print("let go!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if touching object is ice cream that is sticking and if self is idle state
        if(collision.gameObject.GetComponent<IceCreamScript>() != null 
           && state == STATE.idle
           && collision.gameObject.GetComponent<IceCreamScript>().state == STATE.isSticking) 
        {
            state = STATE.attemptToStick;
            print("touching!");
        }
        //always stick successfully on the bowl object
        else if(collision.gameObject.GetComponent<Bowl>() != null  && state == STATE.idle)
        {
            kinetics.isKinematic = true;
            kinetics.velocity = Vector2.zero;
            kinetics.angularVelocity = 0f;
            state = STATE.isSticking;
        }
    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IceCreamScript>() != null)
        {
            print("left from touching!");
        }
    }

    private void ClampObjectWithinCameraBounds()
    {
        // Get the camera bounds in world space
        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));  // Bottom-left corner
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));  // Top-right corner

        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Clamp the position within the camera bounds
        currentPosition.x = Mathf.Clamp(currentPosition.x, minBounds.x, maxBounds.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minBounds.y, maxBounds.y);

        // Check if position needs adjustment
        if (currentPosition != transform.position)
        {
            // Use Rigidbody2D.MovePosition to move the object while respecting physics
            kinetics.MovePosition(currentPosition);
        }
    }
}
