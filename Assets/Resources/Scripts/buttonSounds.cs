using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSounds : MonoBehaviour
{
    public GameObject audioPlayer;

    public void HoverOver()
    {
        Instantiate(audioPlayer).GetComponent<AudioController>().playHover();
    }

    public void buttonClicked()
    {
        Instantiate(audioPlayer).GetComponent<AudioController>().playHover();
    }

    public void playmusic()
    {
        Instantiate(audioPlayer).GetComponent<AudioController>().playTrackone();
    }

    public void buttonPress()
    {
        Instantiate(audioPlayer).GetComponent<AudioController>().PressButton();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
