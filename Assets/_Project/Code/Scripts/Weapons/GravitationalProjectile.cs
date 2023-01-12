using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class GravitationalProjectile : MonoBehaviour
    {
        [BoxGroup("Settings")]
        [SerializeField]
        private float range;
        [BoxGroup("Settings")]
        [SerializeField]
        private float force;
        [BoxGroup("Settings")]
        [SerializeField]
        private LayerMask layerMask;
        [BoxGroup("Settings")]
        [SerializeField]
        private float offset;

        [ShowInInspector]
        [ReadOnly]
        [BoxGroup("Runtime")]
        private bool _isActive;
        [ShowInInspector]
        [ReadOnly]
        [BoxGroup("Runtime")]
        private readonly List<Rigidbody> _rigidbodiesInRange = new List<Rigidbody>();
        private List<Vector3> _forceVectors = new List<Vector3>();

        [Button(ButtonSizes.Large)]
        private void ActivateToggle()
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

        private void InitializeRigidbodies()
        {
            var collidersInRange = new Collider[100];
            var numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, range, collidersInRange, layerMask);
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

            ApplyOrbitAttraction();
        }

        private void ApplyDirectAttraction()
        {
            foreach (var rb in _rigidbodiesInRange)
            {
                var direction = (transform.position - rb.position).normalized;
                rb.AddForce(direction * force);
            }
        }

        private void ApplyOrbitAttraction()
        {
            _forceVectors.Clear();
            var position = transform.position;
            foreach (var rb in _rigidbodiesInRange)
            {
                var direction = (position - rb.position).normalized;
                var forceDirection = Vector3.Cross(direction, Vector3.up);

                var adjustedDirection = (position - (rb.position + (forceDirection * offset))).normalized;
                var adjustedForceDirection = Vector3.Cross(adjustedDirection, Vector3.up);

                _forceVectors.Add(adjustedForceDirection);
                // Orbital movement.
                rb.velocity = adjustedForceDirection * force;
                // Vertical movement.
                rb.AddForce(new Vector3(0, direction.y, 0) * (force * 10));
            }
        }

        private void OnDrawGizmos()
        {
            DrawRangeSphere();
            DrawForceRays();
        }

        private void DrawRangeSphere()
        {
            var position = transform.position;
            var rangeColor = _isActive ? Color.green : Color.yellow;
            Gizmos.color = rangeColor;
            Gizmos.DrawWireSphere(position, range);
            rangeColor.a = 0.2f;
            Gizmos.color = rangeColor;
            Gizmos.DrawSphere(position, range);
        }

        private void DrawForceRays()
        {
            if (_rigidbodiesInRange.Count != _forceVectors.Count) return;

            Gizmos.color = Color.white;
            for (int i = 0; i < _rigidbodiesInRange.Count; i++)
            {
                Gizmos.DrawRay(_rigidbodiesInRange[i].transform.position, _forceVectors[i]);
            }
        }
    }
}