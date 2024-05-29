using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Робив Віталій
 * 
 TODO: 
 ◦ При проходження рівня буде програна запис Микити
   «ГОООООООООООООООООООООООООООООООООООЛ»

 ◦ Пересування +
 ◦ ухиляння +

 ◦ атаки ближнього бою +
    ▪ Буде один Шампур як основна та єдина зброя. +
        ▪ Можливо його посилити отримаючі його з противників, але
          зменшує швидкість атаки +-

 ◦ здоров’я +

    ▪ При нульовому здоров'ї ігрок програє +

 ◦ емоційний настрій (придатись журбі або радості) +
     ▪ Зменшення рівня при отримані урону +
     ▪ Зменшення рівня з часом +
     ▪ Зміна рівня ролі керування героям ігроком +
         • При журбі — зменшується +
         • При радості - звеличується +
 ◦ самогубство +
    ▪ Намагання головного героя погубити себе в незалежності від команд +
 
 */

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [field: SerializeField] public float emotion_level { get; private set; } = 100.0f;
    [SerializeField] private float speed_change_emotion = 1.0f;
    [SerializeField] private float min_speed_change_emotion = 0.2f;
    [SerializeField] private bool is_down_emotion_level = true;

    [SerializeField] private float weapon_speed_attack = 0.1f;
    [SerializeField] private float weapon_damage_attack = 10.0f;
    private bool IsAvailable = true;
    private CircleCollider2D circleCollider;

    private List<String> morales;

    public bool is_dead { get { return health <= 0; } }

    public void addEmotionLevel (float add_emotion_level, MoraleUpper morale)
    {
        emotion_level += add_emotion_level;
        morales.Add(morale.name);
        int count_morale = morales.FindAll(obj => obj == morale.name).Count;

        speed_change_emotion += count_morale * morale.coefficient_speed_change_emotion;
        if (speed_change_emotion < min_speed_change_emotion) speed_change_emotion = min_speed_change_emotion;

        emotion_level -= count_morale * morale.coefficient_damage_added_morale;

        if (emotion_level > 100) emotion_level = 100; 
    }

    private void Start()
    {
        circleCollider = gameObject.GetComponentInChildren<CircleCollider2D>();
        morales = new List<String>();
    }

    void Update()
    {
        changeEmotionTime();
        if (Input.GetKeyUp(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyUp(KeyCode.U)) upgradeWeapon(0.1f, 10f);
    }

    private void FixedUpdate()
    {
        attack();
    }

    public void upgradeWeapon(float weapon_coefficient_speed_attack, float weapon_coefficient_damage_attack)
    {
        weapon_speed_attack += weapon_coefficient_speed_attack;
        weapon_damage_attack += weapon_coefficient_damage_attack;
    }

    public void getHeal(float heal_points)
    {
        health += heal_points;
        if (health >= 100)  health = 100;
    }
    void attack()
    {
        if (!Input.GetButton("Attack1") || IsAvailable == false) return;

        var horizontal_attack = Input.GetAxis("Horizontal");
        var vertical_attack = Input.GetAxis("Vertical");

        var offset_value = math.max(math.abs(circleCollider.offset.x), math.abs(circleCollider.offset.y));

        if (math.abs(horizontal_attack) > math.abs(vertical_attack))
        {
            if (horizontal_attack > 0)
            {
                circleCollider.offset = new Vector2(math.abs(offset_value), 0);
            } else if (horizontal_attack < 0)
            {
                circleCollider.offset = new Vector2(-math.abs(offset_value), 0);
            }
        } else if (math.abs(horizontal_attack) < math.abs(vertical_attack))
        {
            if (vertical_attack > 0)
            {
                circleCollider.offset = new Vector2(0, math.abs(offset_value));
            }
            else if (vertical_attack < 0)
            {
                circleCollider.offset = new Vector2(0, -math.abs(offset_value));
            }
        }

        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        circleCollider.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Enemy>() == null);

        StartCoroutine(StartCooldownAttaked(colliders));

        Debug.Log(colliders.Count);
    }

    public IEnumerator StartCooldownAttaked(List<Collider2D> colliders)
    {
        IsAvailable = false;

        yield return new WaitForSeconds(weapon_speed_attack);

        foreach (Collider2D collider in colliders) 
        {
            // Метод получения урона противником;
            collider.gameObject.GetComponent<Enemy>().getDamage(weapon_damage_attack);
            Debug.Log("БЬЮТ!");
        }

        IsAvailable = true;
    }

    void changeEmotionTime()
    {
        if (!is_down_emotion_level) return;
        if (is_dead) return;
        if (emotion_level <= 0)
        {
            emotion_level = 0;
            return;
        }

        emotion_level -= Time.deltaTime * speed_change_emotion;
    }

    void changeEmotionDamage(float damage)
    {
        if (emotion_level <= 0)
        {
            emotion_level = 0;
            return;
        }

        emotion_level -= math.sqrt(damage);
    }

    void getDamage(float damage)
    {
        changeEmotionDamage(damage);
        health -= damage;
    }

    void getEmotionDamage(float damage)
    {
        emotion_level -= damage;
    }

}
