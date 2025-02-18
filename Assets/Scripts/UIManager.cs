using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    
    SpawnManager _spawnManager;
    
    
    [SerializeField] private Image _liveImage;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _elapsedTime;
    [SerializeField] private Text _gameOver;
    
    void Start() {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _liveImage.sprite = _liveSprites[3];
        StartCoroutine(UpdateTimeRoutine());
    }

    // Update is called once per frame
    void Update() {
        //UpdateTime();
    }

    public void UpdateScore(int score) {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLive) {
        _liveImage.sprite = _liveSprites[(currentLive)];
    }

    IEnumerator UpdateTimeRoutine() {
        while (!_spawnManager.GameOver()) {
            int min = (int)Time.timeSinceLevelLoad / 60;
            int sec = (int)Time.timeSinceLevelLoad % 60;
            _elapsedTime.text = min + ":" + sec;
            yield return new WaitForSeconds(1f);
        }
    }

    public void ShowGameOver(int score) {
        _gameOver.text = "Game Over\n" + "Your Score: " + score;
    }
}