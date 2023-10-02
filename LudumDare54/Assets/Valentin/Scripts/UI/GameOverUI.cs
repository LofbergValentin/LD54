using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject winPanel, loosePanel, backButton;
    [SerializeField] TMPro.TMP_Text leftTime;

    public void DisplayUI(bool value, bool win, float timeLeft)
    {
        backButton.SetActive(value);
        if (win)
        {
            winPanel.SetActive(value);
            var ts = TimeSpan.FromSeconds(timeLeft);
            leftTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);            
        }
        else
        {
            loosePanel.SetActive(value);
        }
    }

    public void HideUI()
    {
        //backButton.SetActive(false);
        winPanel.SetActive(false);
        loosePanel.SetActive(false);
    }

    public void ResetLevel()
    {
        GameObject level = GameManager.Instance.CurrentLevel.ItemsToPlace;
        ItemController itemController = FindFirstObjectByType<ItemController>();
        foreach(ItemHandler item in level.GetComponentsInChildren<ItemHandler>())
        {
            itemController.ResetItem(item);
        }
        level.SetActive(false);
        //backButton.SetActive(false);
    }
}
