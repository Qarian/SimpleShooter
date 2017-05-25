using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;

    public Transform player;
    [SerializeField] float life = 100;

    bool alive = true;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (alive)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent.destination = player.position;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().Hit(Random.Range(5,15));
        }
    }

    public void Hit(float damage)
    {
        life -= damage;
        if (life <= 0)
            StartCoroutine(Die());
    }


    IEnumerator Die()
    {
        alive = false;
        Destroy(gameObject.GetComponent<Collider>());
        Destroy(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>());
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
