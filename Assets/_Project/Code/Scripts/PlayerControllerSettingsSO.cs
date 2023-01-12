using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    [CreateAssetMenu(fileName = "PlayerControllerSettings", menuName = "Project/PlayerControllerSettings", order = 0)]
    public class PlayerControllerSettingsSO : ScriptableObject
    {
        public float playerSpeed = 5f;
        public float cameraSpeed = 150f;
    }
}