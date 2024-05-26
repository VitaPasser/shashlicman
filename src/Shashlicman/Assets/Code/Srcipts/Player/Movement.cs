using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Робив Віталій
 * 
 TODO: 
 ◦ Пересування +
 ◦ ухиляння +
 
 */

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    private Rigidbody2D rb;
    private Player player;
    private Vector2 movementDirection;
    private float control_level { get => player.emotion_level / 100.0f; }

    [SerializeField] private float evasion_speed_coefficient = 40f;
    [SerializeField] private float evasion_cooldown_period = 1f;
    private bool IsAvailable = true;
    public LayerMask myLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        player = GetComponent<Player>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (player.is_dead)
        {
            movementDirection = Vector2.zero;
        }
        else
        {
            movementDirection = move();
        }

        rb.velocity = movementDirection * movementSpeed * useEvasion();
    }

    private Vector2 move()
    {
        float power_control_level = math.exp(control_level);

        Vector2 playerv2 = new Vector2(moveCoefficientPlayer(Input.GetAxis("Horizontal"), power_control_level), moveCoefficientPlayer(Input.GetAxis("Vertical"), power_control_level));

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        List<Vector2> scalars = new List<Vector2>(); 

        foreach (GameObject enemy in enemys)
        {
            scalars.Add(enemy.transform.position - gameObject.transform.position);
        }

        Vector2 badSpiritv = scalars.Max().normalized;

        badSpiritv = new Vector2(moveCoefficientBadSpirit(badSpiritv.x, power_control_level), moveCoefficientBadSpirit(badSpiritv.y, power_control_level));
        badSpiritv.x *= Random.Range(-1f, 9f);
        badSpiritv.y *= Random.Range(-1f, 9f);

        return badSpiritv + playerv2;
    }

    private float useEvasion()
    {
        if(IsAvailable == false || (player.emotion_level < 30))
        {
            return 1;
        }

        if (Input.GetButton("Evasion"))
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

    private float moveCoefficientPlayer(float x, float power_control_level)
    {
        return x * (power_control_level / math.E - (1 - control_level) /math.E) * 2f;
    }

    private float moveCoefficientBadSpirit(float x, float power_control_level)
    {
        return x * ((math.E - power_control_level) / math.E - ((math.E - 2) / math.E) * (1-control_level)) * 0.5f;
    }
}
