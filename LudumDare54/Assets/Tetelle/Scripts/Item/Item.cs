using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My Assets/item")]
public class Item : ScriptableObject
{
    public string DisplayName;
    public List<Vector3> Points;
    public GameObject Prefab;
    public bool IsStocked = false;
       
}
