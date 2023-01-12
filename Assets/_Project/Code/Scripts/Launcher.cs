using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LifeIsTheGame.TechnicalTest
{
    public class Launcher : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return LoadGameCo();
        }

        private IEnumerator LoadGameCo()
        {
            yield return new WaitUntil(CoreComponentsReady);

            Debug.Log($"Core components ready, loading game scene");
            SceneManager.LoadScene(1);
        }

        private bool CoreComponentsReady()
        {
            return GameManager.instance != null;
        }
    }
}