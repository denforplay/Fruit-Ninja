using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    const string startSceneName = "StartScene";
    const string _highScoreFormat = "High Score:\n{0}";
    const string _highBestScoreFormat = "<sprite=0>{0}\nBest:{1}";

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Player _player;
    [SerializeField] private int _pointsToAdd;
    [SerializeField] private TextMesh _scorePopUp;
    [SerializeField] private float _duration = 2.0f;

    private string _playerHighScore = "HighScore";
    public void Start()
    {
        _player.score = 0;
        _player.maxScore = PlayerPrefs.GetInt(_playerHighScore);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == startSceneName)
        {
            _scoreText.text = string.Format(_highScoreFormat, _player.maxScore);
        }
        else
        {
            var position = _scoreText.transform.position;
            _scoreText.text = string.Format(_highBestScoreFormat, _player.score, _player.maxScore);
        }
    }

    public void AddPoint(Block block)
    {
        _player.score += block.GetCost;
        var scorePopUp = Instantiate(_scorePopUp);
        scorePopUp.transform.position = block.transform.position;
        scorePopUp.text = $"{block.GetCost}";
        DOTween.ToAlpha(() => scorePopUp.color, x => scorePopUp.color = x, 0, _duration);
        Destroy(scorePopUp.gameObject, _duration);
        if (_player.score >= PlayerPrefs.GetInt(_playerHighScore))
        {
            _player.maxScore = _player.score;
            PlayerPrefs.SetInt(_playerHighScore, _player.maxScore);
        }

        _scoreText.text = $"<sprite=0>{_player.score}\nBest:{_player.maxScore}";
    }
}
