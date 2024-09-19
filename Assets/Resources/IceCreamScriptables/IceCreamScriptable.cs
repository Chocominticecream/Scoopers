using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IceCreamBase", menuName = "IceCream")]
public class IceCreamScriptable : ScriptableObject
{
    public int stickiness = 2;
    public int value = 2;
    public int scoopability = 2;
    public Texture2D iceCreamGraphic;
    public string colorcode;
    public string name;
    public bool unlocked = false;

}
