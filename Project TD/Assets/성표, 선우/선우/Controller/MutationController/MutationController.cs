using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationController : MonsterController
{
    

    // 재정의 할 수 있는 가능성을 열어둠
    protected override void OnHitEvent()
    {
        base.OnHitEvent(); // 기본 경직 로직을 호출
    }
}
