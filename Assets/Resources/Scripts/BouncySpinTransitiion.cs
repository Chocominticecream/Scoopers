using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncySpinTransition : BaseTransition
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f,startPosY,startPosZ);

        // if(callAtStart)
        // {
        //     CoroutineTransition();
        // }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CoroutineTransition()
    {

        float x;
        float y;
        float z;

        if (reverse)
        {
           x = startPosX;
           y = startPosY;
           z = startPosZ;
        }
        else
        {
           x = endPosX;
           y = endPosY;
           z = endPosZ;
           
        }
        if (reverse)
        {
           LeanTween.cancel(gameObject);
           //trying to cancel the first tween if this is called
           LeanTween.rotate(gameObject, new Vector3(x,y,z), animTime/4).setEaseOutExpo();
        }
        else
        {
           LeanTween.cancel(gameObject);
           LeanTween.rotate(gameObject, new Vector3(x,y,z), animTime).setEaseOutElastic();
        }
       
    }
} 