using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RitualColliderController : MonoBehaviour {

    private bool executionEnabled = false;
    public RectTransform image;

    private LevelAndClockController levelAndClockController;
    private CultistController cultistController;
    private SoundController soundController;

    void Start()
    {
        cultistController = FindObjectOfType<CultistController>();
        levelAndClockController = FindObjectOfType<LevelAndClockController>();
        soundController = FindObjectOfType<SoundController>();
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
            Destroy(gameObject);

            levelAndClockController.ProgessAndCheckWin(gameObjectName);
            cultistController.Animate(gameObjectName);
            soundController.PlayByName(gameObjectName);
        }
    }

}
