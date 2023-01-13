using System.Collections;
using System.Collections.Generic;
using LifeIsTheGame.TechnicalTest;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class ParabolicProjectile : Projectile
    {
        protected override void PlayBasicVFX()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnCollision()
        {
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}