using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
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
        LTSeq sequence = LeanTween.sequence();
        sequence.append(LeanTween.rotate(gameObject, new Vector3(0,0,-3), 2f).setEaseOutSine());
        sequence.append(LeanTween.rotate(gameObject, new Vector3(0,0,3), 2f).setEaseOutSine());

        sequence.append(() => StartLoopingSequence());
    }

    void StartLoopingSequence()
    {
        Awake();
    }
}
