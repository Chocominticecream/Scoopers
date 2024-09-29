using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public GameObject shopSlots;
    public GameObject selectedUIObject;
    public GameObject iceCreamUIobj;
    [SerializeField ]private TMP_Text spawnText;
    
    //shop interface items
    [SerializeField ]private TMP_Text timertextcomponent;
    [SerializeField ]private TMP_Text moneytextcomponent;
    [SerializeField ]private TMP_Text timershoptextcomponent;

    //shop description interface items
    [SerializeField ]private TMP_Text iceCreamNameText;
    [SerializeField ]private TMP_Text iceCreamCostText;
    [SerializeField ]private TMP_Text iceCreamScoopabilityText;
    [SerializeField ]private TMP_Text iceCreamStickinessText;
    [SerializeField ]private TMP_Text iceCreamUnlockedText;
    [SerializeField ]private GameObject buyItemButton;

    private Singleton singleton;

    private int _shoptimer;

    public int shoptimer
    {
        get{return _shoptimer;}
        set
        {
            _shoptimer = value;
            timertextcomponent.text = "Timer: " + _shoptimer;

        }
    }

    private int _money;

    public int money
    {
        get{return _money;}
        set
        {
            _money = value;
            moneytextcomponent.text = "Money: " + _money;

        }
    }
    private int _timesTimerBought = 0;
    private int timerCost = 0;
    public int timesTimerBought
    {
        get{return _timesTimerBought;}
        set
        {
            _timesTimerBought = value;
            int costValue = 5 + (timesTimerBought * 5);
            timerCost = costValue;
            timershoptextcomponent.text = "Buy 10 Timer Seconds<br>Cost: " + timerCost;

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
    
    void Awake()
    {
        singleton = Singleton.Instance;
        money = singleton.money;
        shoptimer = singleton.timer;
        timesTimerBought = singleton.timesTimerBought;
    }

    public void SelectItem(GameObject selectedUI)
    {
        IceCreamUI selectedUIcomponent = selectedUI.GetComponent<IceCreamUI>();

        //first disable selectedUI slot
        foreach(Transform iceCreamUI in shopSlots.transform)
        {
            IceCreamUI iceCreamuiComponent = iceCreamUI.gameObject.GetComponent<IceCreamUI>();

            if(iceCreamuiComponent.selected)
            {
                iceCreamuiComponent.selected = false;
            }
                
        }
        
        //then enable sleected UI
        foreach(Transform iceCreamUI in shopSlots.transform)
        {
            IceCreamUI iceCreamuiComponent = iceCreamUI.gameObject.GetComponent<IceCreamUI>();

            if(selectedUIcomponent.name == iceCreamUI.gameObject.GetComponent<IceCreamUI>().name)
            {
                    iceCreamuiComponent.selected = true;
            }
                
        }
        //set held object to current UI
        selectedUIObject = selectedUI;
        print(selectedUI.GetComponent<IceCreamUI>().name);
        SetShopDescription(selectedUIObject);

    }

    public void ReplaceEquipped(string equippedObjName, Transform equippedSlot)
    {
        int slotIndex = equippedSlot.GetSiblingIndex();
        
        if(selectedUIObject != null && selectedUIObject.GetComponent<IceCreamUI>().state != IceCreamUI.STATE.lockedInShop
           && selectedUIObject.GetComponent<IceCreamUI>().state != IceCreamUI.STATE.equippedInShop)
        {
            
            foreach(Transform iceCreamUI in shopSlots.transform)
            {
                //unequip the equipped object
                IceCreamUI iceCreamuiComponent = iceCreamUI.gameObject.GetComponent<IceCreamUI>();
                if(iceCreamuiComponent.name == equippedObjName)
                {
                    iceCreamuiComponent.state = IceCreamUI.STATE.inShop;
                }
                
                //equip the equipped object
                if(iceCreamuiComponent.name == selectedUIObject.GetComponent<IceCreamUI>().iceCreamObj.name)
                {
                    iceCreamuiComponent.state = IceCreamUI.STATE.equippedInShop;
                }
        
            }
            
            //destroy the equipped object in the equip slot
            foreach (Transform child in equippedSlot)
            {
                Destroy(child.gameObject);
            }
            
            //instantiate the new object and set the state
            GameObject instantiatedUI = Instantiate(iceCreamUIobj, equippedSlot);
            instantiatedUI.GetComponent<IceCreamUI>().iceCreamObj = selectedUIObject.GetComponent<IceCreamUI>().iceCreamObj;
            instantiatedUI.GetComponent<IceCreamUI>().state = IceCreamUI.STATE.equipped;

            //set the ice cream scriptable object
            singleton.iceCreamsUsed[slotIndex] = selectedUIObject.GetComponent<IceCreamUI>().iceCreamObj;

            //free the selecteduiobject
            SetShopDescription(selectedUIObject);
            selectedUIObject = null;
            
        }
        

    }

    private void SetShopDescription(GameObject UIItem)
    {
        IceCreamScriptable UIItemScriptable = UIItem.GetComponent<IceCreamUI>().iceCreamObj;
        IceCreamUI UIItemComponent = UIItem.GetComponent<IceCreamUI>();

        iceCreamNameText.text = UIItemScriptable.name;
        iceCreamCostText.text = "Cost: " + UIItemScriptable.cost;
        iceCreamScoopabilityText.text = "Scoopability: " + UIItemScriptable.scoopability;
        iceCreamStickinessText.text = "Stickiness: " +  UIItemScriptable.stickiness;

        if(UIItemComponent.state == IceCreamUI.STATE.lockedInShop)
        {
            iceCreamUnlockedText.text = "Locked";
            buyItemButton.SetActive(true);
        }
        else if(UIItemComponent.state == IceCreamUI.STATE.inShop)
        {
            iceCreamUnlockedText.text = "Unlocked!";
            buyItemButton.SetActive(false);
        }
        else if(UIItemComponent.state == IceCreamUI.STATE.equippedInShop)
        {
            iceCreamUnlockedText.text = "Equipped!";
            buyItemButton.SetActive(false);
        }
       
    }

    public void BuyItem()
    {
        if(selectedUIObject != null && selectedUIObject.GetComponent<IceCreamUI>().state == IceCreamUI.STATE.lockedInShop)
        {
            IceCreamUI iceCreamComponent = selectedUIObject.GetComponent<IceCreamUI>();
            int icecreamcost = iceCreamComponent.cost;

            if(icecreamcost <= money)
            {
                money -= icecreamcost;
                singleton.money -= icecreamcost;
                iceCreamComponent.selected = true;
                iceCreamComponent.state = IceCreamUI.STATE.inShop;
                singleton.unlocked.Add(iceCreamComponent.name);
                SetShopDescription(selectedUIObject);
            }
            else
            {
                Instantiate(spawnText, moneytextcomponent.transform);
            }

        }
        SetShopDescription(selectedUIObject);   
        
    }

    public void BuyTimer()
    {
        if(money >= timerCost)
        {
            money -= timerCost;
            singleton.money -= timerCost;

            shoptimer += 10;
            singleton.timer += 10;

            timesTimerBought += 1;
            singleton.timesTimerBought += 1;
        }
        else
        {
            Instantiate(spawnText, moneytextcomponent.transform);
        }


    }
}
