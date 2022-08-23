using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    [SerializeField] float jumpSpeed = 15.0f;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] float gravityScaleAtStart;
    [SerializeField] GameObject arrows;
    [SerializeField] Transform bow;
    bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }
    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        if(value.isPressed)
        {
            Instantiate(arrows,bow.position,transform.rotation);
        }
    }
    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.enabled = true;
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        myAnimator.enabled = true;
        myRigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        bool hasClimbingSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", hasClimbingSpeed);
        if (!hasClimbingSpeed && !feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.enabled = false;
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }
    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void Die()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Spikes")) || bodyCollider.IsTouchingLayers(LayerMask.GetMask("enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDead");
            myRigidbody.velocity = deathKick;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
