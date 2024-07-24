using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTest : MonoBehaviour
{
    // 몬스터가 카메라에 보이면 호출되는 메서드
    private void OnBecameVisible()
    {
        ReactToVisibility(true);
    }

    // 몬스터가 카메라에 보이지 않으면 호출되는 메서드
    private void OnBecameInvisible()
    {
        ReactToVisibility(false);
    }

    // 몬스터의 보임/안 보임에 따라 반응하는 메서드
    private void ReactToVisibility(bool isVisible)
    {
        if (isVisible)
        {
            // 몬스터가 카메라에 보일 때의 반응
            Define._Monster0 = true;
            Debug.Log("몬스터가 카메라에 보입니다!");
            // 여기에 몬스터의 반응 코드를 추가하세요.
            // 예: 공격 모드 활성화, 소리 재생 등
        }
    }
}
