using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Shooter shooter;
    [SerializeField] float moveSpeed = 5f;
    

    [Header("Padding")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    [Header("Bounds")]
    Vector2 minBound;
    Vector2 maxBound;
    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    private void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void Move()
    {
        Vector2 newPos = new Vector2();
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(transform.position.x+delta.x,minBound.x + paddingLeft,maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y+delta.y,minBound.y + paddingBottom,maxBound.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter!=null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
