using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class InputManager : MonoBehaviour, IInputActions
    {
        private PlayerInputActions _playerInputActions;
        private event Action<Vector2> _onMove;
        private event Action<Vector2> _onLook;
        private event Action _onInteract;
        private event Action _onFire;

        private void Awake()
        {
            GameManager.instance.SetInputActions(this);
            _playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        private void Update()
        {
            UpdateEventsListeners();
        }

        private void UpdateEventsListeners()
        {
            _onMove?.Invoke(_playerInputActions.Player.Move.ReadValue<Vector2>());
            _onLook?.Invoke(_playerInputActions.Player.Look.ReadValue<Vector2>());
            if (_playerInputActions.Player.Interact.triggered)
                _onInteract?.Invoke();
            if (_playerInputActions.Player.Fire.triggered)
                _onFire?.Invoke();
        }

        public void SubscribeToEventOnMove(Action<Vector2> onMove) => _onMove += onMove;
        public void UnsubscribeFromEventOnMove(Action<Vector2> onMove) => _onMove -= onMove;

        public void SubscribeToEventOnLook(Action<Vector2> onLook) => _onLook += onLook;
        public void UnsubscribeFromEventOnLook(Action<Vector2> onLook) => _onLook -= onLook;

        public void SubscribeToEventOnInteract(Action onInteract) => _onInteract += onInteract;
        public void UnsubscribeFromEventOnInteract(Action onInteract) => _onInteract -= onInteract;

        public void SubscribeToEventOnFire(Action onFire) => _onFire += onFire;
        public void UnsubscribeFromEventOnFire(Action onFire) => _onFire -= onFire;
    }
}