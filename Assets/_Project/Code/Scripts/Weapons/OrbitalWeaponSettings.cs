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
        public float effectDuration = 4;
        public float finalProjectileScale = 3;
        public float finalProjectileHeight = 3;
        public float scalingAnimationDuration = 0.5f;
        public float heightAnimationDuration = 2;
        public LayerMask layerMask;
    }
}