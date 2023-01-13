using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Windows.Speech;

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
        private LayerMask interactableLayerMask;

        [SerializeField]
        [InlineEditor]
        private PlayerControllerSettingsSO playerControllerSettings;

        private IWeapon _weaponPicked, _weaponToPick;

        private void Start()
        {
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            _inputActions = GameManager.instance.inputActions;
            _cinemachinePov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            _cameraT = Camera.main.transform;
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

        private void UpdatePlayerLookingForInteraction(Vector2 delta)
        {
            var ray = new Ray(_cameraT.position, _cameraT.forward);
            if (Physics.Raycast(ray, out var hit, playerControllerSettings.interactionDistance, interactableLayerMask))
            {
                var weapon = hit.collider.GetComponent<IWeapon>();
                if (weapon != null)
                {
                    if (weapon != _weaponToPick)
                    {
                        _weaponToPick?.OnLookedAt(false);
                        _weaponToPick = weapon;
                        _weaponToPick.OnLookedAt(true);
                    }
                }
            }
            else
            {
                _weaponToPick?.OnLookedAt(false);
                _weaponToPick = null;
            }
        }

        private void UpdatePlayerInteraction()
        {
            if (_weaponToPick == null) return;

            _weaponToPick.PickUp();
            PickUpWeapon(_weaponPicked);
        }

        private void PickUpWeapon(IWeapon weaponToPick)
        {
            _weaponPicked = weaponToPick;
            _weaponToPick = null;
        }

        private void OnDestroy()
        {
            UnsubscribeToInputActions();
        }

        private void SubscribeToInputActions()
        {
            _inputActions.SubscribeToEventOnMove(UpdateMovement);
            _inputActions.SubscribeToEventOnLook(UpdatePlayerLookingForInteraction);
            _inputActions.SubscribeToEventOnInteract(UpdatePlayerInteraction);
        }

        private void UnsubscribeToInputActions()
        {
            _inputActions?.UnsubscribeFromEventOnMove(UpdateMovement);
            _inputActions?.UnsubscribeFromEventOnLook(UpdatePlayerLookingForInteraction);
            _inputActions?.UnsubscribeFromEventOnInteract(UpdatePlayerInteraction);
        }

        public void EnableController(bool enable)
        {
            if (enable) SubscribeToInputActions();
            else UnsubscribeToInputActions();
        }
    }
}