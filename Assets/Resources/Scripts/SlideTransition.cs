using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTransition : BaseTransition
{
    // Start is called before the first frame update
    void Start()
    {
        
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
           LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(x,y,z), animTime).setEaseOutExpo();
        }
        else
        {
           LeanTween.cancel(gameObject);
           LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(x,y,z), animTime).setEaseOutExpo();
        }
       
    }
}
