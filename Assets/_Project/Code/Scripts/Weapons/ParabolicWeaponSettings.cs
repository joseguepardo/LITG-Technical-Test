using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    [CreateAssetMenu(fileName = "ParabolicWeaponSettings", menuName = "Project/ParabolicWeaponSettings", order = 1)]
    public class ParabolicWeaponSettings : WeaponSettings
    {
        [Space]
        public float gravity = 1;
    }
}