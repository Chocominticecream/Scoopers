using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IceCreamTrayScript : MonoBehaviour
{
    public GameObject iceCream;
    public GameObject spawnpoint;
    public GameObject spawnpointpos;

    public GameObject currentScoop; //
    private bool scooping = false;

    //values for ice cream prefab (since prefabs can only be assigned values on instantiation)
    public Sprite newsprite; //
    private Image imageComponent;


    RectTransform rectTransform;

    private IceCreamScriptable _iceCreamData;

    public IceCreamScriptable iceCreamData
    {
        get {return _iceCreamData;}
        set
        {
            print("setting data..");
            _iceCreamData = value;
            newsprite = Sprite.Create(_iceCreamData.iceCreamGraphic, new Rect(0, 0, _iceCreamData.iceCreamGraphic.width, _iceCreamData.iceCreamGraphic.height), new Vector2(0.5f, 0.5f));
            transform.Find("ColliderGraphic").GetComponent<Image>().color = SetColorFromHex(_iceCreamData.colorcode);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scooping)
        {
            //slowly enlarge ice cream over time
            currentScoop.transform.localScale += new Vector3(0.0005f, 0.0005f, 0f);
            // // old
            //currentScoop.transform.localScale += new Vector3(0.0001f, 0.0001f, 0f);
        }
    }

    void Awake()
    {
        //pretty risky to reference the object directly but just wanted to keep the spawn code contained
        spawnpoint = GameObject.FindWithTag("IceCreamSpawn");
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnScoop()
    {
        if(currentScoop == null)
        {
            //cant call prefab variables in awake method, as the reference of the prefab is shared across every icecreamtray
            //so i have to use the costly getcomponent function

            //set scoop position, z value, state and initial scale
            GameObject scoop = Instantiate(iceCream,  MouseWorldPosition(), Quaternion.identity);
            scoop.transform.position = new Vector3(scoop.transform.position.x, scoop.transform.position.y, -21f);

            //set the state
            IceCreamScript icecreamComponent = scoop.GetComponent<IceCreamScript>();
            icecreamComponent.state = IceCreamScript.STATE.isHeld;
            
            //set scriptable objects values
            //set the sprite
            newsprite = Sprite.Create(iceCreamData.iceCreamGraphic, new Rect(0, 0, iceCreamData.iceCreamGraphic.width, iceCreamData.iceCreamGraphic.height), new Vector2(0.5f, 0.5f));
            SpriteRenderer iceCreamSpriteComponent = scoop.GetComponent<SpriteRenderer>();
            iceCreamSpriteComponent.sprite = newsprite;

            //set scoopability
            icecreamComponent.scoopability = iceCreamData.scoopability;
            
            //set initialscale plus other values
            scoop.transform.localScale = new Vector3(0.1f, 0.1f, scoop.transform.position.z);
            currentScoop = scoop;
            scooping = true;
        }

        print("scooping!");
    }

    public void EndScoop()
    {
        if(currentScoop != null)
        {
            IceCreamScript icecreamComponent = currentScoop.GetComponent<IceCreamScript>();
            currentScoop.transform.SetParent(spawnpoint.transform);
            icecreamComponent.OnMouseUp();
            
            scooping = false;
            icecreamComponent.collider.enabled = true;
            currentScoop = null;
        }
        print("scooping ended!");
    }

    public void ExitTray()
    {
        scooping = false;
    }

    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private Color SetColorFromHex(string hexColor)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            print("parsed " + hexColor);
            return newColor; 
        }
        return newColor;
    }

    public void SwitchTrays(bool switchIn)
    {
        if(switchIn)
        {
            LeanTween.move(rectTransform, new Vector2(0, rectTransform.anchoredPosition.y), 3f).setEaseOutExpo();
            //rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
        }
        else
        {
            LeanTween.move(rectTransform, new Vector2(-500, rectTransform.anchoredPosition.y), 3f).setEaseOutExpo();
            //rectTransform.anchoredPosition = new Vector2(-500, rectTransform.anchoredPosition.y);
        }
    }

}
