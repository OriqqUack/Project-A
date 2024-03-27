using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningAbility
{

    public void SummonMonster(GameObject summonerMonster) // ��ȯ���� ������ �Ű������� ����
    {
                                 
        if (summonerMonster != null)
        {
            Transform spawnPoint = summonerMonster.transform; // ��ġ�� ������ spawnPoint�� ����
            GameObject summonedMonster = Managers.Game.MonsterSpawn(Define.Monsters.Unknown, "ChestMonsterPBRDefault", spawnPoint);

        }
        else
        {
            Debug.LogWarning("Summon Prefab is not assigned!");
        }
    }

}
