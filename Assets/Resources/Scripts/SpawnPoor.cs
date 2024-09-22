using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoor : MonoBehaviour
{
    private RectTransform recttransform;
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
        recttransform = GetComponent<RectTransform>();
        StartCoroutine(SpawnText());

    }

    private IEnumerator SpawnText()
    {
        LeanTween.move(recttransform, new Vector3(recttransform.position.x,recttransform.position.y-70,recttransform.position.z), 1f).setEaseOutExpo();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
