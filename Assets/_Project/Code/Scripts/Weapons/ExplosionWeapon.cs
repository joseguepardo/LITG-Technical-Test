using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class ExplosionWeapon : Weapon
    {
        private Projectile _projectile;

        public override void Fire()
        {
            if (Time.time <= _lastFireTime + weaponSettings.fireDelay || _projectile != null)
                return;

            FireProjectile();
            PlayFiringFeedback();
            _lastFireTime = Time.time;
        }
    }
}