using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My Assets/Level")]
public class Level : ScriptableObject
{
    public string DisplayName;
    public string ItemsToPlace;
    public float timer;
}
