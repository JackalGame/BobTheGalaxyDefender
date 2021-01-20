using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] float firstWaveAsteroidMinSpawnTime = 0.3f;
    [SerializeField] float firstWaveAsteroidMaxSpawnTime = 1.5f;
    [SerializeField] float secondWaveAsteroidMinSpawnTime = 0.3f;
    [SerializeField] float secondWaveAsteroidMaxSpawnTime = 1.5f;
    [SerializeField] float thirdWaveAsteroidMinSpawnTime = 0.3f;
    [SerializeField] float thirdWaveAsteroidMaxSpawnTime = 1.5f;

    [Header("Enemy Wave")]
    [SerializeField] float enemyMinSpawnTime = 5f;
    [SerializeField] float enemyMaxSpawnTime = 10f;
    [SerializeField] int firstScoreCheckpoint = 1000;
    [SerializeField] int secondScoreCheckpoint = 2000;
    [SerializeField] GameObject[] firstEnemyPrefabArray;
    [SerializeField] GameObject[] secondEnemyPrefabArray;
    [SerializeField] GameObject[] thirdEnemyPrefabArray;



    [Header("Shredders")]
    [SerializeField] GameObject leftLaserShredderPrefab;
    [SerializeField] GameObject rightLaserShredderPrefab;

    private Vector2 screenBounds;
    int score;

    private void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Start()
    {
        SpawnLaserShredders();
        StartCoroutine(AsteroidWave());
        StartCoroutine(EnemyWave());
    }

    private void Update()
    {
        score = FindObjectOfType<GameManager>().totalScoreAmount;
    }

    private void SpawnLaserShredders()
    {
        GameObject rightLaserShredder = Instantiate(rightLaserShredderPrefab) as GameObject;
        rightLaserShredder.transform.position = new Vector2(screenBounds.x + 0.3f, screenBounds.y - 5);

        GameObject leftLaserShredder = Instantiate(leftLaserShredderPrefab) as GameObject;
        leftLaserShredder.transform.position = new Vector2(-screenBounds.x - 0.3f, screenBounds.y - 5);
    }

    

    IEnumerator AsteroidWave()
    {
        while (true)
        {
            if (score < firstScoreCheckpoint) 
            {
                var asteroidRespawnTime = Random.Range(firstWaveAsteroidMinSpawnTime, firstWaveAsteroidMaxSpawnTime);
                yield return new WaitForSeconds(asteroidRespawnTime);
                SpawnAsteroid();
            }
            else if (score >= firstScoreCheckpoint && score < secondScoreCheckpoint)
            {
                var asteroidRespawnTime = Random.Range(secondWaveAsteroidMinSpawnTime, secondWaveAsteroidMaxSpawnTime);
                yield return new WaitForSeconds(asteroidRespawnTime);
                SpawnAsteroid();
            }
            else
            {
                var asteroidRespawnTime = Random.Range(thirdWaveAsteroidMinSpawnTime, thirdWaveAsteroidMaxSpawnTime);
                yield return new WaitForSeconds(asteroidRespawnTime);
                SpawnAsteroid();
            }
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = Instantiate(asteroidPrefab) as GameObject;
        asteroid.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y, screenBounds.y));
    }

   
    IEnumerator EnemyWave()
    {
        while (true)
        {
            var enemyRespawnTime = Random.Range(enemyMinSpawnTime, enemyMaxSpawnTime);
            yield return new WaitForSeconds(enemyRespawnTime);
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        if (score < firstScoreCheckpoint)
        {
            var firstEnemyIndex = Random.Range(0, firstEnemyPrefabArray.Length);
            Spawn(firstEnemyPrefabArray[firstEnemyIndex]);
        }
        else if (score >= firstScoreCheckpoint && score < secondScoreCheckpoint)
        {
            var secondEnemyIndex = Random.Range(0, secondEnemyPrefabArray.Length);
            Spawn(secondEnemyPrefabArray[secondEnemyIndex]);
        }
        else
        {
            var thirdEnemyIndex = Random.Range(0, thirdEnemyPrefabArray.Length);
            Spawn(thirdEnemyPrefabArray[thirdEnemyIndex]);
        }
    }
    
    private void Spawn (GameObject myEnemy)
    {
        GameObject enemy = Instantiate(myEnemy) as GameObject;
        enemy.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y + 1, screenBounds.y - 1));
    }
}
