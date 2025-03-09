using UnityEngine;

public class EndlessBgEffect : MonoBehaviour
{
    private float _movingSpeed;

    private Vector3 _initialPosition;
    private float _bgHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bgHeight = GetComponent<BoxCollider2D>().size.y / 2;

        _movingSpeed = 9f;
        _initialPosition = transform.position;

        GameManager.Instance.SetGameActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameManager.Instance.IsGameOver() && GameManager.Instance.IsGameActive()) {
            MoveBgDown();
        }
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
