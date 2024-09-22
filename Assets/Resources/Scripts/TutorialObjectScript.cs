using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialObjectScript : MonoBehaviour
{
    private Singleton singleton;
    [SerializeField ]private TMP_Text scoopcomponent;
    [SerializeField ]private TMP_Text dropcomponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        singleton = Singleton.Instance;
        if(singleton.tutorialPlayed)
        {
            scoopcomponent.gameObject.SetActive(false);
            dropcomponent.gameObject.SetActive(false);

        }

    }

    public void OnScoopActivate()
    {
        if(!singleton.tutorialPlayed)
        {
            scoopcomponent.gameObject.SetActive(false);
            dropcomponent.gameObject.SetActive(true);
        }
        
    }

    public void OnDropActivate()
    {
        if(!singleton.tutorialPlayed)
        {
            dropcomponent.gameObject.SetActive(false);
            singleton.tutorialPlayed = true;
        }
    }
}
