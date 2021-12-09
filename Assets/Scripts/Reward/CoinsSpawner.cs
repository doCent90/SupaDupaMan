using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinScaler _coinScaler;

    private const float Delay = 0.08f;
    private const int CountCoinsMin = 4;
    private const int CountCoinsMax = 8;
    private const float RandomRangePosition = 0.5f;

    public void StartSpawn(Transform enemy)
    {
        StartCoroutine(Spawn(enemy));
    }

    private void Awake()
    {
        if (_coinScaler == null)
            throw new InvalidOperationException();
    }

    private IEnumerator Spawn(Transform enemy)
    {
        var waitForSeconds = new WaitForSeconds(Delay);
        int randomCount = Random.Range(CountCoinsMin, CountCoinsMax);

        for (int i = 0; i < randomCount; i++)
        {
            var coin = Instantiate(_coinPrefab, RandomSpawnPosition(enemy), Quaternion.identity);
            coin.Init(_coinScaler);

            yield return waitForSeconds;
        }
    }

    private Vector3 RandomSpawnPosition(Transform enemy)
    {
        var position = enemy.position;
        float x = Random.Range(-RandomRangePosition, RandomRangePosition);
        float y = Random.Range(-RandomRangePosition, RandomRangePosition);
        float z = Random.Range(-RandomRangePosition, RandomRangePosition);

        position += new Vector3(x, y, z);

        return position;
    }
}
