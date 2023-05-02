using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StateChange : MonoBehaviour
{
    public float timeForInfection;
    public float infectionInterval;
    public float infectionDuration;
    public float score;
    public float ftime;
    public int iTime;

    public GameObject startMenu;
    public GameObject endScreen;
    public GameObject gameUI;

    public Material hittableMaterial;
    public Material normalMaterial;

    public TextMeshProUGUI infectedDevices;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI endScore;

    public List<GameObject> objectsToInfect;

    Button startButton;
    public Button restartButton;

    int numberOfInfections = 0;

    public bool gameIsActive = false;


    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("Start Button").GetComponent<Button>();

        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        score = 0;

        ftime = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(ftime > 0 && gameIsActive)
        {
            // Infect a machine every time the infection interval elapses

            if (Time.time > timeForInfection)
            {
                InfectNewObject();

                timeForInfection = Time.time + infectionInterval;
            }
        }

        if(gameIsActive)
        {
            // Keeping track of time

            GameTimer();

            // Displays on screen

            scoreText.text = "Score: " + score;

            timeText.text = "Time: " + iTime;

            infectedDevices.text = "Infected Devices:" + numberOfInfections;
        }

        if(ftime == 0)
        {
            gameIsActive = false;

            Cursor.lockState = CursorLockMode.None;

            endScreen.gameObject.SetActive(true);

            endScore.text = "Score:" + score;
        }
    }

    // Procedure of infection
    void InfectNewObject()
    {
        // Get a random game object from the list

        int randomIndex = Random.Range(0, objectsToInfect.Count);

        GameObject objectToInfect = objectsToInfect[randomIndex];

        // Get its mesh renderer

        MeshRenderer meshRenderer = GameObject.Find(objectToInfect.name).GetComponent<MeshRenderer>();

        // recall the method if object is already infected, else infect object

        if (meshRenderer.material == hittableMaterial)
        {
            InfectNewObject();
        } else
        {
            StartCoroutine(InfectTemporarily());
        }

        // Method to infect object then return it to normal and to keep track of how many objects are infected.

        IEnumerator InfectTemporarily()
        {
            meshRenderer.material = hittableMaterial;
            numberOfInfections++;
            objectToInfect.gameObject.tag = "Infected";
            yield return new WaitForSeconds(infectionDuration);
            objectToInfect.gameObject.tag = "Infectuous";
            meshRenderer.material = normalMaterial;
            numberOfInfections--;
        }
    }

    // Timer settings

    void GameTimer()
    {
        if(ftime > 0)
        {
            ftime -= Time.deltaTime;

            iTime = (int)ftime;
        }
        else
        {
            ftime = 0;
        }

    }

    // Start game procedure when start is clicked

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;

        gameIsActive = true;

        startMenu.gameObject.SetActive(false);

        gameUI.gameObject.SetActive(true);
    }

    // Restart

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
