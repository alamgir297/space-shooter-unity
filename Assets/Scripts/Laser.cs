using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField]
    private float _laserSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        if (transform.position.y > 6.5f) {
            Destroy(gameObject);
        }
    }

}
