using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotationSpeed = 50f;
    private SpawnManager spawnManager;
    private Animator animatior;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        animatior = GetComponent<Animator>();
        transform.position = new Vector3(0, 4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidBehavior();
    }

    void AsteroidBehavior() {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Laser")) {
            animatior.SetTrigger("Explode_trig");
            GameManager.Instance.SetGameActive(true);
            spawnManager.StartSpawnRoutines();
            Destroy(gameObject, 1f);
            Destroy(other.gameObject);
        }
    }

}
