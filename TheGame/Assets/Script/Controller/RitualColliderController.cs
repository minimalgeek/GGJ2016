using UnityEngine;
using System.Collections;

public class RitualColliderController : MonoBehaviour {

    private bool executionEnabled = false;

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
            GameState.instance.RitualState.AddExecutedRitual(this.gameObject.name);
            Destroy(gameObject);
        }

    }
}
