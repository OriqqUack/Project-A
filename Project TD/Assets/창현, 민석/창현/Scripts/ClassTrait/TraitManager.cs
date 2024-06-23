using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager
{
    private readonly List<ITrait> activeTraits = new List<ITrait>();

    public void AddTrait(ITrait trait, GameObject target)
    {
        trait.Apply(target);
        activeTraits.Add(trait);
    }

    public void RemoveTrait(ITrait trait, GameObject target)
    {
        trait.Remove(target);
        activeTraits.Remove(trait);
    }

    public void ClearTraits(GameObject target)
    {
        foreach (var trait in activeTraits)
        {
            trait.Remove(target);
        }
        activeTraits.Clear();
    }

    public void ApplyClassTraits(Character characterClass, GameObject target)
    {
        foreach (var trait in characterClass.Traits)
        {
            AddTrait(trait, target);
        }
    }
}
