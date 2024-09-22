using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text heightcomponent;
    private Singleton singleton;

    private void Awake()
    {
        singleton = Singleton.Instance;
        if(singleton.heightHandler < 0)
        {
            singleton.heightHandler = 0;
        }
        heightcomponent.text = "Height: " + singleton.heightHandler + "m";
        singleton.money += singleton.heightHandler;
    }
}
