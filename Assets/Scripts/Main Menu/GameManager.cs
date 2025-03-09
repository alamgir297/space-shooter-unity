using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour {

    public string path;
    private bool _isGameOver;
    private bool _isGameActive;
    private string _playerName;
    private int _highestScore;
    private float _soundVolume;

    public static GameManager Instance; //{ get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _isGameOver = false;
        _isGameActive = false;
        LoadPlayer();
        SetSoundVolume(0.5f);
    }

    //singleton pattern
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        path = Application.persistentDataPath + "savefile.json";
        DontDestroyOnLoad(gameObject);
    }

    // Encapsulation
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
    public void SetSoundVolume(float volume) {
        _soundVolume = volume;
    }
    public float GetSoundVolume() {
        return _soundVolume;
    }


    //game states
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

    //game controls
    public void GameOver(bool isGameOver) {
        _isGameOver = isGameOver;
    }
    public bool IsGameOver() {
        return _isGameOver ? true : false;
    }
    public bool IsGameActive() {
        return _isGameActive ? true : false;
    }
    public void SetGameActive(bool val) {
        _isGameActive = val;
    }



    //data persistence
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
        File.WriteAllText(path, json);
    }

    public void LoadPlayer() {
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            _playerName = data.name;
            _highestScore = data.hightScore;
        }
        else ResetPlayer();
    }

    public void ResetPlayer() {
        _playerName = "Not set";
        _highestScore = 0;
        SavePlayer();
    }

    public bool IsPlayerExist() {
        if(_playerName== "Not set" || _playerName=="" ||!File.Exists(path)) {
            return false;
        }
        return true;
    }

}
