using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public GameObject audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        print("playing music!");
        Instantiate(audioPlayer).GetComponent<AudioController>().playTrackone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
