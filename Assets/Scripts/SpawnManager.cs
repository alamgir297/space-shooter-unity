using System.Collections;
using UnityEngine;



public class SpawnManager : MonoBehaviour {
    private float _enemySpawnRate;
    private float _spawnRateChangeInterval;
    private float _decRate = 0.25f;
    private float _minSpawnRate = 1f;
    private bool _isSpawning;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _trippleShotPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerupsContainer;
    [SerializeField] private GameObject[] _powerupCollection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemySpawnRate = 4f;
        _spawnRateChangeInterval = 7;
        _isSpawning = true;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnRateCoroutine());
        StartCoroutine(SpawnPowerups());

    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator SpawnEnemies() {
        while (_isSpawning) {
            Vector3 position = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnRate);

        }
    }

    IEnumerator SpawnPowerups() {
        while (_isSpawning) {
            int index = RandomPowerup();
            Vector3 position = new Vector3(Random.Range(-8, 8), 7, 0);
            Instantiate(_powerupCollection[index], position, Quaternion.identity);
            yield return new WaitForSeconds(PowerupsSpawnRate());
        }
    }

    IEnumerator SpawnRateCoroutine() {
        while (_isSpawning) {
            yield return new WaitForSeconds(_spawnRateChangeInterval);
            if (_enemySpawnRate - _decRate >= _minSpawnRate) {
                _enemySpawnRate -= _decRate;
            }
        }
    }

    private float PowerupsSpawnRate() {
        return Random.Range(4, 6);
    }

    private int RandomPowerup() {
        return Random.Range(0, 3);
    }
    public void OnPlayerDeath() {
        _isSpawning = false;
        Destroy(_enemyContainer);
    }
    public bool GameOver() {
        return _isSpawning ? false : true;
    }

}
