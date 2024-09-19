using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonJuice : MonoBehaviour
{
    //bubbling signals?
    [SerializeField] private UnityEvent triggerAfterPress;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExpandButton()
    {
        LeanTween.scale(gameObject, new Vector3((1.2f),(1.2f),transform.localScale.z), 0.2f).setEaseOutBounce();
    }

    public void ShrinkButton()
    {
        LeanTween.scale(gameObject, new Vector3((1f),(1f),transform.localScale.z), 0.2f);
    }

    public void SelectShake()
    {
        StartCoroutine(SelectShakeCoroutine());
    }

    private IEnumerator SelectShakeCoroutine()
    {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.rotate(gameObject, new Vector3((0f),(0f),-10f), 0.1f));
        seq.append(LeanTween.rotate(gameObject, new Vector3((0f),(0f),0f), 0.1f));
        yield return new WaitForSeconds(0.2f);
        triggerAfterPress.Invoke();
    }

}
