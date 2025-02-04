using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _enemySpawnRate;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupsContainer;
    [SerializeField]
    private GameObject[] _powerupCollection;
    private float _decRate = 0.25f;
    
    private bool _isSpawning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
        _enemySpawnRate = 5f;
    }

    // Update is called once per frame
    void Update() {
    
    }
    IEnumerator SpawnEnemies() {
        while (_isSpawning) {
            Vector3 position = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(EnemySpawnRate());

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

    private float PowerupsSpawnRate() {
        return Random.Range(4, 8);
    }

    private int RandomPowerup() {
        return Random.Range(0, 3);
    }
    float EnemySpawnRate() {
        int elapsedTime = (int) Time.time;
        //Debug.Log("elt; " + elapsedTime);
        if (elapsedTime % 15 == 0) {
            float multiplier = elapsedTime / 15;
            float factor = _decRate * multiplier;
            if (_enemySpawnRate - factor >= 2) {
                _enemySpawnRate -= factor;
                Debug.Log("spawn rate: " + _enemySpawnRate);
                return _enemySpawnRate;
            }
        }
        return _enemySpawnRate;
    }
    public void OnPlayerDeath() {
        _isSpawning = false;
    }
   
}
