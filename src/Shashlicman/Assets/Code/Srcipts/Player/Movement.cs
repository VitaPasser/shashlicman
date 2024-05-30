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
    [SerializeField] private LayerMask myLayerMask;
    [SerializeField] private AudioClip[] evasion_audioClips; 
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
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

        Vector2 badSpirit = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys.Length > 0)
        {

            List<Vector2> scalars = new List<Vector2>(); 

            foreach (GameObject enemy in enemys)
            {
                scalars.Add(enemy.transform.position - gameObject.transform.position);
            }

            badSpirit = scalars[0];
            foreach (Vector2 enemy in scalars)
            {
                badSpirit = Vector2.Min(enemy, badSpirit);
            }
        }

        badSpirit.x *= Random.Range(-1f, 9f);
        badSpirit.y *= Random.Range(-1f, 9f);
        badSpirit.Normalize();

        badSpirit = new Vector2(moveCoefficientBadSpirit(badSpirit.x, power_control_level), moveCoefficientBadSpirit(badSpirit.y, power_control_level));

        return badSpirit + playerv2;
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

        int evasion_sound_number = UnityEngine.Random.Range(0, evasion_audioClips.Length - 1);
        audioSource.clip = evasion_audioClips[evasion_sound_number];
        audioSource.Play();

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
