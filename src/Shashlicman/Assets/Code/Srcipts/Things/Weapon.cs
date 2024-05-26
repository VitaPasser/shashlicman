using UnityEngine;

public class Weapon : AbstractThing
{

    [SerializeField] private float weapon_coefficient_speed_attack = 0.1f;
    [SerializeField] private float weapon_coefficient_damage_attack = 10.0f;
    public override void changeStatePlayer(Player player)
    {
        player.upgradeWeapon(weapon_coefficient_speed_attack, weapon_coefficient_damage_attack);
    }
}
