using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 300.0f;
    [SerializeField] float moveSpeed = 20.0f;
    [SerializeField] float slowSpeed = 15.0f;
    [SerializeField] float boostSpeed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward *  -steerAmount);
        transform.Translate(Vector2.up * moveAmount);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            moveSpeed = boostSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = slowSpeed;
    }
}
