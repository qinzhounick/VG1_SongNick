using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Spaceshooter
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        

        public Transform[] spawnPoints;
        public GameObject[] asteroidPrefabs;
        public GameObject explosionPrefab;
        public TMP_Text textScore;
        public TMP_Text textMoney;
        public TMP_Text missileSpeedUpgradeText;
        public TMP_Text bonusUpgradeText;

        public float timeElapsed;
        public float asteroidDelay;
        public float maxAsteroidDelay = 2f;
        public float minAsteroidDelay = 0.2f;
        public float missileSpeed = 2f;
        public float bonusMultiplier = 1f;
        public int score;
        public int money;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            StartCoroutine("AsteroidSpawnTimer");

            score = 0;
            money = 0;
        }

        void Update()
        {
            timeElapsed += Time.deltaTime;

            float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
            asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);

            UpdateDisplay();
        
        }

        void UpdateDisplay()
        {
            textScore.text = score.ToString();
            textMoney.text = money.ToString();
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

        public void EarnPoints(int pointAmount)
        {
            score += Mathf.RoundToInt(pointAmount * bonusMultiplier);
            money += Mathf.RoundToInt(pointAmount * bonusMultiplier);
        }

        public void UpgradeMissileSpeed()
        {
            int cost = Mathf.RoundToInt(25 * missileSpeed);
            if(cost <= money)
            {
                money -= cost;

                missileSpeed += 1f;

                missileSpeedUpgradeText.text = "Missle Speed $" + Mathf.RoundToInt(25 * missileSpeed);
            }
        }

        public void UpgradeBonus()
        {
            int cost = Mathf.RoundToInt(100 * bonusMultiplier);
            if(cost <= money)
            {
                money -= cost;

                bonusMultiplier += 1f;

                bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier);
            }
        }
    }

}

