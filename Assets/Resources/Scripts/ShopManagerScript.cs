using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerScript : MonoBehaviour
{
    public GameObject shopSlots;
    public GameObject selectedUIObject;
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
    
    void Awake()
    {
        singleton = Singleton.Instance;
    }

    public void SelectItem(GameObject selectedUI)
    {
        IceCreamUI selectedUIcomponent = selectedUI.GetComponent<IceCreamUI>();
        foreach(Transform iceCreamUI in shopSlots.transform)
        {
            IceCreamUI iceCreamuiComponent = iceCreamUI.gameObject.GetComponent<IceCreamUI>();

            if(selectedUIcomponent.name == iceCreamUI.gameObject.GetComponent<IceCreamUI>().name)
            {
                iceCreamuiComponent.state = IceCreamUI.STATE.selectedInShop;
            }
            else if(iceCreamuiComponent.state != IceCreamUI.STATE.equippedInShop)
            {
                iceCreamuiComponent.state = IceCreamUI.STATE.inShop;
            }
                
        }
        selectedUIObject = selectedUI;
        print(selectedUI.GetComponent<IceCreamUI>().name);
    }

    public void ReplaceEquipped(string equippedObjName, Transform equippedSlot)
    {
        int slotIndex = equippedSlot.GetSiblingIndex();
        
        if(selectedUIObject != null)
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
            selectedUIObject = null;
        }
        

    }
}
