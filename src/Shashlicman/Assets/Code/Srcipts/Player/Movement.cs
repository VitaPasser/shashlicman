using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private Player player;
    [SerializeField] private Enemy enemy;
    private Vector2 movementDirection;
    private float control_level { get => player.emotion_level / 100.0f; }
    private float power_control_level = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.is_dead)
        {
            movementDirection = Vector2.zero;
            return;
        }

        power_control_level = math.exp(control_level);

        float x_player = Input.GetAxis("Horizontal");
        float y_player = Input.GetAxis("Vertical");

        float scalara_x = math.abs(enemy.transform.position.x - player.transform.position.x);
        float scalara_y = math.abs(enemy.transform.position.y - player.transform.position.y);

        if (enemy.transform.position.x < player.transform.position.x)
            scalara_x = -scalara_x;

        if (enemy.transform.position.y < player.transform.position.y)
            scalara_y = -scalara_y;

        float x_badSpirit = scalara_x;
        float y_badSpirit = scalara_y;

        x_player = x_player / math.E * power_control_level;
        y_player = y_player / math.E * power_control_level;
        x_badSpirit = x_badSpirit / power_control_level - x_badSpirit / math.E;
        y_badSpirit = y_badSpirit / power_control_level - y_badSpirit / math.E;

        movementDirection = movement(x_player, y_player, x_badSpirit, y_badSpirit);

    }



    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }

    private Vector2 movement(float x_p, float y_p, float x_b, float y_b)
    {
        float x_result = 0, y_result = 0;
        x_result = isSameSign(x_p, x_b);
        y_result = isSameSign(y_p, y_b);
        return new Vector2(x_result, y_result);
    }

    private float isSameSign(float x, float y)
    {
        if (x == math.abs(x) && (y == math.abs(y) ||
            (-x == math.abs(x) && (-y == math.abs(y)))))
            return math.max(x, y);
        return x + y;
    }
}
