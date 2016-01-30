using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class CultistController : MonoBehaviour {

    [SerializeField]
    private Transform[] ladderChecks;
    public float speed;
    public Boundary boundary;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    public void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        TryToClimbLadder();
    }

    private void TryToClimbLadder()
    {
        GameState.instance.CultistState.climbEnabled = false;
        foreach (Transform ladderCheck in ladderChecks)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, ladderCheck.position, 1 << LayerMask.NameToLayer("Ladder"));

            if (hit)
            {
                GameState.instance.CultistState.climbEnabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement;
        if (GameState.instance.CultistState.climbEnabled)
        {
            myRigidBody.gravityScale = 0.0f;
            movement = new Vector2(speed * moveHorizontal, speed * moveVertical);
        } else
        {
            myRigidBody.gravityScale = 1.0f;
            movement = new Vector2(speed * moveHorizontal, 0.0f);
        }
        myRigidBody.velocity = movement;
        myAnimator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        myRigidBody.position = new Vector3(
            Mathf.Clamp(myRigidBody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(myRigidBody.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );
    }
}