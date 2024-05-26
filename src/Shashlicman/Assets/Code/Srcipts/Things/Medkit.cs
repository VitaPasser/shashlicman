using UnityEngine;

public class Medkit : AbstractThing
{
    [SerializeField] private float heal_health_points = 15f;
    public override void changeStatePlayer(Player player)
    {
        player.getHeal(heal_health_points);
    }
}
