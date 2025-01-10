using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 5f;
    private Player Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* _enemySpeed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision with "+ other.name);
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject.GetComponent<Player>();
            if (Player != null)
            {
                Player.PlayerTakeDamage();
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("Didnt hit");
            }
        }

        if (other.CompareTag("Laser"))
        {
            Debug.LogError("Hit: "+ other.name);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }


}
