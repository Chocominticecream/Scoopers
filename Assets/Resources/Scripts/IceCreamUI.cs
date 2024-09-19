using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IceCreamUI : MonoBehaviour 
{
    public UnityEvent selectClick;
    public UnityEvent alternateClick;
    public enum STATE {equipped, inShop, selectedInShop, equippedInShop};

    public RectTransform recttransform;
    public GameObject selectedIndicator;

    private CanvasGroup canvasGroup;

    public bool unlocked = false;
    public string name = "";

    //for selection
    private Color darkenedColor;
    private Color originalColor;
    private Image imageComponent;


    public STATE state = STATE.inShop;

    private IceCreamScriptable _iceCreamObj;

    public IceCreamScriptable iceCreamObj
    {
        get {return _iceCreamObj;}
        set
        {
            _iceCreamObj = value;
            name = _iceCreamObj.name;
            Sprite newsprite = Sprite.Create(_iceCreamObj.iceCreamGraphic, new Rect(0, 0, _iceCreamObj.iceCreamGraphic.width, _iceCreamObj.iceCreamGraphic.height), new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = newsprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state != STATE.equippedInShop)
        {
            normaliseColor();
        }

        if(state != STATE.selectedInShop)
        {
            selectedIndicator.SetActive(false);
        }

        switch(state)
        {
            case STATE.equippedInShop:
                DarkenColor();
                break;
            case STATE.selectedInShop:
                selectedIndicator.SetActive(true);
                break;
            case STATE.inShop:
                break;
            default:
                break;
        }
    }

    public void scaleObject(bool expand)
    {
        if(expand)
        {
            LeanTween.scale(recttransform, new Vector2(1.2f,1.2f), 0.2f);

        }
        else
        {
            LeanTween.scale(recttransform, new Vector2(1f,1f), 0.2f);
        }

    }

    public void OnClick()
    {
        if(state == STATE.inShop)
        {
            selectClick.Invoke();
        }
        else if(state == STATE.equipped)
        {
            alternateClick.Invoke();
        }
        
    }

    private void ReplaceEquipped()
    {
        alternateClick.Invoke();

    }

    void Awake()
    {
        recttransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        imageComponent = GetComponent<Image>();
        Color currentColor = imageComponent.color;
        originalColor = currentColor;
        darkenedColor = currentColor * 0.7f;

        ShopManagerScript shopManager = GameObject.FindGameObjectWithTag("ShopController").GetComponent<ShopManagerScript>();
        selectClick.AddListener(() => shopManager.SelectItem(gameObject));

        //this method grabs the parent, usually not recommended but i ran out of time lol
        alternateClick.AddListener(() => shopManager.ReplaceEquipped(name, transform.parent));
    }

    private void DarkenColor()
    {
         // Get the Image component

        if (imageComponent != null)
        {
            imageComponent.color = darkenedColor;
        }
    }

    private void normaliseColor()
    {
        if (imageComponent != null)
        {
            imageComponent.color = originalColor;
        }

    }

}
