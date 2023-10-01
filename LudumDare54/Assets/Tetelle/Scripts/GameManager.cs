using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool Finished;

    private float ellapsedTime;

    public Level CurrentLevel;

    public float startDelay = 3f;

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

        Finished = true;
    }

    private void Update()
    {
        if(!Finished)
        {
            ellapsedTime += Time.deltaTime;
        }               
    }

    public void StartLevel()
    {
        //GameObject.Find(CurrentLevel.ItemsToPlace).SetActive(true);
    }

    public void EndLevel()
    {

    }
}
