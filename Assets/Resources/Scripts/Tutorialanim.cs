using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialanim : MonoBehaviour
{
    RectTransform recttransform;
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
        LeanTween.scale(recttransform, new Vector2(1.2f,1.2f), 0.6f).setLoopPingPong();
    }
}
