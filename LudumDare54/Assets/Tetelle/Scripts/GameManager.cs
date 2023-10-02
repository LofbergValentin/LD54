using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool Finished;

    private float remainingTime;

    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] InGameUI inGameUI;

    [SerializeField] GameObject cameraTransition;

    public Level CurrentLevel;
    bool endGameUIDisplayed;
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

    private void Start()
    {
        mainMenuUI.SetActive(true);
    }

    private void Update()
    {
        if(!Finished)
        {
            remainingTime -= Time.deltaTime;
            inGameUI.SetTimer(remainingTime);
        }
        else if (((Finished && CurrentLevel != null && remainingTime > 0 ) || remainingTime <0)&& !endGameUIDisplayed)
        {
            EndLevel(Finished);
        }
    }

    public void StartLevel()
    {
        CurrentLevel.ItemsToPlace.SetActive(true);
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3);
        cameraTransition.SetActive(false);
        remainingTime = CurrentLevel.timer;
        Finished = false;
        inGameUI.DisplayUI(true);
    }

    public void EndLevel(bool win)
    {
        endGameUIDisplayed = true;
        inGameUI.DisplayUI(false);
        gameOverUI.DisplayUI(true, win, remainingTime);
    }
}
