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

    public Define.Monsters MonsterType { get; protected set; } = Define.Monsters.Unknown; // Despawn 하기위해

    GameObject player;
    GameObject rocket;

    private float aggroLossDelay = 3f;
    private Coroutine _aggroCoroutine;
    public override void Init()
    {
        MonsterType = Define.Monsters.Unknown; // 고쳐야함

        rocket = GameObject.Find("rocket");
        _stat = gameObject.GetComponent<MonsterStat>();

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
        // 플레이어가 내 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            // player태그와 tower태그를 감지
            string[] tags = { "Player", "Tower" };
            // 스캔 범위에 들어오는 오브젝트의 태그를 확인해 lockTarget변경
            _lockTarget = FindClosestObject(tags);

            _destPos = _lockTarget.transform.position;
            float distanceAttack = (_destPos - transform.position).magnitude;
            if (distanceAttack <= _stat.AttackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;

        // 코루틴을 사용하여 딜레이를 줌
        if (_lockTarget != rocket)
        {
            
            if (dir.magnitude > _stat.ScanRange)
            {
                if (_aggroCoroutine != null)
                {
                    StopCoroutine(_aggroCoroutine);
                    _aggroCoroutine = null;
                }
                else
                {
                    _aggroCoroutine = StartCoroutine(AggroLossDelay());
                }
            }

        }

        if (_lockTarget != null)
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    // ScanRange안에 있는 애들을 감지하는 메서드
    public GameObject FindClosestObject(string[] tags)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _stat.ScanRange);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (Array.Exists(tags, tag => collider.CompareTag(tag)))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestObject = collider.gameObject;
                    closestDistance = distance;
                }
            }
        }

        return closestObject;
    }

    // 어그로 타임 3초 코루틴
    IEnumerator AggroLossDelay()
    {
        yield return new WaitForSeconds(aggroLossDelay);
        _lockTarget = rocket;
    }

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
