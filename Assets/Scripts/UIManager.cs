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
    void Start() {
        _liveImage.sprite = _liveSprites[3];
    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateScore(int score) {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLive) {
        _liveImage.sprite = _liveSprites[(currentLive)];
    }
}
