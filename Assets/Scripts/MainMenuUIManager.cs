using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIManager : MonoBehaviour {

    [SerializeField] private Button _startGame;
    [SerializeField] private Button _quitGame;
    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GameManager.Instance.SetHighScore(GameManager.Instance.GetHighScore());
        ShowNameNHighScore();
        _startGame.onClick.AddListener(StartGame);
        _quitGame.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update() {
        ShowNameNHighScore();
    }

    public void InputHandler() {
        GameManager.Instance.SetPlayerName(_playerNameInput.text);
    }

    public void ShowNameNHighScore() {
        string name = GameManager.Instance.GetPlayerName();
        int score = GameManager.Instance.GetHighScore();
        _highScoreText.text = "Highest Score\n" + name + ": " + score;
    }

    void StartGame() {
        GameManager.Instance.StartNewGame();
    }
    void QuitGame() {
        GameManager.Instance.Exit();
    }
}
