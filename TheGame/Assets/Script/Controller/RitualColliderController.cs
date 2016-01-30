using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RitualColliderController : MonoBehaviour {

    private bool executionEnabled = false;
    public RectTransform image;

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
            GameState.instance.LevelState.AddExecutedRitual(this.gameObject.name);
            Destroy(image.GetComponent<Image>());
            Destroy(gameObject);
        }

    }
}
