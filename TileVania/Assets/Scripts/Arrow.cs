using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float arrowSpeed = 2f;
    float xSpeed;
    BoxCollider2D arrowCollider;
    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        myRigidBody = GetComponent<Rigidbody2D>();
        arrowCollider = GetComponent<BoxCollider2D>();
        transform.localScale = new Vector2(player.transform.localScale.x,1);
        xSpeed = player.transform.localScale.x * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(arrowCollider.IsTouchingLayers(LayerMask.GetMask("enemies")))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
