using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    [SerializeField]
    protected MonsterStat _stat;

    GameObject player;
    GameObject rocket;

    private Coroutine _aggroCoroutine; // 어그로 해제 코루틴
    private float _aggroDuration = 3.0f; // 어그로 지속 시간
    private bool _isAggroTimeoutActive = false; // 어그로 타임아웃 활성화 여부

    private bool _isStunning = false; // 경직 상태걸렸었는지 여부
    private bool _isStunned = false; // 경직중인지 여부
    private float _stunDuration = 3.0f; // 경직 지속 시간
    private Coroutine _stunCoroutine; // 경직 상태 코루틴

    private NavMeshAgent _navMeshAgent;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;

        rocket = GameObject.Find("rocket");
        _stat = gameObject.GetComponent<MonsterStat>();
        _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();


        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        player = Managers.Game.GetPlayer();
    }

    protected override void UpdateIdle()
    {
        // 처음 lockTarget을 rocket으로 설정
        _lockTarget = rocket;

        if (player == null || rocket == null)
            return;

        if (_lockTarget != null)
            State = Define.State.Moving;

        //float distance = (player.transform.position - transform.position).magnitude;
        //if (distance <= _stat.ScanRange)
        //{
        //	_lockTarget = player;
        //	State = Define.State.Moving;
        //	return;
        //}
    }

    protected override void UpdateMoving()
    {
        // 경직 상태일 때 이동하지 않음
        if (_isStunning) // 코루틴이 시작일때 true였다가 코루틴이 끝나면 false가 되어야한다
        {
            State = Define.State.Stun;
            return;
        }

        // 플레이어가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            // player태그와 tower태그를 감지
            string[] tags = { "Player", "Tower" };
            // 스캔 범위에 들어오는 오브젝트의 태그를 확인해 lockTarget 변경
            GameObject closestObject = FindClosestObject(tags);

            if (closestObject != rocket && closestObject != _lockTarget)
            {
                _lockTarget = closestObject;
                if (_aggroCoroutine != null)
                {
                    StopCoroutine(_aggroCoroutine);
                    _isAggroTimeoutActive = false;
                }
            }

            _destPos = _lockTarget.transform.position;
            float distanceAttack = (_destPos - transform.position).magnitude;
            if (distanceAttack <= _stat.AttackRange)
            {
                _navMeshAgent.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
            else if (closestObject == rocket && !_isAggroTimeoutActive)
            {
                // 타겟이 사정거리 밖으로 나갔을 때 어그로 해제 타이머 작동
                _aggroCoroutine = StartCoroutine(AggroTimeout());
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;

        // 코루틴을 사용하여 딜레이를 줌
        

        if (_lockTarget != null)
        {
            _navMeshAgent.SetDestination(_destPos);
            _navMeshAgent.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        // 경직 상태일 때 스킬 사용하지 않음
        if (_isStunning)
        {
            State = Define.State.Stun;
            return;
        }

        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    protected override void UpdateStun()
    {
        _navMeshAgent.speed = 0;
        
        // 스턴중일때 상태가 바뀌지 않게 함
        if (!_isStunning)
            State = Define.State.Idle;
    }

    // ScanRange안에 있는 애들을 감지하는 메서드
    public GameObject FindClosestObject(string[] tags)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _stat.ScanRange);
        GameObject closestObject = rocket;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (Array.Exists(tags, tag => collider.CompareTag(tag)))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = collider.gameObject;
                }
            }
        }

        return closestObject;
    }

    // 어그로 타이머 코루틴
    private IEnumerator AggroTimeout()
    {
        _isAggroTimeoutActive = true;
        yield return new WaitForSeconds(_aggroDuration);
        _lockTarget = rocket;
        _isAggroTimeoutActive = false;
    }

    // 경직 상태 코루틴(한번만 걸리면 true로 바뀌어서 안걸림)
    // 스턴중일때 false였다가 코루틴이 끝나면 true로 바뀌는 변수가 필요 = _isStunning.
    private IEnumerator Stun()
    {
        _isStunning = true;
        _isStunned = true;
        yield return new WaitForSeconds(_stunDuration);
        _isStunning = false;
    }

    // HitEvent를 통해 경직 상태를 체크하고 적용
    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            // 체력
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _stat.AttackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;

                // 경직 상태 체크 및 적용
                if (_stat.Hp <= _stat.MaxHp / 3 && !_isStunned)
                {
                    if (_stunCoroutine != null)
                        StopCoroutine(_stunCoroutine);
                    _stunCoroutine = StartCoroutine(Stun());
                }
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}
