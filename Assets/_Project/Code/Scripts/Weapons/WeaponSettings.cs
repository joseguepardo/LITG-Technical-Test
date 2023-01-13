using Sirenix.OdinInspector;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public abstract class WeaponSettings : ScriptableObject
    {
        [InfoBox("Rounds per Second")]
        public float fireRate = 5;
        public float fireDelay => 1 / fireRate;
        public float firePower = 20;
    }
}