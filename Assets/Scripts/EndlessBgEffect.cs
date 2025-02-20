using UnityEngine;

public class EndlessBgEffect : MonoBehaviour
{
    private float _movingSpeed;

    private Vector3 _initialPosition;
    private float _bgHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _movingSpeed = 7f;
        _initialPosition = transform.position;
        _bgHeight = GetComponent<BoxCollider2D>().size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBgDown();
        if (transform.position.y < _initialPosition.y - _bgHeight) {
            ResetBg();
        }
    }

    private void MoveBgDown() {
        transform.Translate(Vector3.down * _movingSpeed * Time.deltaTime);
    }

    private void ResetBg() {
        transform.position = _initialPosition;
    }
}
