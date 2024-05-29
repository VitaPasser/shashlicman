
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject weaponBuf;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float weapon_speed_attack = 0.1f;
    [SerializeField] private float weapon_damage_attack = 10.0f;
    [SerializeField] private float hype_speed_attack = 0.1f;
    [SerializeField] private float hype_damage_attack = 10.0f;
    public bool is_dead { get { return health <= 0; } }
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        agent.SetDestination(objs[0].transform.position);
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
    }

    public void getDamage(float damage)
    {
        health -= damage;
    }



}
