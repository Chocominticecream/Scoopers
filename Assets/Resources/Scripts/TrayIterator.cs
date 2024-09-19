using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayIterator : MonoBehaviour
{
    private Singleton singleton;
    private int currentTray = 0;
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
        loadIceCreamdata();
    }

    public void loadIceCreamdata()
    {
        singleton.LoadStarterIceCreams();
        print("loading data..");
        for (int i = 0; i < 3; i++)
        {
            // Access each child by index
            GameObject child = transform.GetChild(i).gameObject;
            
            //set the values for each ice cream tray
            child.GetComponent<IceCreamTrayScript>().iceCreamData = singleton.iceCreamsUsed[i];
            print(singleton.iceCreamsUsed[i]);
            
        }
    }

    public void iterateTrays()
    {
        currentTray += 1;
        if(currentTray > 2)
        {
            currentTray = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            IceCreamTrayScript child = transform.GetChild(i).gameObject.GetComponent<IceCreamTrayScript>();
            if(i == currentTray)
            {
                child.SwitchTrays(true);
            }
            else
            {
                child.SwitchTrays(false);

            }
        
        }
        

    }
}
