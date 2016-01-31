using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RitualColliderController : MonoBehaviour {

    private bool executionEnabled = false;
    public RectTransform image;

    private LevelAndClockController levelAndClockController;
    private CultistController cultistController;
    private SoundController soundController;

    public GameObject pentaMonsta;

    void Start()
    {
        cultistController = FindObjectOfType<CultistController>();
        levelAndClockController = FindObjectOfType<LevelAndClockController>();
        soundController = FindObjectOfType<SoundController>();
        pentaMonsta = GameObject.Find("PentaMonsta");
        pentaMonsta.GetComponent<SpriteRenderer>().sortingOrder = -10;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            executionEnabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            executionEnabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && executionEnabled)
        {
            string gameObjectName = this.gameObject.name;
            GameState.instance.LevelState.AddExecutedRitual(gameObjectName);
            Destroy(image.GetComponent<Image>());

            if (gameObjectName == "Pentagram")
            {
                pentaMonsta.GetComponent<SpriteRenderer>().sortingOrder = 0;
                pentaMonsta.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            } else if (gameObjectName == "Shower" || gameObjectName == "Poop")
            {
                // TODO figure it out
            } else 
            {
                Destroy(gameObject);
            }

            levelAndClockController.ProgessAndCheckWin(gameObjectName);
            cultistController.Animate(gameObjectName);
            soundController.PlayByName(gameObjectName);
        }
    }

}
