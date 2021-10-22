using System.Collections;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private const float Delay = 0.1f;
    private const int CountCoinsMin = 10;
    private const int CountCoinsMax = 20;
    private const float RandomRangePosition = 0.5f;

    public void StartSpawn(EnemyGigant enemyGigant)
    {
        StartCoroutine(Spawn(enemyGigant));
    }

    private IEnumerator Spawn(EnemyGigant enemyGigant)
    {
        var waitForSeconds = new WaitForSeconds(Delay);
        int randomCount = Random.Range(CountCoinsMin, CountCoinsMax);

        for (int i = 0; i < randomCount; i++)
        {
            var coin = Instantiate(_coinPrefab, RandomSpawnPosition(enemyGigant), Quaternion.identity);

            yield return waitForSeconds;
        }
    }

    private Vector3 RandomSpawnPosition(EnemyGigant enemyGigant)
    {
        var position = enemyGigant.transform.position;
        float x = Random.Range(-RandomRangePosition, RandomRangePosition);
        float y = Random.Range(-RandomRangePosition, RandomRangePosition);
        float z = Random.Range(-RandomRangePosition, RandomRangePosition);

        position += new Vector3(x, y, z);

        return position;
    }
}
