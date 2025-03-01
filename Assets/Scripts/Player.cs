using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour {
    //player property
    private float _speedMultiplier;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private int _playerHealth;
    [SerializeField] private int _playerScore;

    //player boundaries
    private const float upper = 0f;
    private const float lower = -2f;
    private const float side = 10.4f;

    //spawns
    private SpawnManager _spawnManager;
    [SerializeField] GameObject _laserNormal;
    [SerializeField] GameObject _laserTrippleShot;

    //fire
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    //animation
    private UIManager _uiManager;
    Animator _animator;
    [SerializeField] GameObject _shieldEffect;
    [SerializeField] GameObject _thrusterEffect;
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _leftEngine, _rightEngine;

    //Audio
    private AudioSource _playerAudio;
    [SerializeField] private AudioClip _laserShotAudio;
    [SerializeField] private AudioClip _powerupCollectionAudio;
    [SerializeField] private AudioClip _explosionAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        transform.position = new Vector3(0, 0, 0);

        _playerScore = 0;
        _playerHealth = 3;
        _playerSpeed = 10f;
        _speedMultiplier = 1.3f;

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();

        if (_spawnManager == null) {
            Debug.Log("not found");
        }
        if (_uiManager == null) {
            Debug.Log("Not found");
        }

        _uiManager.UpdateScore(0);
    }

    // Update is called once per frame
    void Update() {
        PlayerBoundaries();
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire) {
            if (_isTripleShotActive) {
                PlayerFireLaser(_laserTrippleShot);
            }
            else {
                PlayerFireLaser(_laserNormal);
            }
            _playerAudio.PlayOneShot(_laserShotAudio);
        }
    }
    void PlayerMovement() {
        //inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // activate moving animation
        if (Mathf.Abs(horizontalInput) > 0.01f) {
            if (horizontalInput > 0.01f) {
                PlayerMovingRight();
            }
            if (horizontalInput < 0.01f) {
                PlayerMovingLeft();
            }
        }
        else {
            PlayerIdle();
        }

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);
    }

    void PlayerBoundaries() { 
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

    //firing mechanism
    void PlayerFireLaser(GameObject laser) {
        Instantiate(laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        _canFire = Time.time;
        _canFire += _fireRate;
    }

    public void PlayerScores(int score) {
        _playerScore += score;
        _uiManager.UpdateScore(_playerScore);
    }

    //player gets hit by obstacles
    public void PlayerTakeDamage() {
        if (_isShieldActive) {
            _isShieldActive = false;
            _shieldEffect.SetActive(false);
            return;
        }
        _playerHealth--;
        if (_playerHealth == 2) _leftEngine.SetActive(true);
        else _rightEngine.SetActive(true);
        _uiManager.UpdateLives(_playerHealth);
        if (_playerHealth <= 0) {
            _playerAudio.PlayOneShot(_explosionAudio);
            Destroy(gameObject,0.5f);
            _spawnManager.OnPlayerDeath();
            _uiManager.ShowGameOver(_playerScore);
            GameManager.Instance.SetHighScore(_playerScore);
            GameManager.Instance.SavePlayer();
            _gameOver.SetActive(true);
        }
    }

    //player hit powerups
    public void ActivatePowerupTrippleShot() {
        _isTripleShotActive = true;
        _playerAudio.PlayOneShot(_powerupCollectionAudio);
        StartCoroutine(TripleShotCoroutine());
    }
    public void ActivatePowerupSpeedBoost() {
        _isSpeedBoostActive = true;
        _thrusterEffect.SetActive(true);
        _playerAudio.PlayOneShot(_powerupCollectionAudio);
        _playerSpeed = (_playerSpeed * _speedMultiplier) <= 20 ? _playerSpeed *= _speedMultiplier : _playerSpeed;

        StartCoroutine(SpeedBoostCoroutine());
    }
    public void ActivatePowerupShield() {
        _isShieldActive = true;
        _playerAudio.PlayOneShot(_powerupCollectionAudio);
        _shieldEffect.SetActive(true);
        StartCoroutine(ShieldCoroutine());
    }
    IEnumerator TripleShotCoroutine() {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }
    IEnumerator SpeedBoostCoroutine() {
        yield return new WaitForSeconds(5f);
        _playerSpeed = 10f;
        _thrusterEffect.SetActive(false);
    }
    IEnumerator ShieldCoroutine() {
        yield return new WaitForSeconds(5f);
        _isShieldActive = false;
        _shieldEffect.SetActive(false);
    }

    //movement animation
    void PlayerMovingRight() {
        _animator.SetBool("movingRight", true);
        _animator.SetBool("movingLeft", false);
    }
    void PlayerMovingLeft() {
        _animator.SetBool("movingRight", false);
        _animator.SetBool("movingLeft", true);
    }
    void PlayerIdle() {
        _animator.SetBool("movingRight", false);
        _animator.SetBool("movingLeft", false);
    }

}
