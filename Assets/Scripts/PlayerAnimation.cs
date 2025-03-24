using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator _animator;

    [SerializeField] private GameObject _shieldEffect;
    [SerializeField] private GameObject _thrusterEffect;
    [SerializeField] private GameObject _leftEngine, _rightEngine;

    void Start() {
        _animator = GetComponent<Animator>();
        if (_animator == null) {
            Debug.LogError("Animator component not found on the player.");
        }

        // Ensure effects are disabled at the start
        _shieldEffect.SetActive(false);
        _thrusterEffect.SetActive(false);
        _leftEngine.SetActive(false);
        _rightEngine.SetActive(false);
    }

    // Movement animations
    public void PlayerMovingRight() {
        _animator.SetBool("movingRight", true);
        _animator.SetBool("movingLeft", false);
    }

    public void PlayerMovingLeft() {
        _animator.SetBool("movingRight", false);
        _animator.SetBool("movingLeft", true);
    }

    public void PlayerIdle() {
        _animator.SetBool("movingRight", false);
        _animator.SetBool("movingLeft", false);
    }

    // Power-up animations
    public void ActivateShieldEffect(bool isActive) {
        _shieldEffect.SetActive(isActive);
    }

    public void ActivateThrusterEffect(bool isActive) {
        _thrusterEffect.SetActive(isActive);
    }

    public void ActivateEngineDamage(int health) {
        if (health == 2) {
            _leftEngine.SetActive(true);
        }
        else if (health == 1) {
            _rightEngine.SetActive(true);
        }
    }
}