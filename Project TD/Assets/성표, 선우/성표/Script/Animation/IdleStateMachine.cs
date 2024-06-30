using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class IdleStateMachine : StateMachineBehaviour
{
    private int currentClipIndex;
    private float[] animationClipLengths;
    private float currentDelay;

    private Animator animator;

    /// <summary>
    /// 현재 재생되는 애니메이션의 시간 반환
    /// </summary>
    private void RefreshClip()
    {
        currentClipIndex = Random.Range(0, animationClipLengths.Length);
        currentDelay = animationClipLengths[currentClipIndex];
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentDelay -= Time.deltaTime;
    }
}
