using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerController : MonsterController
{
    [SerializeField]
    public string bomberPrefabPath = "Monsters/SuicideBomber"; // ������ �������� ���
    private List<GameObject> summonedBombers = new List<GameObject>(); // ��ȯ�� ��ü�� �����ϱ� ���� ����Ʈ
    private float summonCooldown = 4.0f; // ���ݿ� ���� �����Ǿ��� ���� ��Ÿ��
    private int maxBombers = 3; // �ִ� ��ȯ ��ü ��
    private bool canSummon = true; // ��ȯ�������� ���θ� �Ǵ�

    public override void Init()
    {
        base.Init();

    }

    // ������ ��ȯ ����, ������ ��Ÿ�� ���� ���� Ȯ��
    private void SummonBomber()
    {
        // �������� ��ȯ ��ġ�� ����
        Vector3 summonPosition = GetRandomPositionInAttackRange();

        GameObject bomber = Managers.Resource.Instantiate(bomberPrefabPath);
        // ��ȯ �ҹ߽� ��Ÿ�� �����ڵ�
        if (bomber == null)
        {
            Debug.LogError($"Failed to load bomber prefab from path: {bomberPrefabPath}");
            return;
        }

        bomber.transform.position = summonPosition;
        summonedBombers.Add(bomber);


        var bomberController = bomber.GetComponent<SuicideBomberController>();
        bomberController.OnDespawn += reason =>
        {
            summonedBombers.Remove(bomber);
            if (reason == SuicideBomberController.DespawnReason.Attacked)
            {
                StartCoroutine(SummonCooldownRoutine());
            }
            else if (reason == SuicideBomberController.DespawnReason.SelfDestruct)
            {
                SummonBomber();
            }
        };
    }

    // ��ȯ���� ���ݹ��� ������ �������� ��ȯ�ǰԲ� ����
    private Vector3 GetRandomPositionInAttackRange()
    {
        float radius = _stat.AttackRange;
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 summonPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + transform.position;

        // Optional: Adjust y position based on terrain height
        // summonPosition.y = Terrain.activeTerrain.SampleHeight(summonPosition);

        return summonPosition;
    }

    private IEnumerator SummonCooldownRoutine()
    {
        canSummon = false;
        yield return new WaitForSeconds(summonCooldown);
        canSummon = true;

        // �־������ ���� �𸣰���
        //if (summonedBombers.Count < 3)
        //{
        //    SummonBomber();
        //}
    }


    // ���� �ڵ带 �־���������� OnHitEvent�� ȣ��Ǵ� ����� ���� �����غ����ҵ�
    protected override void OnHitEvent()
    {
        // ��ȯ�� ���� �� ������ ��ȯ
        if (canSummon && summonedBombers.Count < maxBombers)
        {
            SummonBomber();
        }
    }
}
