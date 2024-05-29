using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeDestroy = 3f;
    public float speed = 3f;
    public float damage = 5f;
    public Vector2 direction;
    private Rigidbody2D rb;
    [SerializeField] private Enemy enemy;

    void Start()
    {
        //gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.position, direction);
        rb = GetComponent<Rigidbody2D>();

        damage = enemy.weapon_damage_attack;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        direction = players[0].transform.position;
        Vector3 diference = new Vector3(direction.x, direction.y, 0) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + 270);

        rb.velocity = transform.up * speed;
        Invoke("DestroyBullet", timeDestroy);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;
        
        player.getDamage(damage);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}