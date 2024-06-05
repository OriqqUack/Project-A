using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeRandomAnimation : StateMachineBehaviour
{
    #region Tooltip
    [Tooltip("���忡�� ����ϴ� �Ķ����")]
    #endregion Tooltip
    [SerializeField] private string stateParameterName;
    #region Tooltip
    [Tooltip("���� �ð�")]
    #endregion Tooltip
    [SerializeField] private float blendDuration = 0.5f;
    #region Tooltip
    [Tooltip("�� �ִϸ��̼� Ŭ������ �ð�")]
    #endregion Tooltip
    [SerializeField] float[] animationClipLengths;

    private AnimatorBlender animBlender; 
    private float currentDelay; // ���� ������ �ð�  
    private int currentClipIndex; // ���� ������� Ŭ���� �ε��� ��ȣ

    /// <summary>
    /// ���� ����Ǵ� �ִϸ��̼��� �ð� ��ȯ
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
        //�ִϸ����ͺ��� ã��
        animBlender = animator.GetComponent<AnimatorBlender>();

        //Ŭ�� ������
        RefreshClip();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay < 0f) { PlayUpdatedClip(animator); }
    }
}


