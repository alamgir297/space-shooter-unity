using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    //inputs
    private const float _movementEpsilon = 1e-3f;
    private InputAction _move;
    private InputAction _fire;
    [SerializeField] private InputSystem_Actions _playerControls;

    // Player properties
    private float _currentSpeed;
    private int _playerHealth;
    private int _playerScore;
    [SerializeField] private float _playerSpeed = 7f;

    // Player boundaries
    private const float upper = 0f;
    private const float lower = -2f;
    private const float side = 9.4f;

    // Spawns
    private SpawnManager _spawnManager;
    private PowerupManager _powerupManager;
    [SerializeField] private GameObject _laserNormal;
    [SerializeField] private GameObject _laserTrippleShot;

    // Fire
    private float _fireRate = 0.1f;
    private float _canFire = -1f;

    // Animation
    private UIManager _uiManager;
    private PlayerAnimation _playerAnimation;

    // Audio
    private AudioSource _playerAudio;
    [SerializeField] private AudioClip _laserShotAudio;
    [SerializeField] private AudioClip _powerupCollectionAudio;
    [SerializeField] private AudioClip _explosionAudio;

    private void Awake() {
        _playerControls = new InputSystem_Actions();
    }
    void Start() {
        InitializePlayer();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _powerupManager = GetComponent<PowerupManager>();
        _playerAudio = GetComponent<AudioSource>();

        if (_spawnManager == null) {
            Debug.LogError("Spawn Manager not found.");
        }
        if (_uiManager == null) {
            Debug.LogError("UI Manager not found.");
        }

        _uiManager.UpdateScore(0);
    }

    void Update() {
        PlayerBoundaries();
        PlayerMovement();
    }
    private void OnEnable() {
        _move = _playerControls.Player.Move;
        _move.Enable();

        _fire = _playerControls.Player.Attack;
        _fire.Enable();
        _fire.performed += Fire;
    }
    private void OnDisable() {
        _move.Disable();
        _fire.Disable();
    }
    void InitializePlayer() {
        transform.position = new Vector3(0, 0, 0);
        _playerScore = 0;
        _playerHealth = 3;
        _currentSpeed = _playerSpeed;
    }

    void PlayerMovement() {

        Vector2 direction = _move.ReadValue<Vector2>().normalized;
        // Activate moving animation
        if (Mathf.Abs(direction.x) > _movementEpsilon) {
            if (direction.x > 0) {
                _playerAnimation.PlayerMovingRight();
            }
            else {
                _playerAnimation.PlayerMovingLeft();
            }
        }
        else {
            _playerAnimation.PlayerIdle();
        }

        transform.Translate(_currentSpeed * Time.deltaTime * direction);
    }
    public void Fire(InputAction.CallbackContext contex) {
        if (Time.time >= _canFire) {
            if (_powerupManager.IsTripleShotActive()) {
                PlayerFireLaser(_laserTrippleShot);
            }
            else {
                PlayerFireLaser(_laserNormal);
            }
            PlaySound(_laserShotAudio);
        }
    }

    void PlayerBoundaries() {
        // Define the boundary
        if (transform.position.y > upper) {
            transform.position = new Vector3(transform.position.x, upper, 0);
        }
        if (transform.position.y < lower) {
            transform.position = new Vector3(transform.position.x, lower, 0);
        }

        if (transform.position.x > side) {
            transform.position = new Vector3(side, transform.position.y, 0);
        }
        if (transform.position.x < -1 * side) {
            transform.position = new Vector3(-1 * side, transform.position.y, 0);
        }
    }

    void PlayerFireLaser(GameObject laser) {
        Instantiate(laser, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        _canFire = Time.time;
        _canFire += _fireRate;
    }

    public void PlayerScores(int score) {
        _playerScore += score;
        _uiManager.UpdateScore(_playerScore);
    }

    public void PlayerTakeDamage() {
        if (_powerupManager.IsShieldActive()) {
            _powerupManager.SetShieldActive(false);
            return;
        }

        _playerHealth--;
        _playerAnimation.ActivateEngineDamage(_playerHealth); // Activate engine damage effects
        _uiManager.UpdateLives(_playerHealth);

        if (_playerHealth < 1) {
            PlaySound(_explosionAudio);
            Destroy(gameObject, 0.5f);
            _spawnManager.OnPlayerDeath();
            _uiManager.ShowGameOver(_playerScore);
            GameManager.Instance.SetHighScore(_playerScore);
            GameManager.Instance.SavePlayer();
        }
    }
    public void SetSpeedBoost(float multiplier) {
        _currentSpeed = _playerSpeed * multiplier;
    }
    public void PlaySound(AudioClip clip) {
        _playerAudio.PlayOneShot(clip);
    }
}