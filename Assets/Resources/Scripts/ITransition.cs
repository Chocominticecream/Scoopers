using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    IEnumerator CoroutineTransition();
    void CallTransition();
}
