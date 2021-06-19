using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreController : MonoBehaviour
{
    const string startSceneName = "StartScene";
    const string _highScoreFormat = "High Score:\n{0}";
    const string _highBestScoreFormat = "<sprite=0>{0}\nBest:{1}";

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Player _player;

    private string _playerHighScore = "HighScore";
    public void Start()
    {
        _player.score = 0;
        _player.maxScore = PlayerPrefs.GetInt(_playerHighScore);
        if (SceneManager.GetActiveScene().name == startSceneName)
        {
            _scoreText.text = string.Format(_highScoreFormat, _player.maxScore);
        }
        else
        {
            _scoreText.text = string.Format(_highBestScoreFormat, _player.score, _player.maxScore);
        }
    }

    public void AddPoint()
    {
        _player.score++;
        if (_player.score >= PlayerPrefs.GetInt(_playerHighScore))
        {
            _player.maxScore = _player.score;
            PlayerPrefs.SetInt(_playerHighScore, _player.maxScore);
        }

        _scoreText.text = $"<sprite=0>{_player.score}\nBest:{_player.maxScore}";
    }
}
