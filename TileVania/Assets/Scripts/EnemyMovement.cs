using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    CircleCollider2D bodyCollider;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0);
    }
    void Flip()
    {
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        Flip();
    }
}
