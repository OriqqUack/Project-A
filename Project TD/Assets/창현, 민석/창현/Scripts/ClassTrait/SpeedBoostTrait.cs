using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostTrait : ITrait
{
    public string Name => "Speed Boost";
    public string Description => "Increases movement speed by 20%.";

    public int Level { get; private set; } = 1;
    private readonly float speedMultiplier = 1.2f;

    public void Apply(GameObject target)
    {
        var movement = target.GetComponent<PlayerStat>();
        if (movement != null)
        {
            movement.MoveSpeed *= speedMultiplier;
        }
    }

    public void Remove(GameObject target)
    {
        var movement = target.GetComponent<PlayerStat>();
        if (movement != null)
        {
            movement.MoveSpeed /= speedMultiplier;
        }
    }
}
