using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelAndClockController : MonoBehaviour {

    private LevelLoader levelLoader;
    private Text clockText;
    private GameObject ritualContainer;
    private GameObject player;
    private GameObject failedPanel;

    public int lengthOfLevel = 10;
    private float counter = 0.0f;

    private float countDown = 2.0f;

    [SerializeField]
    private GameObject[] ritualsToComplete;

	void Start () {
        levelLoader = FindObjectOfType<LevelLoader>();
        clockText = GameObject.Find("Clock").GetComponent<Text>();
        ritualContainer = GameObject.Find("RitualContainer");
        player = GameObject.FindGameObjectWithTag("Player");
        failedPanel = GameObject.Find("FailedPanel");

        failedPanel.SetActive(false);
        counter = lengthOfLevel;

        GameState.instance.LevelState.ResetExecutedRituals();

        SeedRitualOrder();
	}
	
	void Update () {
        float timePassed = Time.deltaTime;
        counter -= timePassed;

        if (counter > 0)
        {
            string seconds = string.Format("0{0:x2}", ((int)counter).ToString());
            clockText.text = "00:" + seconds;
        }

        if (counter <= 0)
        {
            // level should be ended until now...
            failedPanel.SetActive(true);
            Destroy(player);
            if (countDown <= 0)
            {
                levelLoader.LoadCurrentLevel();
            }
            countDown -= timePassed;
        }

	}

    private void SeedRitualOrder()
    {

    }
}
