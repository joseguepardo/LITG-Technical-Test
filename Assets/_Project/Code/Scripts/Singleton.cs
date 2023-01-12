using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T instance { get; private set; }

        protected virtual void Awake()
        {
            CreateSingleton();
        }

        private void CreateSingleton()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}