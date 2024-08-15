using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberStat : MutationStat
{
    public override void OnAttacked(Stat attacker)
    {
        // �⺻ ������ ��� ����
        float damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            // �������� ���� �� SuicideBomberController�� OnAttacked �̺�Ʈ�� ȣ��
            SuicideBomberController bomberController = GetComponent<SuicideBomberController>();
            if (bomberController != null)
            {
                bomberController.OnAttacked();
                OnDead(attacker); // �θ� Ŭ������ ���� ���� ȣ��
            }
        }
    }
}
