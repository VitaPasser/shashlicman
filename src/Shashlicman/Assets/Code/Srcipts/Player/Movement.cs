using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private Player player;
    private Vector2 movementDirection;
    public float control_level { get; set; }
    private float power_control_level = 0;
    public float critical_level = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        control_level = 1f;
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

        float x_badSpirit = 5f;
        float y_badSpirit = -5f;

        float min_x_player = x_player / math.E;
        float min_y_player = y_player / math.E;

        float max_x_player = x_player - min_x_player;
        float max_y_player = y_player - min_y_player;

        x_player = x_player / math.E * power_control_level - min_x_player;
        y_player = y_player / math.E * power_control_level - min_y_player;

        float min_x_badSpirit = x_badSpirit / math.E;
        float min_y_badSpirit = y_badSpirit / math.E;

        float max_x_badSpirit = x_badSpirit - min_x_badSpirit;
        float max_y_badSpirit = y_badSpirit - min_y_badSpirit;

        x_badSpirit = x_badSpirit / math.E * power_control_level - min_x_badSpirit;
        y_badSpirit = y_badSpirit / math.E * power_control_level - min_y_badSpirit;

        float x_movementDirection = (x_player - x_badSpirit) / 2;
        float y_movementDirection = (y_player - y_badSpirit) / 2;

        /*Vector2 playerMovementDirection = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")))/ math.E;
        Vector2 badSpiritMovementDirection = (new Vector2(x_badSpirit, y_badSpirit));

        playerMovementDirection *= power_control_level;*/

        movementDirection = new Vector2(x_movementDirection, y_movementDirection);
        
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }
}
