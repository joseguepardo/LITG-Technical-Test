using System;
using System.Collections;
using System.Collections.Generic;
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
        private float playerSpeed = 2.0f;
        private Vector3 _playerVelocity;

        private void Start()
        {
            InitializeInputActions();
        }

        private void InitializeInputActions()
        {
            _inputActions = GameManager.instance.inputActions;
            _inputActions.SubscribeToEventOnMove(UpdateMovement);
        }

        private void UpdateMovement(Vector2 movement)
        {
            var movementV3 = new Vector3(movement.x, 0, movement.y);
            controller.Move(movementV3 * (Time.deltaTime * playerSpeed));
        }

        private void OnDestroy()
        {
            if (_inputActions == null) return;

            _inputActions.UnsubscribeFromEventOnMove(UpdateMovement);
        }
    }
}