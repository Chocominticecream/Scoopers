using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCamera : MonoBehaviour
{
    public enum EFFECT {normal, transitionToShop}
    public UnityEvent transMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftCamera(float movement)
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, new Vector3(transform.position.x,movement,transform.position.z), 3f).setEaseOutExpo();
    }

    public void CenterCamera()
    {
        StartCoroutine(CenterCameraCoroutine());
    }

    private IEnumerator CenterCameraCoroutine()
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, new Vector3(transform.position.x,0f,transform.position.z), 3f).setEaseOutExpo();
        yield return new WaitForSeconds(3);
        transMenu.Invoke();
    }


}
