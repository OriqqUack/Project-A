using UnityEngine;

public class HealingSpiritStat : SoulStat
{
    public override void OnAttacked(Stat attacker)
    {
        base.OnAttacked(attacker); // �θ� Ŭ������ ������ ó�� ������ �״�� ���

        if (attacker == null)
        {
            Debug.LogError("Attacker is null in HealingSpiritStat.OnAttacked");
            return;
        }

        // ������ ���ظ� �Ծ��� �� �������� ���� �߰�
        HealingSpiritController controller = GetComponent<HealingSpiritController>();
        if (controller != null && Hp > 0 && !_isStunning) // ������� ���� ������
        {
            // ���⼭ ������ �̷��� �������� ��� Ÿ������ �ָ��� �÷��̾ ���ȴٸ�
            // �÷��̾ Ÿ������ ������ ���̹Ƿ� �ٸ� Ÿ���� �������� �־��� �� ����.
            // ���⼭ attacker �������� �־������� ������ ������ �ʿ䰡 �־��.
            controller.TriggerFlee();
        }
    }
}
