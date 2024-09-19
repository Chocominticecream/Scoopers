using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedIceCreamsScript : MonoBehaviour
{
    public GameObject iceCreamUIobj;
    private Singleton singleton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        singleton = Singleton.Instance;
        iterateAndSetIceCreams();
    }

    public void iterateAndSetIceCreams()
    {
        print("instantiating children in shop");
        for (int i = 0; i < 3; i++)
        {
            IceCreamScriptable iceCreamData = singleton.iceCreamsUsed[i];
            GameObject child = transform.GetChild(i).gameObject;
            GameObject instantiatedUI = Instantiate(iceCreamUIobj, child.transform);
            
            //grab the image info 
            IceCreamUI codecomponent = instantiatedUI.GetComponent<IceCreamUI>();
            codecomponent.iceCreamObj = iceCreamData;
            codecomponent.state = IceCreamUI.STATE.equipped;

            // Sprite newsprite = Sprite.Create(iceCreamData.iceCreamGraphic, new Rect(0, 0, iceCreamData.iceCreamGraphic.width, iceCreamData.iceCreamGraphic.height), new Vector2(0.5f, 0.5f));
            // instantiatedUI.GetComponent<Image>().sprite = newsprite;

            // IceCreamUI codecomponent = instantiatedUI.GetComponent<IceCreamUI>();
            // codecomponent.state = IceCreamUI.STATE.equipped;
            // codecomponent.name = iceCreamData.name;
        }
    }
}
