using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    private float _enemyFireRate;
    private float _laserSpeed = 5f;

    private Player _player;
    private Animator _animator;
    private UIManager _uiManager;
    private AudioSource _enemyAudio;


    [SerializeField] private float _enemySpeed = 5f;
    [SerializeField] GameObject _laserPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemyFireRate = 7f;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = GetComponent<Animator>();
        _enemyAudio = GetComponent<AudioSource>();

        //StartCoroutine(EnemyFireRoutine());
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
    
    IEnumerator EnemyFireRoutine() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(4, _enemyFireRate));
            FireLaser();
        }
    }

    void FireLaser() {
        GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        laser.transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
        if (transform.position.y < -6f) {
            Destroy(laser.gameObject);
        }
    }

    void EnemyExplosion() {
        _enemySpeed = 0f;
        _animator.SetTrigger("EnemyHit");
    }
}
