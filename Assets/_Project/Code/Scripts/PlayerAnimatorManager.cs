using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerAnimatorManager : MonoBehaviour, IPlayerAnimator
{
    [SerializeField]
    private Animator animator;
    private const string AnimatorTriggerName1 = "Dance_1";
    private const string AnimatorTriggerName2 = "Dance_2";
    private const string AnimatorTriggerName3 = "Dance_3";
    private readonly int _animatorTriggerHash1 = Animator.StringToHash(AnimatorTriggerName1);
    private readonly int _animatorTriggerHash2 = Animator.StringToHash(AnimatorTriggerName2);
    private readonly int _animatorTriggerHash3 = Animator.StringToHash(AnimatorTriggerName3);

    public enum AnimationState
    {
        HouseDancing,
        MacarenaDance,
        WaveHipHopDance
    }

    [Button]
    public void SetAnimation(AnimationState animationState)
    {
        var triggerHash = animationState switch
        {
            AnimationState.HouseDancing => _animatorTriggerHash1,
            AnimationState.MacarenaDance => _animatorTriggerHash2,
            _ => _animatorTriggerHash3
        };
        animator.SetTrigger(triggerHash);
    }
}