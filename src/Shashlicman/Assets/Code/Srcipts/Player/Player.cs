using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
          зменшує швидкість атаки

 ◦ здоров’я +

    ▪ При нульовому здоров'ї ігрок програє +

 ◦ емоційний настрій (придатись журбі або радості) +
     ▪ Зменшення рівня при отримані урону +
     ▪ Зменшення рівня з часом +
     ▪ Зміна рівня ролі керування героям ігроком +
         • При журбі — зменшується +
         • При радості - звеличується +
 ◦ самогубство +-
    ▪ Намагання головного героя погубити себе в незалежності від команд +-
 
 */

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [field: SerializeField] public float emotion_level { get; private set; } = 100.0f;
    [SerializeField] private float speed_change_emotion = 1.0f;
    [SerializeField] private bool is_down_emotion_level = true;

    [SerializeField] private float weapon_level_upgrade = 1.0f;
    [SerializeField] private float weapon_speed_attack = 0.1f;
    [SerializeField] private float weapon_damage_attack = 10.0f;

    public bool is_dead { get { return health <= 0; } }

    private void Start()
    {
    }

    void Update()
    {
        changeEmotionTime();
        if (Input.GetKeyUp(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        attack();
    }



    void attack()
    {
        if (!Input.GetButton("Attack1")) return;

        CircleCollider2D cc = this.gameObject.transform.GetChild(1).GetComponent<CircleCollider2D>();

        var horizontal_attack = Input.GetAxis("Horizontal");
        var vertical_attack = Input.GetAxis("Vertical");

        var offset_value = math.max(math.abs(cc.offset.x), math.abs(cc.offset.y));

        if (math.abs(horizontal_attack) > math.abs(vertical_attack))
        {
            if (horizontal_attack > 0)
            {
                cc.offset = new Vector2(math.abs(offset_value), 0);
            } else if (horizontal_attack < 0)
            {
                cc.offset = new Vector2(-math.abs(offset_value), 0);
            }
        } else if (math.abs(horizontal_attack) < math.abs(vertical_attack))
        {
            if (vertical_attack > 0)
            {
                cc.offset = new Vector2(0, math.abs(offset_value));
            }
            else if (vertical_attack < 0)
            {
                cc.offset = new Vector2(0, -math.abs(offset_value));
            }
        }

        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        cc.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Enemy>() == null);

        // Метод получения урона противником

        Debug.Log(colliders.Count);
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
