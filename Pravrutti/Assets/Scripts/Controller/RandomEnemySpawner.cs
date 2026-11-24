using UnityEngine;

[System.Serializable]
public class EnemyContainer
{
    public GameObject enemy;
    [Range(0,100)]
    public float probability;
    public bool isDefeated;
}

public class RandomEnemySpawner : MonoBehaviour
{
    public EnemyContainer[] enemies;
    public RadarCombatController radar;

    public float currentRandom;
    private void Awake()
    {
        foreach (EnemyContainer enemyContainer in enemies)
        {
            enemyContainer.enemy.SetActive(false);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnEnable()
    {
        CheckForEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetEnemyDefeated(GameObject enemy)
    {
        foreach (EnemyContainer enemyContainer in enemies)
        {
            if (enemyContainer.enemy == enemy)
            {
                enemyContainer.isDefeated = true;
                break;
            }
        }
    }
    public void CheckForEnemy()
    {
        currentRandom = Random.Range(0, 100);
        float cumulativeProbability = 0f;
        foreach (EnemyContainer enemyContainer in enemies)
        {
            cumulativeProbability += enemyContainer.probability;
            if (currentRandom <= cumulativeProbability && !enemyContainer.isDefeated)
            {
                enemyContainer.enemy.SetActive(true);
                radar.SetEnemy(this, enemyContainer.enemy);

                radar.gameObject.SetActive(true);
                break;
            }
        }
    }
}
