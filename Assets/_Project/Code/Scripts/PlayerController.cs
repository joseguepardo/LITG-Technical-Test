using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace LifeIsTheGame.TechnicalTest
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private IInputActions _inputActions;

        [BoxGroup("Settings")]
        [SerializeField]
        private CharacterController controller;
        [BoxGroup("Settings")]
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        private CinemachinePOV _cinemachinePov;
        private Transform _cameraT;

        [BoxGroup("Settings")]
        [SerializeField]
        [InlineEditor]
        private PlayerControllerSettingsSO playerControllerSettings;

        [BoxGroup("Settings/Weapons")]
        [SerializeField]
        private LayerMask interactableLayerMask;
        [BoxGroup("Settings/Weapons")]
        [SerializeField]
        private Transform weaponHolderT;

        private IWeapon _weaponPicked, _weaponToPick;
        private Tween _weaponMoveTween, _weaponRotationTween;

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
            // Debug.Log($"#PlayerController# Interacting");
            if ((_weaponMoveTween != null && _weaponMoveTween.IsPlaying()) ||
                (_weaponRotationTween != null && _weaponRotationTween.IsPlaying()))
                return;
            DropWeapon();
            PickUpWeapon();
        }

        private void DropWeapon()
        {
            if (_weaponPicked == null) return;

            // Debug.Log($"#PlayerController# Dropping");
            _weaponPicked.Drop();
            _weaponPicked.weaponTransform.SetParent(null);
            _weaponPicked = null;
        }

        private void PickUpWeapon()
        {
            if (_weaponToPick == null) return;

            // Debug.Log($"#PlayerController# Picking up");
            _weaponToPick.PickUp();
            _weaponPicked = _weaponToPick;
            _weaponToPick = null;

            weaponHolderT.SetParent(Camera.main.transform);
            weaponHolderT.localPosition = new Vector3(0.382f, -0.25f, 0.378f);
            weaponHolderT.localEulerAngles = Vector3.zero;

            var weaponT = _weaponPicked.weaponTransform;
            weaponT.SetParent(weaponHolderT);
            _weaponMoveTween = weaponT.DOLocalMove(Vector3.zero, 0.2f);
            _weaponRotationTween = weaponT.DOLocalRotate(Vector3.zero, 0.2f);
        }

        private void FireWeapon()
        {
            if (_weaponPicked == null) return;

            _weaponPicked.Fire();
            PlayFireFeedback();
        }

        private void PlayFireFeedback()
        {
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
            _inputActions.SubscribeToEventOnFire(FireWeapon);
        }

        private void UnsubscribeToInputActions()
        {
            _inputActions?.UnsubscribeFromEventOnMove(UpdateMovement);
            _inputActions?.UnsubscribeFromEventOnLook(UpdatePlayerLookingForInteraction);
            _inputActions?.UnsubscribeFromEventOnInteract(UpdatePlayerInteraction);
            _inputActions?.UnsubscribeFromEventOnFire(FireWeapon);
        }

        public void EnableController(bool enable)
        {
            if (enable)
            {
                SubscribeToInputActions();
            }
            else
            {
                UnsubscribeToInputActions();
                DropWeapon();
            }
        }
    }
}