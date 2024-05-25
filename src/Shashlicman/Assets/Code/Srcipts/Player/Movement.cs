using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Робив Віталій
 * 
 TODO: 
 ◦ Пересування +
 ◦ ухиляння
 ◦ атаки ближнього бою
    ▪ Буде один Шампур як основна та єдина зброя.
        ▪ Можливо його посилити отримаючі його з противників, але
          зменшує швидкість атаки
 
 */

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }
}
