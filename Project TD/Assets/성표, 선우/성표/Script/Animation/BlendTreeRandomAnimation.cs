using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeRandomAnimation : StateMachineBehaviour
{
    #region Tooltip
    [Tooltip("블렌드에서 사용하는 파라미터")]
    #endregion Tooltip
    [SerializeField] private string stateParameterName;
    #region Tooltip
    [Tooltip("블렌드 시간")]
    #endregion Tooltip
    [SerializeField] private float blendDuration = 0.5f;
    #region Tooltip
    [Tooltip("각 애니메이션 클립들의 시간")]
    #endregion Tooltip
    [SerializeField] float[] animationClipLengths;

    private AnimatorBlender animBlender; 
    private float currentDelay; // 현재 딜레이 시간  
    private int currentClipIndex; // 현재 재생중인 클립의 인덱스 번호

    /// <summary>
    /// 현재 재생되는 애니메이션의 시간 반환
    /// </summary>
    private void RefreshClip()
    {
        currentClipIndex = Random.Range(0, animationClipLengths.Length);
        currentDelay = animationClipLengths[currentClipIndex];
    }

    private void PlayUpdatedClip(Animator animator)
    {
        RefreshClip();
        animBlender.BlendLerp(animator, stateParameterName, currentClipIndex, blendDuration);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //애니메이터블렌더 찾기
        animBlender = animator.GetComponent<AnimatorBlender>();

        //클립 재조정
        RefreshClip();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay < 0f) { PlayUpdatedClip(animator); }
    }
}


