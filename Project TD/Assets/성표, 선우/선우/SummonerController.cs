using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerController : MonsterController
{
    [SerializeField]
    private GameObject suicideBomberPrefab; // ������ �������� �־����
    private List<GameObject> summonedBombers = new List<GameObject>();
    private int maxBombers = 3;

    public override void Init()
    {
        base.Init();
        // �߰� �ʱ�ȭ �ڵ尡 �ʿ��ϸ� ���⿡ �ۼ�
    }

    protected override void UpdateSkill()
    {
        base.UpdateSkill();
        // ��ų ��� ���� �� ������ ��ȯ ����
        if (summonedBombers.Count < maxBombers)
        {
            StartCoroutine(SummonBomber());
        }
    }

    private IEnumerator SummonBomber()
    {
        // ������ ��ȯ
        GameObject bomber = Instantiate(suicideBomberPrefab, transform.position + transform.forward * 2, Quaternion.identity);
        summonedBombers.Add(bomber);

        // �������� �Ҹ�� �� ����Ʈ���� ����
        bomber.GetComponent<SuicideBomberController>().OnDespawn += () => summonedBombers.Remove(bomber);

        yield return null;
    }
}
