using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    StateChange stateChange;
    void Start()
    {
        stateChange = StateChange.instance;
    }
    public void StartGame()
    {
        stateChange.score = 0;

        stateChange.ftime = 10;

        stateChange.gameIsActive = true;

        stateChange.startMenu.gameObject.SetActive(false);

        stateChange.gameUI.gameObject.SetActive(true);
    }
}
