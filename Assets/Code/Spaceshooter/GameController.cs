using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Spaceshooter
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        

        public Transform[] spawnPoints;
        public GameObject[] asteroidPrefabs;
        public GameObject explosionPrefab;

        public float timeElapsed;
        public float asteroidDelay;
        public float maxAsteroidDelay = 2f;
        public float minAsteroidDelay = 0.2f;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            StartCoroutine("AsteroidSpawnTimer");
        }

        void Update()
        {
            timeElapsed += Time.deltaTime;

            float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
            asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);
        }

        void SpawnAsteroid()
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
            int randomAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
            GameObject randomAsteroidPrefab = asteroidPrefabs[randomAsteroidIndex];

            Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
        }

        IEnumerator AsteroidSpawnTimer()
        {
            yield return new WaitForSeconds(asteroidDelay);

            SpawnAsteroid();
            StartCoroutine("AsteroidSpawnTimer");
        }
    }

}

