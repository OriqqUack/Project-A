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

    protected GameObject player;
    protected GameObject rocket;

    protected Coroutine _aggroCoroutine; // 어그로 해제 코루틴
    protected float _aggroDuration = 3.0f; // 어그로 지속 시간
    protected bool _isAggroTimeoutActive = false; // 어그로 타임아웃 활성화 여부

    [SerializeField]
    protected LayerMask targetLayerMask; // 감지할 타깃 레이어 마스크 (직접 설정해서 해야함, 레이어마스크 아직 미설정)

    protected NavMeshAgent _navMeshAgent;

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

    public override Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                    anim.speed = 1.0f;
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.speed = 1.0f;
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.speed = _stat.AttackSpeed;
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.Stun:
                    anim.speed = 1.0f;
                    anim.CrossFade("STUN", 0.1f);
                    break;

            }
        }
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
        if (_stat._isStunning) // 코루틴이 시작일때 true였다가 코루틴이 끝나면 false가 되어야한다
        {
            State = Define.State.Stun;
            return;
        }

        // 플레이어가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            GameObject closestObject = FindClosestObject();

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
                _aggroCoroutine = StartCoroutine(Co_AggroTimeout());
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
        if (_stat._isStunning)
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
        if (!_stat._isStunning)
            State = Define.State.Idle;
    }

    // ScanRange안에 있는 애들을 감지하는 메서드
    public GameObject FindClosestObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _stat.ScanRange, targetLayerMask);
        GameObject closestObject = rocket;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider.gameObject;
            }
        }

        return closestObject;
    }

    // 어그로 타이머 코루틴
    private IEnumerator Co_AggroTimeout()
    {
        _isAggroTimeoutActive = true;
        yield return new WaitForSeconds(_aggroDuration);
        _lockTarget = rocket;
        _isAggroTimeoutActive = false;
    }
    
    // OnAttacked를 통해 경직 상태를 체크하고 적용
    protected virtual void OnHitEvent()
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
