
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject weaponBuf;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float weapon_speed_attack = 0.1f;
    [field: SerializeField] public float weapon_damage_attack { get; private set; } = 10.0f;
    [SerializeField] private float hype_speed_attack = 0.1f;
    [SerializeField] private float hype_emotional_damage_attack = 5.0f;
    [SerializeField] private float hype_health_damage_attack = 2.0f;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float movementSpeed_ = 2f;
    [SerializeField] private Vector2 fixStuck = new Vector2(0, 0);
    [SerializeField] private bool isShoot = true;
    private Rigidbody2D rb;
    public bool is_dead { get { return health <= 0; } }
    private bool IsAvailableHype = true;
    private bool IsAvailableShoot = true;
    private CircleCollider2D rangeAttack;
    private CircleCollider2D rangeHypeSelect;
    private Animator anim;
    private enum UseTool
    {
        gun = 1,
        hype = 2
    }
    private UseTool useTool = UseTool.gun;

    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private AudioClip[] hit_audioClips;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rangeAttack = gameObject.GetComponentsInChildren<CircleCollider2D>()[0];
        rangeHypeSelect = gameObject.GetComponentsInChildren<CircleCollider2D>()[1];
        movementSpeed_ = movementSpeed;
        anim = GetComponentInChildren<Animator>();

    }

    private void FixedUpdate()
    {
        if(transform.position.x > 7)
        {
            transform.position = new Vector3(7, transform.position.y, transform.position.z);
        } else if (transform.position.x < -7)
        {
            transform.position = new Vector3(-7, transform.position.y, transform.position.z);
        }

        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }

        if (is_dead)
        {
            Vector3 vector = gameObject.transform.position;
            Debug.Log(vector);

            GameObject gaudiosource_ = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            AudioSource audiosource = gaudiosource_.GetComponent<AudioSource>();
            int sound_number = UnityEngine.Random.Range(0, hit_audioClips.Length - 1);
            audiosource.clip = hit_audioClips[sound_number];
            audiosource.Play();

            GameObject.Destroy(gameObject);
            Instantiate(weaponBuf, vector, Quaternion.identity);
        } else
        {
            rb.velocity = move() * movementSpeed_ + fixStuck * movementSpeed_;

            GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

            if (Mathf.Abs(transform.position.y - objs[0].transform.position.y) > 2.0f)
            {
                if (transform.position.y > objs[0].transform.position.y)
                {
                    anim.SetBool("isVverh", false);
                    anim.SetBool("isVniz", true);
                    anim.SetBool("isLeft", false);
                    anim.SetBool("isRight", false);
                }
                else
                {
                    anim.SetBool("isVverh", true);
                    anim.SetBool("isVniz", false);
                    anim.SetBool("isRight", false);
                    anim.SetBool("isLeft", false);
                }
            }
            else
            {   
                if(transform.position.x > objs[0].transform.position.x)
                {
                    anim.SetBool("isLeft", true);
                    anim.SetBool("isRight", false);
                    anim.SetBool("isVverh", false);
                    anim.SetBool("isVniz", false);
                }
                else
                {
                    anim.SetBool("isLeft", false);
                    anim.SetBool("isRight", true);
                    anim.SetBool("isVverh", false);
                    anim.SetBool("isVniz", false);
                }
                        
            }

            selectingHype();
            selectingShoot();
        }
    }
    
    /*private void isDoHype()
    {
        if (IsAvailableHype) return;
        rangeAttack.transform.localPosition;

    }*/

    private void selectingShoot()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (useTool == UseTool.gun && isShoot)
        {
            doShoot(objs[0].GetComponent<Player>());
        }

    }
    private void doShoot(Player player)
    {
        if (!IsAvailableShoot) return;

        StartCoroutine(StartCooldownShoot(player));
    }

    public IEnumerator StartCooldownShoot(Player player)
    {
        IsAvailableShoot = false;

        yield return new WaitForSeconds(weapon_speed_attack);

        IsAvailableShoot = true;

        if (player != null) 
        { 
            // Метод получения урона противником;
            Instantiate(bullet, gameObject.transform.position, player.gameObject.transform.rotation);
            Debug.Log("Стреляют!");
        }
    }

    private void selectingHype()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        rangeHypeSelect.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Player>() == null);

        int use = Random.Range(0, 10);

        if (colliders.Count > 0 && (use == 1))
        {
            useTool = UseTool.gun;
            movementSpeed_ = movementSpeed;
        } else if (colliders.Count > 0)
        {
            useTool = UseTool.hype;
            movementSpeed_ = movementSpeed / 2;
            doHype();
        } else
        {
            useTool = UseTool.gun;
            movementSpeed_ = movementSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null) { fixStuck = Vector2.zero; return; }
        fixStuck = new Vector2(Random.Range(-9, 9), Random.Range(-9, 9));
    }

    private Vector2 move()
    {
        Vector2 badSpirit = nearTarget();

        badSpirit.x *= Random.Range(-2f, 18f);
        badSpirit.y *= Random.Range(-2f, 18f);
        return badSpirit.normalized;
    }

    public IEnumerator StartCooldownSpawnWeaponBuf(Vector3 vector)
    {

        yield return new WaitForSeconds(0.5f);
        Instantiate(weaponBuf, vector, Quaternion.identity);
    }

    public void getDamage(float damage, Vector2 position)
    {   
        health -= damage;
        transform.position = (transform.position + (transform.position - new Vector3(position.x, position.y, 0)));
    }

    private Vector2 nearTarget()
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
        return badSpirit;
    }

    void doHype()
    {
        if (!IsAvailableHype) return;

        transform.GetChild(3).gameObject.SetActive(true);

        Vector2 near_target = nearTarget().normalized;
        Vector3 difference = new Vector3(near_target.x, near_target.y, 0) - rangeAttack.transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rangeAttack.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 180);


        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        rangeAttack.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Player>() == null);

        StartCoroutine(StartCooldownHype(colliders));

        Debug.Log(colliders.Count);
    }

    public IEnumerator StartCooldownHype(List<Collider2D> colliders)
    {
        IsAvailableHype = false;

        yield return new WaitForSeconds(hype_speed_attack);

        transform.GetChild(3).gameObject.SetActive(false);

        IsAvailableHype = true;

        if (colliders.Count > 0 && (null != colliders[0]))
        {
            foreach (Collider2D collider in colliders)
            {
                // Метод получения урона противником;
                collider.gameObject.GetComponent<Player>().getEmotionDamage(hype_emotional_damage_attack);
                collider.gameObject.GetComponent<Player>().getDamage(hype_health_damage_attack);
                Debug.Log("БЬЮТ!");
            }
        }
    }


}
