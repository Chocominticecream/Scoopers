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
    public UnityEvent deactivateScoopTutorial;
    public UnityEvent deactivateDropTutorial;
    private bool scooping = false;

    //values for ice cream prefab (since prefabs can only be assigned values on instantiation)
    public Sprite newsprite; //
    private Image imageComponent;
    RectTransform rectTransform;

    private int scoopability = 0;

    private IceCreamScriptable _iceCreamData;

    public IceCreamScriptable iceCreamData
    {
        get {return _iceCreamData;}
        set
        {
            print("setting data..");
            _iceCreamData = value;
            scoopability = _iceCreamData.scoopability;
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
        // if(scooping)
        // {
        //     if (currentScoop.transform.localScale.x < (1f+(scoopability-2)*0.1) && currentScoop.transform.localScale.y < (1f+(scoopability-2)*0.1))
        //     {
        //         //slowly enlarge ice cream over time
        //         currentScoop.transform.localScale += new Vector3(0.0002f+((scoopability-1)*0.0001f), 0.0002f+((scoopability-1)*0.0001f), 0f);
        //     }
            
            // // old
            //currentScoop.transform.localScale += new Vector3(0.0001f, 0.0001f, 0f);
        // }
    }

    void Awake()
    {
        //pretty risky to reference the object directly but just wanted to keep the spawn code contained
        spawnpoint = GameObject.FindWithTag("IceCreamSpawn");
        rectTransform = GetComponent<RectTransform>();

        TutorialObjectScript tutorialObjectScript = GameObject.FindWithTag("Tutorial").GetComponent<TutorialObjectScript>();
        deactivateScoopTutorial.AddListener(() => tutorialObjectScript.OnScoopActivate());
        deactivateDropTutorial.AddListener(() => tutorialObjectScript.OnDropActivate());
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
            scoop.transform.SetParent(spawnpoint.transform);

            //set the state
            IceCreamScript icecreamComponent = scoop.GetComponent<IceCreamScript>();
            icecreamComponent.state = IceCreamScript.STATE.isHeld;
            
            //set scriptable objects values
            //set the sprite
            icecreamComponent.iceCreamObj = iceCreamData;
            //set initialscale plus other values
            scoop.transform.localScale = new Vector3(0.1f, 0.1f, scoop.transform.position.z);
            currentScoop = scoop;
            scooping = true;
            InvokeRepeating("ScoopBigger", 0f, 1f / 30f);
            deactivateScoopTutorial.Invoke();
        }

        print("scooping!");
    }

    public void EndScoop()
    {
        if(currentScoop != null)
        {
            IceCreamScript icecreamComponent = currentScoop.GetComponent<IceCreamScript>();
            icecreamComponent.OnMouseUp();
            
            scooping = false;
            icecreamComponent.collider.enabled = true;
            currentScoop = null;
            deactivateDropTutorial.Invoke();
            CancelInvoke("ScoopBigger");
        }
        print("scooping ended!");
    }

    public void ExitTray()
    {
        CancelInvoke("ScoopBigger");
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

    public void ScoopBigger()
    {
         if(currentScoop.transform.localScale.x < (1f+(scoopability-2)*0.1) && currentScoop.transform.localScale.y < (1f+(scoopability-2)*0.1))
         {
            currentScoop.transform.localScale += new Vector3(0.002f+((scoopability-1)*0.001f), 0.002f+((scoopability-1)*0.001f), 0f);
         }
         
    }

}
