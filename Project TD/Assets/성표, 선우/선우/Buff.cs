using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    MonsterStat _stat;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    private void Start()
    {
        _stat = gameObject.GetComponent<MonsterStat>();
    }

    public void ApplyBuff(StatusEffect effect)
    {
        effect.ApplyEffect(_stat);
        activeEffects.Add(effect);
        StartCoroutine(RemoveEffectAfterDuration(effect));
    }

    private IEnumerator RemoveEffectAfterDuration(StatusEffect effect)
    {
        yield return new WaitForSeconds(effect.Duration);
        effect.RemoveEffect(_stat);
        activeEffects.Remove(effect);
    }

    public void RemoveAllEffects()
    {
        foreach (StatusEffect effect in activeEffects)
        {
            effect.RemoveEffect(_stat);
        }
        activeEffects.Clear();
    }
}