using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamTrayScript : MonoBehaviour
{
    public GameObject iceCream;
    public GameObject spawnpoint;
    public GameObject spawnpointpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnScoop()
    {
        GameObject scoop = Instantiate(iceCream, spawnpoint.transform);
        iceCream.transform.position = spawnpointpos.transform.position;
        iceCream.transform.position = new Vector3(iceCream.transform.position.x, iceCream.transform.position.y, -11f);
        IceCreamScript icecreamComponent = iceCream.GetComponent<IceCreamScript>();
        icecreamComponent.state = IceCreamScript.STATE.isHeld;

        print("scooping!");

    }

    public void EndScoop()
    {

        print("scooping ended!");
    }
}
