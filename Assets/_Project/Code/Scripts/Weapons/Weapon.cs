using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField]
        [InlineEditor]
        protected WeaponSettings weaponSettings;
        [SerializeField]
        protected Projectile projectilePrefab;
        [SerializeField]
        protected Transform firePointT;
        [SerializeField]
        protected GameObject interactPopUpGO;
        [SerializeField]
        protected Rigidbody rb;

        protected float _lastFireTime = 0;

        public Transform weaponTransform => transform;

        public void OnLookedAt(bool looking)
        {
            interactPopUpGO.SetActive(looking);
        }

        public virtual void Fire()
        {
            if (Time.time <= _lastFireTime + weaponSettings.fireDelay)
                return;

            FireProjectile();
            PlayFiringFeedback();
            _lastFireTime = Time.time;
        }

        protected virtual void FireProjectile()
        {
            var projectile = Instantiate(projectilePrefab, firePointT.position, Quaternion.identity)
                .GetComponent<Projectile>();
            projectile.rb.velocity = firePointT.forward * weaponSettings.firePower;
        }

        protected virtual void PlayFiringFeedback()
        {
        }

        public virtual void PickUp()
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        public virtual void Drop()
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
    }
}