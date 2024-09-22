using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyAllNonSticking()
    {
        print("destroying");
        foreach(Transform child in transform)
        {
            if(child.gameObject.GetComponent<IceCreamScript>().state != IceCreamScript.STATE.isSticking)
            {
                child.gameObject.GetComponent<IceCreamScript>().DestroyIceCream();
            }
        }
    }
}
