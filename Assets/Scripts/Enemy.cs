using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _enemySpeed = 5f;
    private Player Player;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
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
            //Player = other.gameObject.GetComponent<Player>();
            if (Player != null) {
                Player.PlayerTakeDamage();
                EnemyExplosion();
                Destroy(this.gameObject, 1f);
            }
            else {
                Debug.LogError("Didnt hit");
            }
        }

        if (other.CompareTag("Laser")) {
            EnemyExplosion();
            Destroy(gameObject, 1f);
            Destroy(other.gameObject);
        }
    }

    void EnemyExplosion() {
        _enemySpeed = 0f;
        animator.SetTrigger("EnemyHit");
    }

}
