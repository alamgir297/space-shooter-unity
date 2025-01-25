using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _enemySpawnRate = 5;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupsContainer;
    private bool _isSpawning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(SpawnEnemies());
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
            Vector3 position = new Vector3(Random.Range(-8, 8), 7, 0);
            Instantiate(_trippleShotPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(PowerupsSpawnRate());
        }
    }

    private float PowerupsSpawnRate() {
        return Random.Range(4, 8);
    }

    public void OnPlayerDeath() {
        _isSpawning = false;
    }
}
