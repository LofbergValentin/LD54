using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigation : MonoBehaviour
{

    public void OnLevelClicked(Level level)
    {
        GameManager.Instance.CurrentLevel = level;
        GameManager.Instance.StartLevel();
    }
}
