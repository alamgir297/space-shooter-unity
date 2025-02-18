using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private int _mainGameScenIndex;
    private int _mainmenuSceneIndex;
    private bool _isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _mainGameScenIndex = 1;
        _mainmenuSceneIndex = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartNewGame() {
        SceneManager.LoadScene(_mainGameScenIndex);
    }

    public void GameOver() {
        _isGameOver = true;
    }
    public bool IsGameOver() {
        return _isGameOver ? true : false;
    }
}
