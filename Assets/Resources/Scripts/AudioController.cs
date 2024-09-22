using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource src;
    public AudioSource music;
    public AudioClip hoverclip, pickedPuzzle, slottedPuzzle, buttonPress, iceCreamStick, iceCreamPlop, destroyIceCream,
    trackOne;
    private bool playmusic = false;

    private Singleton soundEffectController = Singleton.Instance;

    private void Start()
    {
        
    }
    
    private void Update()
    {
        if(src.isPlaying == false && !playmusic)
        {
            Destroy(transform.gameObject);
        }
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        src = GetComponent<AudioSource>();
        music = GetComponent<AudioSource>();

        music.volume = 1f;
    }

    public void PlayMusic()
    {
        
    }

    public void StopMusic()
    {
        
    }

    public void playHover()
    {
       src.PlayOneShot(hoverclip);
    }
    
    public void pickPuzzle()
    {
       src.PlayOneShot(pickedPuzzle);
    }

    public void slotPuzzle()
    {
       src.PlayOneShot(slottedPuzzle);
    }

    public void PressButton()
    {
       src.PlayOneShot(buttonPress);
    }

    public void StickIceCream()
    {
        src.PlayOneShot(iceCreamStick);
    }

    public void PlopIceCream()
    {
        src.PlayOneShot(iceCreamPlop);
    }

    public void DestroyIceCream()
    {
      src.PlayOneShot(destroyIceCream);
    }

    public void playTrackone()
    {
       music.clip = trackOne;
       playmusic = true;
       music.loop = true; 
       music.Play();
    }

}
