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
 ◦ ухиляння +

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

    private readonly float customE = math.E - 1.512f;

    [SerializeField] private float evasion_speed_coefficient = 4f;
    [SerializeField] private float evasion_cooldown_period = 1f;
    private bool IsAvailable = true;

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

        movementDirection = move();

    }

    private Vector2 move()
    {
        float power_control_level = customExp(control_level);

        float evasion_coefficient = useEvasion();

        float x_player = evasion_coefficient * moveCoefficientPlayer(Input.GetAxis("Horizontal"), power_control_level);
        float y_player = evasion_coefficient * moveCoefficientPlayer(Input.GetAxis("Vertical"), power_control_level);

        float x_badSpirit = moveCoefficientBadSpirit(toVector(enemy.transform.position.x, player.transform.position.x), power_control_level);
        float y_badSpirit = moveCoefficientBadSpirit(toVector(enemy.transform.position.y, player.transform.position.y), power_control_level);

        return normalisationVector(x_player, y_player, x_badSpirit, y_badSpirit);
    }

    private float useEvasion()
    {

        if (Input.GetButton("Evasion") && (IsAvailable == true))
        {
            StartCoroutine(StartCooldown());
            return evasion_speed_coefficient;
        }

        return 1;

    }

    public IEnumerator StartCooldown()
    {
        IsAvailable = false;

        yield return new WaitForSeconds(evasion_cooldown_period);

        IsAvailable = true;
    }

    private float toVector(float enemy_x, float player_x)
    {
        float scalara_x = math.abs(enemy_x - player_x) + Random.Range(-20, 20);

        if (enemy_x < player_x)
            scalara_x = -scalara_x;

        return scalara_x;
    }
    

    private float moveCoefficientPlayer(float x, float power_control_level)
    {
        return x / customE * power_control_level;
    }

    private float moveCoefficientBadSpirit(float x, float power_control_level)
    {
        return x / power_control_level - x / customE;
    }

    private float customExp(float x)
    {
        return math.pow(customE, x);
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }

    private Vector2 normalisationVector(float x_p, float y_p, float x_b, float y_b)
    {
        float x_result = 0, y_result = 0;
        x_result = isSameSign(x_p, x_b);
        y_result = isSameSign(y_p, y_b);
        return new Vector2(x_result, y_result);
    }

    private float isSameSign(float x, float y)
    {
        if (x == 0 || (y == 0))
            return x + y;

        if (x == math.abs(x) && (y == math.abs(y) ||
            (-x == math.abs(x) && (-y == math.abs(y)))))
            return math.max(x, y);

        return x + y;
    }
}
