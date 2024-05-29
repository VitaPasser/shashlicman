
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
//using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject weaponBuf;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float weapon_speed_attack = 0.1f;
    [SerializeField] private float weapon_damage_attack = 10.0f;
    [SerializeField] private float hype_speed_attack = 0.1f;
    [SerializeField] private float hype_damage_attack = 10.0f;
    [SerializeField] private float movementSpeed = 2f;
    private Rigidbody2D rb;
    public bool is_dead { get { return health <= 0; } }
    private bool IsAvailable = true;
    private CircleCollider2D circleCollider;
    //NavMeshAgent agent;
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        circleCollider = gameObject.GetComponentInChildren<CircleCollider2D>();

    }

    private void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        //agent.SetDestination(objs[0].transform.position);

    }

    private Vector2 move()
    {
        Vector2 badSpirit = Vector2.zero;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {

            List<Vector2> scalars = new List<Vector2>();

            foreach (GameObject player in players)
            {
                scalars.Add(player.transform.position - gameObject.transform.position);
            }

            badSpirit = scalars[0];
            foreach (Vector2 player in scalars)
            {
                badSpirit = Vector2.Min(player, badSpirit);
            }
        }

        badSpirit.x *= Random.Range(-1f, 9f);
        badSpirit.y *= Random.Range(-1f, 9f);
        return badSpirit.normalized;
    }

    private void FixedUpdate()
    {
        if (is_dead)
        {
            Vector3 vector = gameObject.transform.position;
            Debug.Log(vector);
            GameObject.Destroy(gameObject);
            Instantiate(weaponBuf, vector, Quaternion.identity);
        }
        rb.velocity = move() * movementSpeed;
    }

    public IEnumerator StartCooldownSpawnWeaponBuf(Vector3 vector)
    {

        yield return new WaitForSeconds(0.5f);
        Instantiate(weaponBuf, vector, Quaternion.identity);
    }

    public void getDamage(float damage)
    {
        health -= damage;
    }

    void doHype()
    {
        if (!(Input.GetButton("Attack1") && IsAvailable)) return;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - circleCollider.transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        circleCollider.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 180);


        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        circleCollider.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Player>() == null);

        StartCoroutine(StartCooldownHype(colliders));

        Debug.Log(colliders.Count);
    }

    public IEnumerator StartCooldownHype(List<Collider2D> colliders)
    {
        IsAvailable = false;

        yield return new WaitForSeconds(hype_speed_attack);

        if (colliders.Count > 0 && (null != colliders[0]))
        {
            foreach (Collider2D collider in colliders)
            {
                // Метод получения урона противником;
                collider.gameObject.GetComponent<Enemy>().getDamage(weapon_damage_attack);
                Debug.Log("БЬЮТ!");
            }
        }

        IsAvailable = true;
    }


}
