using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberStat : MutationStat
{
    public override void OnAttacked(Stat attacker)
    {
        // 기본 데미지 계산 로직
        float damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            // 자폭병이 죽을 때 SuicideBomberController의 OnAttacked 이벤트를 호출
            SuicideBomberController bomberController = GetComponent<SuicideBomberController>();
            if (bomberController != null)
            {
                bomberController.OnAttacked();
                OnDead(attacker); // 부모 클래스의 죽음 로직 호출
            }
        }
    }
}
