using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    [CreateAssetMenu(fileName = "OrbitalWeaponSettings", menuName = "Project/OrbitalWeaponSettings", order = 2)]
    public class OrbitalWeaponSettings : WeaponSettings
    {
        [Space]
        public float range = 15;
        public float force = 20;
        public float offset = 0.2f;
        public LayerMask layerMask;
    }
}