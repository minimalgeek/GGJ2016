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
    //public Boundary boundary;

    private bool climbEnabled;
    private bool facingRight = true;
    private string currentAnimation;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    private GameObject[] ritualImages;

    public void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        ritualImages = GameObject.FindGameObjectsWithTag("RitualImage");

        foreach (GameObject ritualImage in ritualImages)
        {
            ritualImage.SetActive(false);
        }
    }

    void Update()
    {
        TryToClimbLadder();

        if (NotRitualAnimatorState())
        {
            ActivateImages(currentAnimation, false);
        } else
        {
            ActivateImages(currentAnimation, true);
        }
    }

    private void ActivateImages(string name, bool setOrNot)
    {
        foreach (GameObject ritualImage in ritualImages)
        {
            if (name == ritualImage.name)
            {
                ritualImage.SetActive(setOrNot);
                break;
            }
        }
    }

    private void TryToClimbLadder()
    {
        climbEnabled = false;
        foreach (Transform ladderCheck in ladderChecks)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, ladderCheck.position, 1 << LayerMask.NameToLayer("Ladder"));

            if (hit)
            {
                climbEnabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement;
        if (climbEnabled)
        {
            myRigidBody.gravityScale = 0.0f;
            movement = new Vector2(speed * moveHorizontal, speed * moveVertical);
        } else
        {
            myRigidBody.gravityScale = 1.0f;
            movement = new Vector2(speed * moveHorizontal, myRigidBody.velocity.y);
        }
        myRigidBody.velocity = movement;
        myAnimator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        Flip(moveHorizontal);

        /*
        myRigidBody.position = new Vector3(
            Mathf.Clamp(myRigidBody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(myRigidBody.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );
        */
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Animate(string triggerName)
    {
        myAnimator.SetTrigger(triggerName);
        currentAnimation = triggerName;
    }

    private bool NotRitualAnimatorState()
    {
        return myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("NotRitual");
    }

}