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

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetItemIntoSuitcase(Item item, Point point)
    {
        bool busy = valise.containsFullPoint(item.Points, point);
        if (!busy)
        {
            item.IsStocked = true;
            if (checkIsFinished()) 
            { 
                Debug.Log("you have finished the game");
            }
        }
        else
        {
            Debug.Log("Point already full");
        }
    }

    private bool checkIsFinished()
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
    }
}
