using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    private GameObject _powerupTrippleShot;
    [SerializeField]
    private float _fireRate = .5f;
    private float _canFire = -1f;

    [SerializeField]
    private float _playerHealth = 3f;
    private float score = 0f;
    private SpawnManager spawnManager;
    private bool _powerup1 = false;
    private bool _powerup2 = false;
    private bool _activateTrippleShot = false;
    private int tsLimit = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire) {
            //FireLaser();
            if (_activateTrippleShot) {
                FireLaser(_powerupTrippleShot);
            }
            else {
                FireLaser(laserPrefab);
            }
        }
    }
    void PlayerMovement() {
        //inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //player boundaries
        float upper = 0f;
        float lower = -5f;
        float side = 11f;

        Vector3 direction = new Vector3(x, y, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //defines the boundary
        if (transform.position.y > upper) {
            transform.position = new Vector3(transform.position.x, upper, 0);
        }
        if (transform.position.y < lower) {
            transform.position = new Vector3(transform.position.x, lower, 0);
        }

        if (transform.position.x > side) {
            transform.position = new Vector3(-1 * side, transform.position.y, 0);
        }
        if (transform.position.x < -1 * side) {
            transform.position = new Vector3(side, transform.position.y, 0);
        }
    }

    void FireLaser(GameObject laser) {
        Instantiate(laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        _canFire = Time.time;
        _canFire += _fireRate;
    }
    public void PlayerTakeDamage() {
        _playerHealth--;
        if (_playerHealth <= 0) {
            Destroy(gameObject);
            spawnManager.OnPlayerDeath();
        }
    }
    public void ActivateTrippleShot() {
        _activateTrippleShot = true;
        StartCoroutine(TrippleShotCoroutine());
    }
    IEnumerator TrippleShotCoroutine() {
        yield return new WaitForSeconds(5f);
        _activateTrippleShot = false;
    }
    public void PlayerScore() {
        score++;
    }



}
