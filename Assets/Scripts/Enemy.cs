using UnityEngine;

public class Enemy : MonoBehaviour {
    private Player _player;
    private Animator _animator;
    private UIManager _uiManager;
    private AudioSource _enemyAudio;


    [SerializeField] private float _enemySpeed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = GetComponent<Animator>();
        _enemyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        EnemyBehavior();
    }

    void EnemyBehavior() {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        if (transform.position.y < -5f) {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //_player = other.gameObject.GetComponent<_player>();
            if (_player != null) {
                _player.PlayerTakeDamage();
                EnemyExplosion();
                _enemyAudio.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 1f);
            }
            else {
                Debug.LogError("Didnt hit");
            }
        }

        if (other.CompareTag("Laser")) {
            EnemyExplosion();
            _enemyAudio.Play();
            _player.PlayerScores(10);
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 1f);
            Destroy(other.gameObject);
        }
    }

    void EnemyExplosion() {
        _enemySpeed = 0f;
        _animator.SetTrigger("EnemyHit");
    }
}
