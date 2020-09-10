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
        updateAnimationAndMove();
    }

    void updateAnimationAndMove()
    {
        if (vel != Vector3.zero)
        {
            MoveCharacter();
            //animator.SetFloat("moveX", vel.x);
            //animator.SetFloat("moveY", vel.y);
            //animator.SetBool("moving", true);
        }/*
        else
        {
            //animator.SetBool("moving", false);
        }*/
    }

    void MoveCharacter()
    {
        myRigidBody.AddForce(new Vector2(vel.x*speed*Time.deltaTime, 0));
        
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
