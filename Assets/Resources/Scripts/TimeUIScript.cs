using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TimeUIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text timeDisplayComponent;
    [SerializeField] private GameObject timerEndTransitionobj;
    [SerializeField] private MenuUnityEvent endingTrans = new MenuUnityEvent();
    [SerializeField] private UnityEvent blockPanel;

    private Singleton singleton;
    private float timeCounter = 0f;
    private bool pauseTimer = false;

    private int _time;

    public int time
    {
        get{return _time;}
        set
        {
            _time = value; 
            timeDisplayComponent.text = "Time: " + _time;
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!pauseTimer)
        {
            timeCounter += Time.deltaTime;
        }
        
        if(timeCounter >= 1f)
        {
            time -= 1;
            timeCounter = 0f;
        }

        if(time <= 0)
        {
            timeCounter = 0f;
            if(!pauseTimer)
            {
                endingTrans.Invoke(timerEndTransitionobj);
                blockPanel.Invoke();
            }
            pauseTimer = true;
            
        }
        
    }

    void Awake()
    {
        singleton = Singleton.Instance;
        time = singleton.timer;
        UIController findUI = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        endingTrans.AddListener(findUI.CallUIObject);
    }



}
