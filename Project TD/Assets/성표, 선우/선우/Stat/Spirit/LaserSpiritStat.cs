using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpiritStat : SpiritStat
{
    // �߰����� ������ �ʿ��� ��� ���� ����
    // ��: ������ �⺻ ������, �ִ� ������ ��

    protected override void Start()
    {
        base.Start();
        // LaserSpiritStat�� �ʿ��� �߰� �ʱ�ȭ �۾��� �ִٸ� �̰��� �߰�
    }

    // ���ݹ޾��� ���� ó�� (������ ���� �ʱ�ȭ)
    public override void OnAttacked(Stat attacker)
    {
        base.OnAttacked(attacker);
        // �ʿ��� ��� ������ ���� �ʱ�ȭ ������ ���� �߰� ����
    }
}
