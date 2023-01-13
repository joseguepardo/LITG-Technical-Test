using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    [CreateAssetMenu(fileName = "TimeWrapWeaponSettings", menuName = "Project/TimeWrapWeaponSettings", order = 3)]
    public class ExplosionWeaponSettings : WeaponSettings
    {
        [Space]
        public float range = 15;
        public float force = 20;
        public float explosionForce = 20;
        public float explosionsDelay = 4;
        public float explosionsDuration = 2;
        public float finalProjectileScale = 3;
        public float finalProjectileHeight = 3;
        public float scalingAnimationDuration = 0.5f;
        public float heightAnimationDuration = 2;
        public LayerMask layerMask;
    }
}