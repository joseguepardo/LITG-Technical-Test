using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LifeIsTheGame.TechnicalTest
{
    public class ExplosionProjectile : Projectile
    {
        [InlineEditor]
        [SerializeField]
        private ExplosionWeaponSettings explosionWeaponSettings;

        [ShowInInspector]
        [ReadOnly]
        [BoxGroup("Runtime")]
        private bool _isActive;
        [ShowInInspector]
        [ReadOnly]
        [BoxGroup("Runtime")]
        private readonly List<Rigidbody> _rigidbodiesInRange = new List<Rigidbody>();

        [Button(ButtonSizes.Large)]
        private void ActivateFloatingEffectToggle()
        {
            if (_isActive)
            {
                _isActive = false;
                _rigidbodiesInRange.Clear();
            }
            else
            {
                _isActive = true;
                InitializeRigidbodies();
            }
        }

        [Button(ButtonSizes.Large)]
        private void StartExplosions()
        {
            StartCoroutine(StartExplosionsCo());
        }

        private IEnumerator StartExplosionsCo()
        {
            var delay = new WaitForSeconds(explosionWeaponSettings.explosionsDuration / _rigidbodiesInRange.Count);
            while (_rigidbodiesInRange.Count > 0)
            {
                var randomIndex = Random.Range(0, _rigidbodiesInRange.Count);
                var rigidbodyInRange = _rigidbodiesInRange[randomIndex];
                var direction = new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 1), Random.Range(-1, 1)).normalized;
                rigidbodyInRange.AddForce(direction * explosionWeaponSettings.explosionForce, ForceMode.VelocityChange);
                _rigidbodiesInRange.RemoveAt(randomIndex);

                yield return delay;
            }
        }

        private void InitializeRigidbodies()
        {
            var collidersInRange = new Collider[100];
            var numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionWeaponSettings.range,
                collidersInRange, explosionWeaponSettings.layerMask);
            for (var i = 0; i < numberOfColliders; i++)
            {
                var rb = collidersInRange[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    _rigidbodiesInRange.Add(rb);
                }
            }
        }

        private void FixedUpdate()
        {
            UpdateForces();
        }

        private void UpdateForces()
        {
            if (!_isActive)
            {
                return;
            }

            ApplyDirectAttraction();
        }

        private void ApplyDirectAttraction()
        {
            foreach (var rb in _rigidbodiesInRange)
            {
                var direction = (transform.position - rb.position).normalized;
                direction.x = 0;
                direction.z = 0;
                rb.AddForce(direction * explosionWeaponSettings.force);
            }
        }

        private void OnDrawGizmos()
        {
            DrawRangeSphere();
        }

        private void DrawRangeSphere()
        {
            var position = transform.position;
            var rangeColor = _isActive ? Color.green : Color.yellow;
            Gizmos.color = rangeColor;
            Gizmos.DrawWireSphere(position, explosionWeaponSettings.range);
            rangeColor.a = 0.2f;
            Gizmos.color = rangeColor;
            Gizmos.DrawSphere(position, explosionWeaponSettings.range);
        }

        protected override void PlayBasicVFX()
        {
            throw new NotImplementedException();
        }

        protected override void OnCollision()
        {
            StartCoroutine(ProjectileEffectCo());
        }

        private IEnumerator ProjectileEffectCo()
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.detectCollisions = false;
            transform.DOLocalMove(transform.position + new Vector3(0, explosionWeaponSettings.finalProjectileHeight, 0),
                explosionWeaponSettings.heightAnimationDuration).SetEase(Ease.OutQuint);
            transform.DOScale(explosionWeaponSettings.finalProjectileScale,
                    explosionWeaponSettings.scalingAnimationDuration)
                .SetEase(Ease.OutQuint);

            ActivateFloatingEffectToggle();
            yield return new WaitForSeconds(explosionWeaponSettings.explosionsDelay);
            yield return StartExplosionsCo();
            ActivateFloatingEffectToggle();
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutBack).OnComplete(() => Destroy(gameObject));
        }
    }
}