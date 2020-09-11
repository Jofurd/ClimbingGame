using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}
public class Player : MonoBehaviour
{
    public PlayerState currentState;
    public float drag;
    public float dragThreshold;
    public float speed;
    public float jumpHeight;
    private Rigidbody2D myRigidBody;
    private Vector3 vel;
    private Animator animator;

    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vel = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0 && currentState == PlayerState.idle)
        {
            currentState = PlayerState.walk;
        }
        vel.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            myRigidBody.AddForce(new Vector2(0, jumpHeight));
        }
    }

    private void FixedUpdate()
    {
        if (myRigidBody.velocity.x > 0)
        {
            Debug.Log(1);
            myRigidBody.AddForce(new Vector2(-drag * Time.deltaTime, 0));
        }
        else if (myRigidBody.velocity.x < 0)
        {
            Debug.Log(2);
            myRigidBody.AddForce(new Vector2(drag * Time.deltaTime, 0));
        }

        if (Mathf.Abs(myRigidBody.velocity.x) < dragThreshold) { 
            Debug.Log(3);
            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
        }
        updateAnimationAndMove();
    }

    void updateAnimationAndMove()
    {
        if (vel != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        myRigidBody.AddForce(new Vector2(vel.x*speed*Time.deltaTime, 0));
    }

}
