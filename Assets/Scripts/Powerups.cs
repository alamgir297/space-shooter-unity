using UnityEngine;

public class Powerups : MonoBehaviour {

    [SerializeField] private PowerupType _powerupType;
    [SerializeField] private float _speed;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _speed = 9f;
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
            PowerupManager powerupManager = other.GetComponent<PowerupManager>();
            if (powerupManager != null) {
                powerupManager.ActivatePowerup(_powerupType);
            }
            Destroy(gameObject);

            //if (CompareTag("TrippleShot")) {
            //    player.ActivatePowerupTrippleShot();
            //    Destroy(gameObject);
            //}
            //if (CompareTag("SpeedBoost")) {
            //    player.ActivatePowerupSpeedBoost();
            //    Destroy(gameObject);
            //}
            //if (CompareTag("Shield")) {
            //    player.ActivatePowerupShield();
            //    Destroy(gameObject);
            //}

        }
    }


}

public enum PowerupType {
    TripleShot,
    SpeedBoost,
    Shield
}
