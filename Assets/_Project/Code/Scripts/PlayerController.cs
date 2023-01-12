using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IInputActions _inputActions;

        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        private CinemachinePOV _cinemachinePov;
        private Transform _cameraT;

        [SerializeField]
        [InlineEditor]
        private PlayerControllerSettingsSO playerControllerSettings;

        private void Start()
        {
            InitializeVariables();
            SubscribeToInputActions();
        }

        private void InitializeVariables()
        {
            _inputActions = GameManager.instance.inputActions;
            _cinemachinePov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            _cameraT = Camera.main.transform;
        }

        private void SubscribeToInputActions()
        {
            _inputActions.SubscribeToEventOnMove(UpdateMovement);
        }

        private void UpdateMovement(Vector2 movement)
        {
            var movementV3 = new Vector3(movement.x, 0, movement.y);
            movementV3 = _cameraT.forward * movementV3.z + _cameraT.right * movementV3.x;
            movementV3.y = 0;
            controller.Move(movementV3 * (Time.deltaTime * playerControllerSettings.playerSpeed));
        }

        private void Update()
        {
            UpdateCameraSpeed();
        }

        private void UpdateCameraSpeed()
        {
            _cinemachinePov.m_HorizontalAxis.m_MaxSpeed = playerControllerSettings.cameraSpeed;
            _cinemachinePov.m_VerticalAxis.m_MaxSpeed = playerControllerSettings.cameraSpeed;
        }

        private void OnDestroy()
        {
            UnsubscribeToInputActions();
        }

        private void UnsubscribeToInputActions()
        {
            _inputActions?.UnsubscribeFromEventOnMove(UpdateMovement);
        }
    }
}