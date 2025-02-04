using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _elapsedTime;
    void Start() {
        _liveImage.sprite = _liveSprites[3];
    }

    // Update is called once per frame
    void Update() {
        UpdateTime();
    }

    public void UpdateScore(int score) {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLive) {
        _liveImage.sprite = _liveSprites[(currentLive)];
    }

    public void UpdateTime() {
        string timeText;
        float passedTime = Time.time;
        int min = (int) passedTime / 60;
        int sec = (int)passedTime % 60;
        timeText = "" + min + ":" + sec;
        _elapsedTime.text = timeText;
    }
}
