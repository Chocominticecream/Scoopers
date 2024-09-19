using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeightLineCollisionScript : MonoBehaviour
{
    private float movementval = 3f;
    private Collider2D heightCollision;
    private IceCreamScript iceCreaminCollision;
    private bool isInCollision = false;
    public UnityEvent moveCamera;
    public UnityEvent moveLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if( iceCreaminCollision != null &&
        //     iceCreaminCollision.state == IceCreamScript.STATE.attemptToStick)
        // {
        //     isInCollision = true;
        // }
        // StartCoroutine(OnHeightSuccess());
        
    }
    
    //interestingly all update() scripts stop if awake has a runtime error
    void Awake()
    {
        //heightCollision = transform.Find("HeightLine").GetComponent<Collider2D>();
        MoveCamera moveCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<MoveCamera>();
        moveCamera.AddListener(() => moveCameraScript.ShiftCamera(movementval));
        HeightLineScript heightline = GameObject.FindWithTag("HeightLine").GetComponent<HeightLineScript>();
        moveLine.AddListener(() => heightline.ShiftLine(movementval));
        heightCollision = GetComponent<Collider2D>();
    }

    public void checkHeight(float icecreamheight)
    {
        print(icecreamheight);
        while(icecreamheight >= movementval)
        {
            StartCoroutine(OnHeightSuccess());
        }

    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.GetComponent<IceCreamScript>() != null)
    //     {
    //         if(iceCreaminCollision == null &&
    //            collision.gameObject.GetComponent<IceCreamScript>().state == IceCreamScript.STATE.attemptToStick)
    //         {
    //             iceCreaminCollision = collision.gameObject.GetComponent<IceCreamScript>();
    //             print("an ice cream eligible is in contact!");
    //         }
            
    //         print("an ice cream is in contact!");
    //     }

        
        
    // }

    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.GetComponent<IceCreamScript>() != null)
    //     {
    //         if( iceCreaminCollision != null &&
    //             iceCreaminCollision.state == IceCreamScript.STATE.attemptToStick)
    //         {
    //             isInCollision = false;
    //             iceCreaminCollision = null;
    //             print("an ice cream ineligible left collision!");
    //         }
            
    //         print("ice cream has left collision!");
            
    //     }
    // }

    private IEnumerator OnHeightSuccess()
    {
        // if(isInCollision && iceCreaminCollision != null)
        // {
        //     if(iceCreaminCollision.state == IceCreamScript.STATE.isSticking)
        //     {
                iceCreaminCollision = null;
                isInCollision = false;
                heightCollision.enabled = false;
                print("reached " + movementval+2f + " meters!");
                moveCamera.Invoke();
                movementval += 4f;
                moveLine.Invoke();
                yield return new WaitForSeconds(4f);
                heightCollision.enabled = true;
        //     }

        // }
    }
}
