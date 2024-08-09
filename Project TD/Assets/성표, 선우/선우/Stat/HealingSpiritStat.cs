using UnityEngine;

public class HealingSpiritStat : SoulStat
{
    public override void OnAttacked(Stat attacker)
    {
        base.OnAttacked(attacker); // 부모 클래스의 데미지 처리 로직을 그대로 사용

        if (attacker == null)
        {
            Debug.LogError("Attacker is null in HealingSpiritStat.OnAttacked");
            return;
        }

        // 정령이 피해를 입었을 때 도망가는 로직 추가
        HealingSpiritController controller = GetComponent<HealingSpiritController>();
        if (controller != null && Hp > 0 && !_isStunning) // 살아있을 때만 도망감
        {
            // 여기서 로직을 이렇게 수행했을 경우 타워보다 멀리서 플레이어가 때렸다면
            // 플레이어가 타깃으로 잡히기 전이므로 다른 타깃을 기준으로 멀어질 수 있음.
            // 여기서 attacker 기준으로 멀어지도록 로직을 수정할 필요가 있어보임.
            controller.TriggerFlee();
        }
    }
}
