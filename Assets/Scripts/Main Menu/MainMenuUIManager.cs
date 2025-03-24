using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIManager : MonoBehaviour {
    private bool _isSettingsEnabled;

    //main menu items
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitGame;
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TMP_InputField _askPlayerNameInput;
    [SerializeField] private GameObject _quitPopupPanel;
    [SerializeField] private GameObject _askPlayerNamePopup;

    //settings menu items
    [SerializeField] private Button _editNameButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private GameObject settingsOptions;
    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private GameObject _resetPopupPanel;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TextMeshProUGUI _volumeLevelText;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //_isSettingsEnabled = false;
        _startGame.onClick.AddListener(StartGame);
        //_quitGame.onClick.AddListener(QuitGame);
        _settingsButton.onClick.AddListener(ToggleSettings);
        _volumeSlider.value = GameManager.Instance.GetSoundVolume();

        GetHighScore();
        ShowNameNHighScore();
        ShowPlayerName();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleSettings();
        }
    }

    public void InputHandler() {
        if (!GameManager.Instance.IsPlayerExist()) {
            GameManager.Instance.SetPlayerName(_askPlayerNameInput.text);
            GameManager.Instance.SavePlayer();
            Debug.Log("name: " + GameManager.Instance.GetPlayerName());
            ShowNameNHighScore();
            ShowPlayerName();
            ToggleAskPlayerNamePopup();
        }
        else {
            GameManager.Instance.SetPlayerName(_playerNameInput.text);
            _playerNameInput.gameObject.SetActive(false);
            ShowPlayerName();
            ShowNameNHighScore();
        }
    }

    public void ShowNameNHighScore() {
        string name = GameManager.Instance.GetPlayerName();
        int score = GameManager.Instance.GetHighScore();
        _highScoreText.text = "Highest Score\n" + name + ": " + score;
    }

    //game states
    void StartGame() {
        if (!GameManager.Instance.IsPlayerExist()) {
            ToggleAskPlayerNamePopup();
        }
        else {
            GameManager.Instance.StartNewGame();
        }
    }
    public void QuitGame() {
        GameManager.Instance.Exit();
    }
    void GetHighScore() {
        GameManager.Instance.SetHighScore(GameManager.Instance.GetHighScore());
    }
    void ShowPlayerName() {
        _playerNameText.text = GameManager.Instance.GetPlayerName();
    }
    public void ResetGame() {
        GameManager.Instance.ResetPlayer();
        _resetPopupPanel.SetActive(false);
        ShowPlayerName();
        ShowNameNHighScore();
    }

    //settings options
    public void ToggleSettings() {
        if (!_isSettingsEnabled) {
            settingsOptions.SetActive(true);
            mainOptions.SetActive(false);
        }
        else {
            settingsOptions.SetActive(false);
            mainOptions.SetActive(true);
        }
        _isSettingsEnabled = !_isSettingsEnabled;
    }

    public void UpdateSoundVolumeFromSlider() {
        
        GameManager.Instance.SetSoundVolume(_volumeSlider.value);
        audioSource.volume = GameManager.Instance.GetSoundVolume();
        float volume = GameManager.Instance.GetSoundVolume() * 100;

        _volumeLevelText.text = "Volume:" + (int)volume;
        if (volume < 70) {
            _volumeSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
        else {
            _volumeSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
    }

    public void ToggleEditName() {
        _playerNameInput.gameObject.SetActive(!_playerNameInput.gameObject.activeSelf);
    }
    public void ToggleResetPopup() {
        _resetPopupPanel.SetActive(!_resetPopupPanel.activeSelf);
    }
    public void ToggleQuitPopup() {
        _quitPopupPanel.SetActive(!_quitPopupPanel.activeSelf);
    }
    public void ToggleAskPlayerNamePopup() {
        _askPlayerNamePopup.SetActive(!_askPlayerNamePopup.activeSelf);
    }
}
