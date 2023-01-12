using System;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public interface IInputActions
    {
        void SubscribeToEventOnMove(Action<Vector2> onMove);
        void UnsubscribeFromEventOnMove(Action<Vector2> onMove);
        
        void SubscribeToEventOnLook(Action<Vector2> onLook);
        void UnsubscribeFromEventOnLook(Action<Vector2> onLook);
        
        void SubscribeToEventOnInteract(Action onInteract);
        void UnsubscribeFromEventOnInteract(Action onInteract);
        
        void SubscribeToEventOnFire(Action onFire);
        void UnsubscribeFromEventOnFire(Action onFire);
    }
}