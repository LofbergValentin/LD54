using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]List<Item> items;
    Point testPoint, testPoint2;
    [SerializeField] Suitcase valise;

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
        testPoint=valise.GetPoint(0);
        testPoint2 = valise.GetPoint(6);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetItemIntoSuitcase(items[0], testPoint);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetItemIntoSuitcase(items[2], testPoint2);
        }
    }

    private void SetItemIntoSuitcase(Item item, Point point)
    {
        bool busy = valise.containsFullPoint(item.Points, point);
        //Debug.Log("x:" + testPoint2.Position.x + " y:" + testPoint2.Position.y + " z:" + testPoint2.Position.z);
        if (!busy)
        {

            GameObject newItem = Instantiate(item.Prefab, point.Position, Quaternion.identity);
            item.IsStocked = true;
            if (checkIsFinished()) { Debug.Log("fini bien ouej"); }
        }
        else
        {
            Debug.Log("fess");
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
