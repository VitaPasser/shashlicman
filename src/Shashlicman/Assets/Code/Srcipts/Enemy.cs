
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
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

}
