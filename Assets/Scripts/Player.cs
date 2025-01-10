using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed=10f;
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
    private bool _powerup1=false;
    private bool _powerup2=false;
    private bool _powerup3=false;
    private int tsLimit = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time>= _canFire)
        {
            FireLaser();
        }
    }
    void PlayerMovement()
    {
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
        if (transform.position.y > upper)
        {
            transform.position = new Vector3(transform.position.x, upper, 0);
        }
        if (transform.position.y < lower)
        {
            transform.position = new Vector3(transform.position.x, lower, 0);
        }

        if (transform.position.x > side)
        {
            transform.position = new Vector3(-1 * side, transform.position.y, 0);
        }
        if (transform.position.x < -1 * side)
        {
            transform.position = new Vector3(side, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided with: "+other.name);
        if (other.CompareTag("Powerup3"))
        {
            _powerup3 = true;
            tsLimit = 5;
            Destroy(other.gameObject);
        }
        
    }
    void FireLaser()
    {
        if (_powerup3 && tsLimit>0)
        {
            Instantiate(_powerupTrippleShot, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            //Instantiate(_powerupTrippleShot, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            _canFire = Time.time;
            _canFire += _fireRate;
            tsLimit--;
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            //Instantiate(_powerupTrippleShot, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            _canFire = Time.time;
            _canFire += _fireRate;
        }
        
    }

    public void PlayerTakeDamage()
    {
        _playerHealth --;
        Debug.Log("Health remain: " + _playerHealth);
        if (_playerHealth <= 0)
        {
            Destroy(gameObject);
            spawnManager.OnPlayerDeath();
        }
    }

    public void PlayerScore()
    {
        score ++;
    }



}
