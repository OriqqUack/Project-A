using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationController : MonsterController
{
    protected new int maxStunCount = 4; // �ִ� ���� Ƚ��

    // ������ �� �� �ִ� ���ɼ��� �����
    protected override void OnHitEvent()
    {
        base.OnHitEvent(); // �⺻ ���� ������ ȣ��
    }

    protected override IEnumerator Stun()
    {
        return base.Stun(); // �⺻ ���� �ڷ�ƾ ȣ��
    }
}
