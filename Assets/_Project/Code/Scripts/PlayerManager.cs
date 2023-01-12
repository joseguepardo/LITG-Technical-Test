using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class PlayerManager : MonoBehaviour, IPlayer
    {
        public IPlayerAnimator playerAnimator => playerAnimatorComponent;
        public IPlayerController playerController => playerControllerComponent;
        public IPlayerCameraController playerCameraController => playerCameraControllerComponent;
        [SerializeField]
        private PlayerAnimator playerAnimatorComponent;
        [SerializeField]
        private PlayerController playerControllerComponent;
        [SerializeField]
        private PlayerCameraController playerCameraControllerComponent;

        private void Awake()
        {
            GameManager.instance.SetPlayer(this);
        }
    }
}