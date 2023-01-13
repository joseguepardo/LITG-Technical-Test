using System;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public abstract class Projectile : MonoBehaviour
    {
        public Rigidbody rb;

        private bool _collided;

        protected virtual void Start()
        {
            PlayBasicVFX();
        }

        protected abstract void PlayBasicVFX();

        protected void OnCollisionEnter(Collision collision)
        {
            if (!_collided)
                OnCollision();
            _collided = true;
        }

        protected abstract void OnCollision();

        public virtual void Fire(Vector3 velocity)
        {
            rb.velocity = velocity;
        }
    }
}