using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTransition : MonoBehaviour
{
    [SerializeField] public float animTime = 0.6f;
    [SerializeField] public float delay = 0.6f;
    [SerializeField] public float preDelay = 0f;

    [SerializeField] protected float startPosX;
    [SerializeField] protected float startPosY;
    [SerializeField] protected float startPosZ;

    [SerializeField] protected float endPosX;
    [SerializeField] protected float endPosY;
    [SerializeField] protected float endPosZ;

    public bool reverse = false;

    //booleans to decide the behaviour of the code, could be better ways to do this
    [SerializeField] protected bool callAtStart = false;
    
    public abstract void CoroutineTransition();

}
