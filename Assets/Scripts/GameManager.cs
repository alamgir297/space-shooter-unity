using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    private bool _isGameOver = false;
    private string _playerName;
    private int _highestScore;


    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _startGameButton.onClick.AddListener(StartNewGame);
        _quitButton.onClick.AddListener(Exit);
        LoadPlayer();
    }

    private void Awake() {
        if (Instance != null && Instance!=this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string GetPlayerName() {
        return _playerName;
    }
    public void SetPlayerName(string name) {
        _playerName = name;
    }

    public int GetHighScore() {
        return _highestScore;
    }
    public void SetHighScore(int highScore) {
        if (highScore > _highestScore) {
            _highestScore = highScore;
        }
    }

    public void StartNewGame() {
        SceneManager.LoadScene(1);
    }
    public void BackToMain() {
        SceneManager.LoadScene(0);
    }
    public void Exit() {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }
    public void GameOver() {
        _isGameOver = true;
    }
    public bool IsGameOver() {
        return _isGameOver ? true : false;
    }

    [System.Serializable]
    private class SaveData {
        public string name;
        public int hightScore;
    }

    public void SavePlayer() {
        SaveData data = new();
        data.name = _playerName;
        data.hightScore = _highestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath+ "savefile.json", json);
    }

    public void LoadPlayer() {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            _playerName = data.name;
            _highestScore = data.hightScore;
        }
    }

}
