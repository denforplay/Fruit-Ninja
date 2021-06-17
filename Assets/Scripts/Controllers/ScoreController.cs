using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Player _player;
    private string _playerHighScore = "HighScore";
    public void Start()
    {
        _player.score = 0;
        _player.maxScore = PlayerPrefs.GetInt(_playerHighScore);
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            _scoreText.text = $"High Score:\n{_player.maxScore}";
        }
        else
        {
            _scoreText.text = $"<sprite=0>{_player.score}\nBest:{_player.maxScore}";
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
