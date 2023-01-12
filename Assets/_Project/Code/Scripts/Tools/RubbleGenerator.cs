using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace LifeIsTheGame.TechnicalTest
{
    public class RubbleGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] rubblePrefabs;
        [SerializeField]
        private int numberOfRubble;
        [SerializeField]
        private float rubbleRadius;
        [SerializeField]
        private float minScale, maxScale;

        private List<GameObject> rubbles = new List<GameObject>();

        private void Start()
        {
            GenerateRubble();
        }

        [Button(ButtonSizes.Large)]
        private void GenerateRubble()
        {
            foreach (var rubble in rubbles)
            {
                Destroy(rubble);
            }

            rubbles.Clear();

            // Spawn rubble instances.
            for (var i = 0; i < numberOfRubble; i++)
            {
                var randomPosition = new Vector3(Mathf.Lerp(-1, 1, Random.value), 0, Mathf.Lerp(-1, 1, Random.value)) *
                    rubbleRadius;
                var prefab = rubblePrefabs[Random.Range(0, rubblePrefabs.Length)];
                var rubbleT = Instantiate(prefab, randomPosition, Quaternion.identity).transform;
                rubbleT.localScale *= Mathf.Lerp(minScale, maxScale, Random.value);
                rubbleT.parent = transform;
                rubbles.Add(rubbleT.gameObject);
            }
        }
    }
}