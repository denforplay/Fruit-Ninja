using UnityEngine;
using TMPro;
public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;
    private string _playerHighScore = "HighScore";

    private void Start()
    {
        Player.maxScore = PlayerPrefs.GetInt(_playerHighScore);
        _scoreText.text = $"<sprite=0>{Player.score}\nBest:{Player.maxScore}";
    }

    public void AddPoint()
    {
        _score++;
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            Player.maxScore = Player.score;
            PlayerPrefs.SetInt("HighScore", _score);
        }

        _scoreText.text = $"<sprite=0>{_score}\nBest:{Player.maxScore}";
    }
}
