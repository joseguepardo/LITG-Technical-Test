using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class OrbitalWeapon : Weapon
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