using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAnimator
{
    void SetAnimation(PlayerAnimatorManager.AnimationState animationState);
}