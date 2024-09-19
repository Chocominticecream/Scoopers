using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//cant edit this in the inspector so i guess I have to manually invoke it
public class MenuUnityEvent : UnityEvent<GameObject>{}

public class MenuTransition : MonoBehaviour
{
    private ArrayList uiElements = new ArrayList();
    private GameObject calledMenu;
    [SerializeField] private MenuUnityEvent playOtherUI = new MenuUnityEvent();

    //setters and getters + setter method so that it can be called from unity event

    // Start is called before the first frame update
    void Start()
    {
        //GetUIChildren();
        //EventCallUIChildren();
        UIController findUI = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        playOtherUI.AddListener(findUI.CallUIObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUIChildren()
    {
        print("grabbed children!");
        foreach (Transform child in transform)
        {
            uiElements.Add(child.gameObject);
        }
    }

    public void EventCallUIChildren(bool reversal = false)
    {
        StartCoroutine(CallUIChildren(reversal));
    }

    public void setCalledMenu(GameObject menu)
    {
        calledMenu = menu;
    }

    private IEnumerator CallUIChildren(bool reversal=false)
    {
        //if not reversal set the menu to true 
        if(!reversal)
        {
            gameObject.SetActive(true);
        }

        //disable ui interactions when first calling animations
        for (int i = 0; i < uiElements.Count; i++)
        {
            GameObject uiElement = (GameObject)uiElements[i];
            CanvasGroup comp = uiElement.GetComponent<CanvasGroup>();
            if(comp != null)
            {
                comp.blocksRaycasts = false;
            }
        }
        
        //play the animations of ui elements
        for (int i = 0; i < uiElements.Count; i++)
        {
            GameObject uiElement = (GameObject)uiElements[i];
            BaseTransition transitionComponent = uiElement.GetComponent<BaseTransition>();
            
            if(transitionComponent != null)
            {
                transitionComponent.reverse = reversal;
                //make elements visible if transitioning in
                if(!reversal)
                {
                   uiElement.GetComponent<CanvasGroup>().alpha = 1;
                }

                yield return new WaitForSeconds(transitionComponent.preDelay);
                transitionComponent.CoroutineTransition();
                yield return new WaitForSeconds(transitionComponent.delay);
                
                //make elements invisible if going in reverse
                if(reversal)
                {
                   uiElement.GetComponent<CanvasGroup>().alpha = 0;
                }
                
            }
        }
        
        //block raycasts once more so that ui can be clickable
        for (int i = 0; i < uiElements.Count; i++)
        {
            GameObject uiElement = (GameObject)uiElements[i];
            CanvasGroup comp = uiElement.GetComponent<CanvasGroup>();
            if(comp != null)
            {
                comp.blocksRaycasts = true;
            }
        }
        
        //if reversing (transitioning into another menu) set the active state to 
        //also call the UI of the next menu it transitions into
        if(reversal)
        {
            if(calledMenu != null)
            {
                playOtherUI.Invoke(calledMenu);
            }
            gameObject.SetActive(false);
        }

       
    }


}
