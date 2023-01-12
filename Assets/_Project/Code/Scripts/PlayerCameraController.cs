using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class PlayerCameraController : MonoBehaviour, IPlayerCameraController
    {
        [SerializeField]
        private CinemachineVirtualCamera fpsCamera, selectionCamera;

        public void SetFPSCamera()
        {
            fpsCamera.gameObject.SetActive(true);
            selectionCamera.gameObject.SetActive(false);
        }

        public void SetSelectionCamera()
        {
            fpsCamera.gameObject.SetActive(false);
            selectionCamera.gameObject.SetActive(true);
        }
    }
}