using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blockInput(bool block)
    {
        if(block)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
             GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        

    }
}
