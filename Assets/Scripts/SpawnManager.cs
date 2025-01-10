using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _enemySpawnRate = 5;
    [SerializeField]
    private float _powerupSpawnRate = 7f;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupsContainer;
    private bool _isSpawning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemies()
    {
        while (_isSpawning)
        {
            Vector3 position= new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent= _enemyContainer.transform;
            Debug.Log("creted :"+newEnemy.name);   
            yield return new WaitForSeconds(_enemySpawnRate);

        }
    }

    IEnumerator SpawnPowerups()
    {
        while (true)
        {
            Vector3 position= new Vector3(Random.Range(-9, 9), 7, 0 );
            Instantiate(_trippleShotPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(_powerupSpawnRate);
        }
    }

    public void OnPlayerDeath()
    {
        _isSpawning = false;
    }
}
