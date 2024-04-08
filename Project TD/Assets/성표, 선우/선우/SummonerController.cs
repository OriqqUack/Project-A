using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerController : MonsterController
{

    public void SummonEvent() // �ִϸ����� �̺�Ʈ�� ���� ��ȹ
    {
        GameObject obj = Managers.Game.MonsterSpawn(Define.Monsters.Monster3, "Knight");

        Transform spawnPoint = gameObject.transform;

        Vector3 randomOffset = Random.insideUnitSphere * Random.Range(0, _stat.AttackRange);
        randomOffset.y = 0f;

        Vector3 spawnPosition = spawnPoint.position + randomOffset;

        obj.transform.position = spawnPosition;
    }
}
