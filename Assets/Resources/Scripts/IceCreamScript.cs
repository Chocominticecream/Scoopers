using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private float movementheight = 0f;
    private bool bowl = false;
    private bool exitFactor = false; //a hacky method that significantly increases sticking chance when leaving collision
    private bool plopPlayed = false;

    private Vector2 minBounds;  // Bottom-left corner of the camera
    private Vector2 maxBounds;
    private Vector3 newPosition;
    public CircleCollider2D circlecollider;
    public Collider2D collider;
    public GameObject darkeninglayer;
    public GameObject audioPlayer;
    public GameObject snowflake;

    public UnityEvent shiftOnheightreached;
    public UnityEvent swapTrays;
    
    //ice cream vars
    public int scoopability;
    public int reward;
    public int stickiness;

    private IceCreamScriptable _iceCreamObj;

    public IceCreamScriptable iceCreamObj
    {
        get {return _iceCreamObj;}
        set
        {
            _iceCreamObj = value;
            name = _iceCreamObj.name;
            reward = _iceCreamObj.value;
            scoopability = _iceCreamObj.scoopability;
            stickiness = _iceCreamObj.stickiness;
            Sprite newsprite = Sprite.Create(_iceCreamObj.iceCreamGraphic, new Rect(0, 0, _iceCreamObj.iceCreamGraphic.width, _iceCreamObj.iceCreamGraphic.height), new Vector2(0.5f, 0.5f));
            GetComponent<SpriteRenderer>().sprite = newsprite;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {   

        // to be disabled if isSticking or attempttostick
        if(state == STATE.isHeld)
        {
            ClampObjectWithinCameraBounds();
        }
        
       
        switch(state)
        {
            case STATE.isHeld:
                kinetics.velocity = Vector2.zero;
                kinetics.angularVelocity = 0f;
                newPosition = MouseWorldPosition() + offset;
                kinetics.MovePosition(newPosition);
                collider.enabled = false;
                break;
            case STATE.attemptToStick:
                
                //push the object a little bit if not sticking, prevents situations where
                //the ice cream stays completely stationary when it doesn't make sense to
                if (kinetics.velocity.magnitude < 0.01f)
                {      
                    kinetics.velocity = new Vector2(0.2f, 0f);
                    kinetics.angularVelocity = 0.1f;
                }
                kinetics.velocity *= 0.997f;
                kinetics.angularVelocity *= 0.997f;

                timer += Time.deltaTime;
                if(timer >= 1f)
                {

                    //roll a number between 1-20 if less than 3/ stickiness value, stick
                    int rand = Random.Range(1,25);
                    if(exitFactor)
                    {
                        rand = (int)(rand*1.5);
                        exitFactor = false;
                    }

                    if(rand < 4 +(int)((stickiness-1)*2) || bowl)
                    {
                        Darken();
                        print("sticking!");
                        movementheight = CalculatePoint(true);
                        state = STATE.isSticking;
                        Instantiate(audioPlayer).GetComponent<AudioController>().pickPuzzle();
                        shiftOnheightreached.Invoke();
                    }
                    else
                    {
                        print("not sticking!");
                    }

                    
                 
                    timer = 0f;
                }
                
                //small jolt timer to recaclulate if the ice cream is idle for too long
                // // for testing
                // state = STATE.isSticking;
                break;
            case STATE.isSticking:
                kinetics.isKinematic = true;
                kinetics.velocity = Vector2.zero;
                kinetics.angularVelocity = 0f;
                break;
            default:
                break;

        }
        
    }

    private void Awake()
    {
        kinetics = GetComponent<Rigidbody2D>();
        circlecollider = GetComponent<CircleCollider2D>();
        collider =  GetComponent<Collider2D>();
        HeightLineCollisionScript heightline = GameObject.FindWithTag("HeightLineCollision").GetComponent<HeightLineCollisionScript>();
        shiftOnheightreached.AddListener(() => heightline.checkHeight(movementheight));

        TrayIterator trayIterator = GameObject.FindWithTag("TrayIterator").GetComponent<TrayIterator>();
        swapTrays.AddListener(() => trayIterator.iterateTrays());
    }

    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseDrag()
    {
        
    }

    private void OnMouseDown()
    {
        if(state == STATE.idle)
        {

            state = STATE.isHeld;
            offset = transform.position - MouseWorldPosition();
        }
        
    }

    public void OnMouseUp()
    {
        if(state == STATE.isHeld)
        {
            state = STATE.idle;
            collider.enabled = true;

            
            //hacky technique to make the object jump an extremely small amount
            //so that it checks collisions properly if its let go when in contact 
            //with another collision
            Vector2 jumpForce = new Vector2(0f, 0.5f);
            kinetics.AddForce(jumpForce, ForceMode2D.Impulse);
            swapTrays.Invoke();
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!plopPlayed && collision.gameObject.GetComponent<Ground>() == null)
        {
            Instantiate(audioPlayer).GetComponent<AudioController>().PlopIceCream();
            Instantiate(snowflake, new Vector3(transform.position.x, CalculatePoint(false), transform.position.z), Quaternion.identity);
            plopPlayed = true;
        }
        //im not yandere dev i just coded tis in 2 days pls dont @ me
        //check if touching object is ice cream that is sticking and if self is idle state
        if(collision.gameObject.GetComponent<IceCreamScript>() != null 
           && (collision.gameObject.GetComponent<IceCreamScript>().state == STATE.isSticking || collision.gameObject.GetComponent<IceCreamScript>().state == STATE.attemptToStick)
           && state == STATE.idle)
        //    && (collision.gameObject.GetComponent<IceCreamScript>().state == STATE.isSticking || collision.gameObject.GetComponent<IceCreamScript>().state == STATE.attemptToStick)) 
        {
            state = STATE.attemptToStick;

        }
        //always stick successfully on the bowl object
        else if(collision.gameObject.GetComponent<Bowl>() != null  && state == STATE.idle)
        {
            bowl = true;
            state = STATE.attemptToStick;
        }
        else if (collision.gameObject.GetComponent<Ground>() != null  && state != STATE.isHeld)
        {
            DestroyIceCream();
        }

        if(collision.gameObject.GetComponent<IceCreamScript>() != null
           && collision.gameObject.GetComponent<IceCreamScript>().state == STATE.attemptToStick
           && state != STATE.isSticking)
        {
            state = STATE.attemptToStick;
        }
    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IceCreamScript>() != null 
           && state == STATE.attemptToStick
           && collision.gameObject.GetComponent<IceCreamScript>().state == STATE.isSticking)
        {
            state = STATE.idle;
            // Vector2 jumpForce = new Vector2(0f, 0.5f);
            // kinetics.AddForce(jumpForce, ForceMode2D.Impulse); 
            //this boolean here forces the ice cream to stick faster when exiting
            //this is so that if it gets stuck between two ice creams it will stick faster to them
            // hacky solution
            exitFactor =  true;
            print("exitfactor detected!");
            //set timer to 1f so that it sticks faster when in contact with two collisions
            //since that usually means that the ice cream is in a stable spot
            //so might as well stick quicker as well as to prevent delayed sticking
            //when the ice cream constantly exits and enters collisions

            timer += 1f;

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
        //currentPosition.y = Mathf.Clamp(currentPosition.y, minBounds.y, maxBounds.y);

        // Check if position needs adjustment
        if (currentPosition != transform.position)
        {
            // Use Rigidbody2D.MovePosition to move the object while respecting physics
            kinetics.MovePosition(currentPosition);
        }
    }

    private float CalculatePoint(bool highest)
    {
        //get center coords 
        Vector2 center = circlecollider.bounds.center;

        //get radius, taking scale in account
        float radius = circlecollider.radius * transform.lossyScale.y;

        print(transform.lossyScale.y);
        
        float highestPoint = 0f;
        if(highest)
        {
            highestPoint = center.y + radius;
        }
        else
        {
            highestPoint = center.y - radius;
        }
        

        print("highest point: " + highestPoint);

        return highestPoint;

    }
    

    private void DarkenColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color;
            Color darkenedColor = currentColor * 0.9f;
            spriteRenderer.color = darkenedColor;
        }
    }

    private void Darken()
    {
        darkeninglayer.SetActive(true);
    }

    public void DestroyIceCream()
    {
        Instantiate(snowflake, transform.position, Quaternion.identity);
        Instantiate(audioPlayer).GetComponent<AudioController>().DestroyIceCream();
        Destroy(gameObject);
    }

}
