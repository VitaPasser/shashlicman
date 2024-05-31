using UnityEngine;

public class Medkit : AbstractThing
{
    [SerializeField] private float heal_health_points = 30f;
    public override void changeStatePlayer(Player player)
    {
        player.getHeal(heal_health_points);
    }
}
