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
    public bool tutorial = true;
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
        if (!(Input.GetButton("Attack1") && IsAvailable && !is_dead)) return;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - circleCollider.transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        circleCollider.transform.rotation = Quaternion.Euler(0f, 0f, rotZ+180);


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

        IsAvailable = true;

        if (colliders.Count > 0 && (null != colliders[0]))
        {
            foreach (Collider2D collider in colliders)
            {
                // Метод получения урона противником;
                collider.gameObject.GetComponent<Enemy>().getDamage(weapon_damage_attack, transform.position);
                Debug.Log("БЬЮ!");
            }
        }
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

    public void getDamage(float damage)
    {
        changeEmotionDamage(damage);
        health -= damage;
    }

    public void getEmotionDamage(float damage)
    {
        emotion_level -= damage;
    }

}
