using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    [CreateAssetMenu(fileName = "TimeWrapWeaponSettings", menuName = "Project/TimeWrapWeaponSettings", order = 3)]
    public class TimeWrapWeaponSettings : WeaponSettings
    {
        [Space]
        public float timeModifier = 0.5f;
    }
}