using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerController : MonsterController
{
    [SerializeField]
    private GameObject suicideBomberPrefab; // 자폭병 프리팹을 넣어야함
    private List<GameObject> summonedBombers = new List<GameObject>();
    private int maxBombers = 3;

    public override void Init()
    {
        base.Init();
        // 추가 초기화 코드가 필요하면 여기에 작성
    }

    protected override void UpdateSkill()
    {
        base.UpdateSkill();
        // 스킬 사용 중일 때 자폭병 소환 로직
        if (summonedBombers.Count < maxBombers)
        {
            StartCoroutine(SummonBomber());
        }
    }

    private IEnumerator SummonBomber()
    {
        // 자폭병 소환
        GameObject bomber = Instantiate(suicideBomberPrefab, transform.position + transform.forward * 2, Quaternion.identity);
        summonedBombers.Add(bomber);

        // 자폭병이 소멸될 때 리스트에서 제거
        bomber.GetComponent<SuicideBomberController>().OnDespawn += () => summonedBombers.Remove(bomber);

        yield return null;
    }
}
