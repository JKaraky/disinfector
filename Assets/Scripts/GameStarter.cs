using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    StateChange stateChange;
    void Start()
    {
        stateChange = GameObject.Find("State Changer").GetComponent<StateChange>();
    }
    public void StartGame()
    {
        stateChange.score = 0;

        stateChange.ftime = 10;

        stateChange.gameIsActive = true;

        stateChange.startMenu.gameObject.SetActive(false);

        stateChange.gameUI.gameObject.SetActive(true);

        Debug.Log("I was clicked");
    }
}
