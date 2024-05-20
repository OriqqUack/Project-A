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

    public override void Init()
    {
        MonsterType = Define.Monsters.Unknown; // 고쳐야함

		_stat = gameObject.GetComponent<MonsterStat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        player = Managers.Game.GetPlayer();
        rocket = Managers.Game.GetRocket();
    }

	protected override void UpdateIdle()
	{
		

		if (player == null || rocket == null)
			return;
		// 처음 lockTarget을 rocket으로 설정
		_lockTarget = rocket;
		if(_lockTarget != null)
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
			string[] tags = { "player", "tower" };
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
		// 목표물과의 거리가 스캔범위보다 멀어지면 Idle로 상태 변환
		/* 내비메시의 speed값이 그대로 남아 계속 질질 끌고 오는 버그가 있어 
		 내비메시의 speed값을 0으로 조정해줌 다른 방법이 있을듯 보임....*/
		if (dir.magnitude > _stat.ScanRange)
		{
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.speed = 0f;
            State = Define.State.Idle;
		}
		else
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
