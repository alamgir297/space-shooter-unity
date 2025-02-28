using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour {
    
    private bool _isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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
}
