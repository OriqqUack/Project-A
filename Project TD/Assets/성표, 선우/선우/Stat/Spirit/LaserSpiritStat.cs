using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpiritStat : SpiritStat
{
    // 추가적인 스탯이 필요할 경우 선언 가능
    // 예: 레이저 기본 데미지, 최대 데미지 등

    protected override void Start()
    {
        base.Start();
        // LaserSpiritStat에 필요한 추가 초기화 작업이 있다면 이곳에 추가
    }

    // 공격받았을 때의 처리 (레이저 공격 초기화)
    public override void OnAttacked(Stat attacker)
    {
        base.OnAttacked(attacker);
        // 필요한 경우 레이저 공격 초기화 로직을 여기 추가 가능
    }
}
