using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonerController : MonsterController
{
    [SerializeField]
    public string _bomberPrefabPath = "Monsters/SuicideBomber"; // 자폭병 프리팹의 경로
    private List<GameObject> _summonedBombers = new List<GameObject>(); // 소환수 개체를 관리하기 위한 리스트
    private float _summonCooldown = 4.0f; // 공격에 의해 디스폰되었을 때의 쿨타임
    private int _maxBombers = 3; // 최대 소환 개체 수
    private bool _canSummon = true; // 소환가능한지 여부를 판단

    public override void Init()
    {
        base.Init();

    }

    // 자폭병 소환 로직, 디스폰시 쿨타임 적용 여부 확인
    private void SummonBomber()
    {
        // 랜덤으로 소환 위치값 적용
        Vector3 summonPosition = GetRandomPositionInAttackRange();

        GameObject bomber = Managers.Game.Spawn(Define.WorldObject.Monster, _bomberPrefabPath);
        // 소환 불발시 나타날 에러코드
        if (bomber == null)
        {
            Debug.LogError($"Failed to load bomber prefab from path: {_bomberPrefabPath}");
            return;
        }

        bomber.transform.position = summonPosition;
        _summonedBombers.Add(bomber);


        var bomberController = bomber.GetComponent<SuicideBomberController>();
        bomberController.OnDespawn += reason =>
        {
            _summonedBombers.Remove(bomber);
            if (reason == SuicideBomberController.DespawnReason.Attacked)
            {
                StartCoroutine(Co_SummonCooldownRoutine());
            }
            
        };
    }

    // 소환사의 공격범위 내에서 랜덤으로 소환되게끔 만듬
    private Vector3 GetRandomPositionInAttackRange()
    {
        float radius = _stat.AttackRange;
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 summonPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + transform.position;

        // Optional: Adjust y position based on terrain height
        // summonPosition.y = Terrain.activeTerrain.SampleHeight(summonPosition);

        return summonPosition;
    }

    private IEnumerator Co_SummonCooldownRoutine()
    {
        _canSummon = false;
        yield return new WaitForSeconds(_summonCooldown);
        _canSummon = true;

        // 넣어야할지 말지 모르겠음
        //if (summonedBombers.Count < 3)
        //{
        //    SummonBomber();
        //}
    }


    // 위에 코드를 넣어야할지말지 OnHitEvent가 호출되는 방식을 같이 생각해봐야할듯
    protected override void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _stat.AttackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }

            // 소환사 공격 시 자폭병 소환
            if (_canSummon && _summonedBombers.Count < _maxBombers)
            {
                SummonBomber();
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}
