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
    [SerializeField] private Text _gameOverText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pauseMenuUi;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject _touchControls;
    
    void Start() {
        //EnableTouchControls(IsMobilePlatform());

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _liveImage.sprite = _liveSprites[3];
        Time.timeScale = 1;

        SetSoundVolume();

        StartCoroutine(UpdateTimeRoutine());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }
        ShowHighScore();
    }
    private bool IsMobilePlatform() {
        if (Application.isMobilePlatform) {
            Debug.Log("Mobile platform");
        }
        return Application.isMobilePlatform;
    }

    public void UpdateScore(int score) {
        _scoreText.text = "Score: " + score;
    }

    public void ShowHighScore() {
        string name = GameManager.Instance.GetPlayerName();
        int score = GameManager.Instance.GetHighScore();
        _highScoreText.text = "" + name + ": " + score;
    }

    public void UpdateLives(int currentLive) {
        if(currentLive< _liveSprites.Length)
            _liveImage.sprite = _liveSprites[(currentLive)];
    }

    IEnumerator UpdateTimeRoutine() {
        while (!GameManager.Instance.IsGameOver()) {
            int min = (int)Time.timeSinceLevelLoad / 60;
            int sec = (int)Time.timeSinceLevelLoad % 60;
            _elapsedTime.text = min + ":" + sec;
            yield return new WaitForSeconds(1f);
        }
    }

    public void ShowGameOver(int score) {
        _gameOverPanel.SetActive(true);
        _gameOverText.text = "Game Over\n" + "Your Score: " + score;
    }

    private void EnableTouchControls(bool isMobile) {
        _touchControls.SetActive(isMobile);
    }
    void TogglePauseMenu() {
        if (Time.timeScale == 1) {
            Time.timeScale = 0;
            _pauseMenuUi.SetActive(true);
        }
        else {
            Time.timeScale = 1;
            _pauseMenuUi.SetActive(false);
        }
    }
    public void MainMenu() {
        GameManager.Instance.BackToMain();
    }
    public void Restart() {
        GameManager.Instance.StartNewGame();
    }
    void SetSoundVolume() {
        audioSource.volume = GameManager.Instance.GetSoundVolume();
    }
}