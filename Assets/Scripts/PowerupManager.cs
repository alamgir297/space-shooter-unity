using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PowerupManager : MonoBehaviour {
    private float _speedMultiplier = 1.5f;
    private float _powerupDelay = 5f;
    private Player _player;
    private PlayerAnimation _animation;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    private AudioSource _powerupAudio;
    [SerializeField] private AudioClip powerupCollectedAudio;

    private void Awake() {
        _player = GetComponent<Player>();
        _animation = GetComponent<PlayerAnimation>();
        _powerupAudio = GetComponent<AudioSource>();
    }

    public void ActivatePowerup(PowerupType powerupType) {
        PlayAudio();
        switch (powerupType) {
            case PowerupType.TripleShot:
                ActivateTripleShot();
                break;
            case PowerupType.SpeedBoost:
                ActivateSpeedBoost();
                break;
            case PowerupType.Shield:
                ActivateShield();
                break;
        }
    }

    void ActivateTripleShot() {
        _isTripleShotActive = true;
        StartCoroutine(DeactivatePowerupAfterDelay(PowerupType.TripleShot));
    }
    void ActivateSpeedBoost() {
        _isSpeedBoostActive = true;
        _player.SetSpeedBoost(_speedMultiplier);
        _animation.ActivateThrusterEffect(true);
        StartCoroutine(DeactivatePowerupAfterDelay(PowerupType.SpeedBoost));
    }
    void ActivateShield() {
        _isShieldActive = true;
        _animation.ActivateShieldEffect(true);
        StartCoroutine(DeactivatePowerupAfterDelay(PowerupType.Shield));
    }

    IEnumerator DeactivatePowerupAfterDelay(PowerupType powerupType) {
        yield return new WaitForSeconds(_powerupDelay);

        switch (powerupType) {
            case PowerupType.TripleShot:
                _isTripleShotActive = false;
                break;
            case PowerupType.SpeedBoost:
                _isSpeedBoostActive = false;
                _player.SetSpeedBoost(1);
                _animation.ActivateThrusterEffect(false);
                break;
            case PowerupType.Shield:
                _isShieldActive = false;
                _animation.ActivateShieldEffect(false);
                break;
        }
    }
    public void SetShieldActive(bool isActive) {
        _isShieldActive = isActive;
        _animation.ActivateShieldEffect(false);
    }
    public bool IsTripleShotActive() => _isTripleShotActive;
    public bool IsSpeedBoostActive() => _isSpeedBoostActive;
    public bool IsShieldActive() => _isShieldActive;

    private void PlayAudio() {
        _powerupAudio.PlayOneShot(powerupCollectedAudio);
    }
}
