using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float boostSpeed;
    [SerializeField] float baseSpeed;
    [SerializeField] float gameOverSpeed;
    [SerializeField] float torqueAmount = 1;
    SurfaceEffector2D surfaceEffector2D;
    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControl()
    {
        gameOver = true;
        surfaceEffector2D.speed = gameOverSpeed;
    }
    void RespondToBoost()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2D.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2D.AddTorque(-torqueAmount);
        }
    }
}
