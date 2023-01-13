using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public interface IWeapon
    {
        Transform weaponTransform { get; }
        void OnLookedAt(bool looking);
        void Fire();
        void PickUp();
        void Drop();
    }
}