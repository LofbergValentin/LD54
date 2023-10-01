using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]List<Item> items;
    public Suitcase valise;

    public static GameManager Instance { get => instance; set => instance = value; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    /*public void SetItemIntoSuitcase(ItemHandler item, Point point)
    {
        bool busy = valise.ContainsFullPoint(item, point);
        if (!busy)
        {
            item.Item.IsStocked = true;
            if (CheckIsFinished()) 
            { 
                Debug.Log("you have finished the game");
            }
        }
        else
        {
            Debug.Log("Point already full");
        }
    }

    private bool CheckIsFinished()
    {
        bool finished = true;
        foreach (Item item in items)
        {
            Debug.Log(item.DisplayName + " " + item.IsStocked);
            if(!item.IsStocked)
            {
                finished = false;
            }
        }
        return finished;
    }*/
}
