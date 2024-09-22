using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public string test = "test!";
    public static Singleton Instance = new Singleton();

    
    public IceCreamScriptable[] allIceCreams;
    
    //temp variables
    public int scoreHandler = 0;
    public int heightHandler = 0;
    
    //variables to be saved
    public int timer = 30;
    public int money = 5;
    public int timesTimerBought = 0;
    public bool startersLoaded = false;
    public List<string> unlocked = new List<string> {"Chocolate", "Strawberry", "Vanilla"};
    public IceCreamScriptable[] iceCreamsUsed = new IceCreamScriptable[3];
    public bool tutorialPlayed = false;


    public void Shuffle<T>(IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void Awake() 
    { 
    // If there is an instance, and it's not me, delete myself.

       if (Instance != null && Instance != this) 
       { 
           Destroy(this); 
       } 
       else 
       { 
           Instance = this; 
           DontDestroyOnLoad(this.gameObject); 
           LoadStarterIceCreams();
           
       } 

    }
    
    //also loads all icecream objects for reference
    public void LoadStarterIceCreams()
    {
        //by right there should be a more modular way to do this but it may make things too complicated and require more time
        //so i will settle for this
        scoreHandler = 0;
        heightHandler = 0;

        if(!startersLoaded)
        {
            print("loading starters..");
            IceCreamScriptable chocolate = Resources.Load<IceCreamScriptable>("IceCreamScriptables/002-Chocolate");
            IceCreamScriptable strawberry = Resources.Load<IceCreamScriptable>("IceCreamScriptables/001-Strawberry");
            IceCreamScriptable vanilla = Resources.Load<IceCreamScriptable>("IceCreamScriptables/003-Vanilla");

            iceCreamsUsed[0] = strawberry;
            iceCreamsUsed[1] = chocolate;
            iceCreamsUsed[2] = vanilla;
            startersLoaded = true;
            allIceCreams = Resources.LoadAll<IceCreamScriptable>("IceCreamScriptables/");
        }
        
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
