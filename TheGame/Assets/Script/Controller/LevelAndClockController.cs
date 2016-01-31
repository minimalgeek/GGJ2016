using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LevelAndClockController : MonoBehaviour {

    private LevelLoader levelLoader;
    private Text clockText;
    private GameObject player;
    private GameObject failedPanel;
    private GameObject winPanel;

    private GameObject[] ritualsToComplete;
    private string[] ritualsStrings;
    private GameObject[] ritualsIcons;

    public int lengthOfLevel = 10;
    private float counter = 0.0f;

    private float countDown = 2.0f;

    private bool levelWon = false;

    private bool newLoaded = false;

	void Start ()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        clockText = GameObject.Find("Clock").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        failedPanel = GameObject.Find("FailedPanel");
        winPanel = GameObject.Find("WinPanel");

        CollectRitualsAndRitualNames();
        DeactivatePanels();

        counter = lengthOfLevel;
        GameState.instance.LevelState.ResetExecutedRituals();
        SeedRitualOrder();
    }

    private void DeactivatePanels()
    {
        failedPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void CollectRitualsAndRitualNames()
    {
        ritualsToComplete = GameObject.FindGameObjectsWithTag("Ritual");
        ritualsIcons = GameObject.FindGameObjectsWithTag("RitualIcon");

        ritualsStrings = new string[ritualsToComplete.Length];
        int idx = 0;
        foreach (GameObject ritual in ritualsToComplete)
        {
            ritualsStrings[idx++] = ritual.name;
        }

        foreach (GameObject ritual in ritualsIcons)
        {
            ritual.SetActive(false);
        }
    }

    void Update () {
        float timePassed = Time.deltaTime;
        counter -= timePassed;

        if (levelWon)
        {
            // level should be ended until now...
            winPanel.SetActive(true);
            FreezePlayer();
            if (countDown <= 0 && !newLoaded)
            {
                levelLoader.LoadNextLevel();
                newLoaded = true;
            }
            countDown -= timePassed;
        }
        else
        {
            if (counter > 0)
            {
                string seconds = ((int)counter).ToString();
                if (seconds.Length == 2)
                {
                    clockText.text = "00:" + seconds;
                }
                else
                {
                    clockText.text = "00:0" + seconds;
                }
            }
            else if (counter <= 0)
            {
                // level should be ended until now...
                failedPanel.SetActive(true);
                FreezePlayer();
                if (countDown <= 0)
                {
                    levelLoader.LoadCurrentLevel();
                }
                countDown -= timePassed;
            }
        }
	}

    private void FreezePlayer()
    {
        player.GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionX |
                RigidbodyConstraints2D.FreezePositionY;
    }

    private void SeedRitualOrder()
    {
        ritualsStrings = Shuffle(ritualsStrings);

        int idx = 0;
        foreach (string ritualName in ritualsStrings)
        {
            foreach (GameObject ritualIcon in ritualsIcons)
            {
                if (ritualIcon.name == ritualName)
                {
                    ritualIcon.SetActive(true);
                    Vector3 oriPos = ritualIcon.GetComponent<RectTransform>().anchoredPosition;
                    oriPos.x = 10 + idx * 53;
                    ritualIcon.GetComponent<RectTransform>().anchoredPosition = oriPos;
                    idx++;
                }
            }
        }
    }
    
    public void ProgessAndCheckWin(string newlyCompletedRitual)
    {
        LevelState levelState = GameState.instance.LevelState;
        if (levelState.LatestCompletedIndex == ritualsStrings.Length - 1)
        {
            levelWon = true;
        }
        else if (! (ritualsStrings[levelState.LatestCompletedIndex] == newlyCompletedRitual))
        {
            // wrong ritual order!
            counter = 0;
        }
    }
    
    private string[] Shuffle(string[] list)
    {
        System.Random rnd = new System.Random();
        return list.OrderBy(x => rnd.Next()).ToArray();
    }
}
