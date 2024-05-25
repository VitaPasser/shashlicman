using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

/*
 * Робив Віталій
 * 
 TODO: 
 ◦ При проходження рівня буде програна запис Микити
   «ГОООООООООООООООООООООООООООООООООООЛ»

 ◦ Пересування +
 ◦ ухиляння +

 ◦ атаки ближнього бою
    ▪ Буде один Шампур як основна та єдина зброя.
        ▪ Можливо його посилити отримаючі його з противників, але
          зменшує швидкість атаки

 ◦ здоров’я +

    ▪ При нульовому здоров'ї ігрок програє

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

    public bool is_dead { get { return health <= 0; } }

    private void Start()
    {
    }

    void Update()
    {
        changeEmotionTime();
    }

    private void FixedUpdate()
    {
    }

    void changeEmotionTime()
    {
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

}
