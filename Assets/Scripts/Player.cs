using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour {
    //player property
    [SerializeField]
    private float _playerSpeed = 10f;
    [SerializeField]
    private int _playerHealth = 3;
    [SerializeField]
    private int _playerScore  = 0;

    //spawns
    private SpawnManager spawnManager;
    [SerializeField]
    GameObject _laserNormal;
    [SerializeField]
    GameObject _laserTrippleShot;
    [SerializeField]

    //fire
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
   
    //animation
    private UIManager _uiManager;
    Animator animator;
    private Coroutine _currentRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        animator = GetComponent<Animator>();

        if (spawnManager == null) {
            Debug.Log("not found");
        }
        if(_uiManager== null) {
            Debug.Log("Not found");
        }

        _uiManager.UpdateScore(0);
    }

    // Update is called once per frame
    void Update() {
        PlayerBoundaries();
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire) {
            //PlayerFireLaser();
            if (_isTripleShotActive) {
                PlayerFireLaser(_laserTrippleShot);
            }
            else {
                PlayerFireLaser(_laserNormal);
            }
        }
    }
    void PlayerMovement() {
        //inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Debug.Log("horizontalInput: " + horizontalInput);
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
        //player boundaries
        float upper = 0f;
        float lower = -5f;
        float side = 11f;

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
        if (!_isShieldActive) {
            _playerHealth--;
        }
           
        _uiManager.UpdateLives(_playerHealth);
        if (_playerHealth <= 0) {
            Destroy(gameObject);
            spawnManager.OnPlayerDeath();
        }
    }

    //player hit powerups
    public void ActivatePowerupTrippleShot() {
        _isTripleShotActive = true;
        CheckNStartCoroutine();
    }
    public void ActivatePowerupSpeedBoost() {
        _isSpeedBoostActive = true;
        _playerSpeed = 15;
        CheckNStartCoroutine();
    }
    public void ActivatePowerupShield() {
        _isShieldActive = true;
        animator.SetBool("shield", true);
        CheckNStartCoroutine();
    }
    
    IEnumerator PowerupCoroutine() {
        yield return new WaitForSeconds(5f);
        if (_isTripleShotActive) {
            _isTripleShotActive = false;
        }
        if (_isSpeedBoostActive) {
            _playerSpeed = 10;
        }
        if (_isShieldActive) {
            _isShieldActive = false;
            animator.SetBool("shield", false);
        }
    }

    void CheckNStartCoroutine() {
        if (_currentRoutine!=null) {
            StopCoroutine(_currentRoutine);
        }
        _currentRoutine = StartCoroutine(PowerupCoroutine());
    }
    
    //movement animation
    void PlayerMovingRight() {
        animator.SetBool("movingRight", true);
        animator.SetBool("movingLeft", false);
    }
    void PlayerMovingLeft() {
        animator.SetBool("movingRight", false);
        animator.SetBool("movingLeft", true);
    }
    void PlayerIdle() {
        Debug.Log("called idle");
        animator.SetBool("movingRight", false);
        animator.SetBool("movingLeft", false);
    }

}
