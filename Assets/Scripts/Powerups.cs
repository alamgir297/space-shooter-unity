using UnityEngine;

public class Powerups : MonoBehaviour {
    [SerializeField]
    private float _speed = 5f;
    private float _powerupDuration = 5f;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        PowerupsBehavior();
    }
    void PowerupsBehavior() {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -5f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (CompareTag("TrippleShot")) {
                player.ActivatePowerupTrippleShot();
                Destroy(gameObject);
            }
            if (CompareTag("SpeedBoost")) {
                player.ActivatePowerupSpeedBoost();
                Destroy(gameObject);
            }
            if (CompareTag("Shield")) {
                player.ActivatePowerupShield();
                Destroy(gameObject);
            }
            
        }
    }


}
