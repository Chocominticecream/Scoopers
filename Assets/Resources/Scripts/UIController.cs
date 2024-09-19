using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //variable to store ui to be called on start
    [SerializeField] private List<GameObject> uiOnStart = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartUIMenus();
        CallStartingUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //make all menu transitions get their ui children
    private void StartUIMenus()
    {
        foreach (Transform child in transform)
        {
            MenuTransition menuComponent = child.GetComponent<MenuTransition>();
            menuComponent.GetUIChildren();
        }
    }
    
    //call the starting UI 
    private void CallStartingUI()
    {
        foreach (GameObject comp in uiOnStart)
        {
            CallUIObject(comp);
        }
    }

    public void CallUIObject(GameObject comp)
    {
        MenuTransition menuComponent = comp.GetComponent<MenuTransition>();
        comp.SetActive(true);
        menuComponent.EventCallUIChildren();
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
