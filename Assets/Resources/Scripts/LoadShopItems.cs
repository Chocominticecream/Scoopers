using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadShopItems : MonoBehaviour
{
    public GameObject shopSlots;
    public GameObject iceCreamUIobj;
    private Singleton singleton;
    private int currentScroll = 0;
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
        setShopItems();
        
    }

    private void setShopItems()
    {
        foreach(IceCreamScriptable iceCreamData in singleton.allIceCreams)
        {
            GameObject instantiatedUI = Instantiate(iceCreamUIobj, shopSlots.transform);
            
            IceCreamUI codecomponent = instantiatedUI.GetComponent<IceCreamUI>();
            codecomponent.iceCreamObj = iceCreamData;
            codecomponent.state = IceCreamUI.STATE.lockedInShop;
            // Sprite newsprite = Sprite.Create(iceCreamData.iceCreamGraphic, new Rect(0, 0, iceCreamData.iceCreamGraphic.width, iceCreamData.iceCreamGraphic.height), new Vector2(0.5f, 0.5f));
            // instantiatedUI.GetComponent<Image>().sprite = newsprite;
            // IceCreamUI codecomponent = instantiatedUI.GetComponent<IceCreamUI>();
            // codecomponent.name = iceCreamData.name;
            print("setting :" + codecomponent.name);

        }
        
        
         foreach(Transform iceCreamUI in shopSlots.transform)
        {
            IceCreamUI icecreamComponent = iceCreamUI.gameObject.GetComponent<IceCreamUI>();

            //unlock all locked items
            foreach(string unlockedItem in singleton.unlocked)
            {
                if(icecreamComponent.name == unlockedItem)
                {
                    icecreamComponent.state = IceCreamUI.STATE.inShop;
                }
                
            }
            
            //set all equipped items
            foreach(IceCreamScriptable iceCreamData in singleton.iceCreamsUsed)
            {
                //explicitly set equipped items in the shop
                if(icecreamComponent.name == iceCreamData.name)
                {
                    print("set equipped!");
                    icecreamComponent.state = IceCreamUI.STATE.equippedInShop;
                }

            }
            
        }
    }

    public void ScrollShop(bool rightScroll)
    {
        float scrollValue = 850 / 6;
        int scrollFactor = shopSlots.transform.childCount - 6;
        if(rightScroll)
        {
            if(currentScroll < scrollFactor)
            {
                currentScroll += 1;
            }
        }
        else
        {
            if(currentScroll > 0)
            {
                currentScroll -= 1;
            }
            
        }

        RectTransform recttransformitems = shopSlots.GetComponent<RectTransform>();
        
        LeanTween.move(shopSlots.GetComponent<RectTransform>(), new Vector3((-350f + -(currentScroll*scrollValue)),recttransformitems.anchoredPosition.y), 0.6f).setEaseOutExpo();


    }
}
