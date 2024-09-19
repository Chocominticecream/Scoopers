using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeightLineScript : MonoBehaviour
{
    [SerializeField] private TMP_Text heightDisplayComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftLine(float movementval)
    {
        LeanTween.move(gameObject, new Vector3(transform.position.x,movementval,transform.position.z), 4f).setEaseOutExpo();
        float newmovementval = movementval+2f;
        heightDisplayComponent.text = "Height Line: " + newmovementval + "m";
    }
}

