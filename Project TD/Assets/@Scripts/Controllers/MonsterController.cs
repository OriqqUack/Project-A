using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
	[SerializeField]
	MonsterStat _stat;

    public Define.Monsters MonsterType { get; protected set; } = Define.Monsters.Unknown; // Despawn 하기위해

    public override void Init()
    {
        MonsterType = Define.Monsters.Unknown; // 고쳐야함

		_stat = gameObject.GetComponent<MonsterStat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
	}

	protected override void UpdateIdle()
	{
		GameObject player = Managers.Game.GetPlayer();
		if (player == null)
			return;

		float distance = (player.transform.position - transform.position).magnitude;
		if (distance <= _stat.ScanRange)
		{
			_lockTarget = player;
			State = Define.State.Moving;
			return;
		}
	}

	protected override void UpdateMoving()
	{
		// 플레이어가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= _stat.AttackRange)
			{
				NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
				nma.SetDestination(transform.position);
				State = Define.State.Skill;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.1f)
		{
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

	void OnHitEvent()
	{
		if (_lockTarget != null)
		{
			// 체력
			PlayerStat targetStat = _lockTarget.GetComponent<PlayerStat>();
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
