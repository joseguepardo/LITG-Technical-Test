using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class ParabolicWeapon : Weapon
    {
        protected override void FireProjectile()
        {
            var projectile = Instantiate(projectilePrefab, firePointT.position, Quaternion.identity)
                .GetComponent<ParabolicProjectile>();
            projectile.rb.velocity = firePointT.forward * weaponSettings.firePower;
            // projectileRigidbody.AddForce(Physics.gravity * projectileRigidbody.mass * 0.5f, ForceMode.Acceleration);
        }
    }
}