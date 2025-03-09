using System.Collections;
using UnityEngine;



public class SpawnManager : MonoBehaviour {
    private float _enemySpawnRate;
    private float _spawnRateChangeInterval;
    private float _decRate = 0.25f;
    private float _minSpawnRate = 2f;
    private bool _isSpawning;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _trippleShotPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerupsContainer;
    [SerializeField] private GameObject[] _powerupCollection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemySpawnRate = 5f;
        _spawnRateChangeInterval = 10;
        GameManager.Instance.GameOver(false);
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(3f);
        while (IsGameActive()) {
            Vector3 position = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnRate);

        }
    }

    IEnumerator SpawnPowerups() {
        while (IsGameActive()) {
            int index = RandomPowerup();
            Vector3 position = new Vector3(Random.Range(-8, 8), 7, 0);
            Instantiate(_powerupCollection[index], position, Quaternion.identity);
            yield return new WaitForSeconds(PowerupsSpawnRate());
        }
    }

    IEnumerator SpawnRateCoroutine() {
        while (IsGameActive()) {
            yield return new WaitForSeconds(_spawnRateChangeInterval);
            if (_enemySpawnRate - _decRate >= _minSpawnRate) {
                _enemySpawnRate -= _decRate;
                Debug.Log("spr: " + _enemySpawnRate);
            }
        }
    }

    public void StartSpawnRoutines() {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnRateCoroutine());
        StartCoroutine(SpawnPowerups());
    }
    private float PowerupsSpawnRate() {
        return Random.Range(4, 6);
    }

    private int RandomPowerup() {
        return Random.Range(0, 3);
    }
    public void OnPlayerDeath() {
        GameManager.Instance.GameOver(true);
        Destroy(_enemyContainer);
    }
    public bool IsGameOver() {
        return GameManager.Instance.IsGameOver() ? true : false;
    }

    private bool IsGameActive() {
        if(GameManager.Instance.IsGameActive() && !IsGameOver()) {
            return true;
        }
        return false;
    }

}
