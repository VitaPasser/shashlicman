using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleUpper : AbstractThing
{
    [SerializeField] private float added_emotion_points = 40f;
    [field: SerializeField] public float coefficient_speed_change_emotion { get; private set; } = 0.2f;
    [field: SerializeField] public float coefficient_damage_added_morale { get; private set; } = 0.2f;
    public override void changeStatePlayer(Player player)
    {
        added_emotion_points += added_emotion_points * coefficient_damage_added_morale;
        player.addEmotionLevel(added_emotion_points, this);
    }
}
