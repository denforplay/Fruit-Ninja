using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private string _playerHighScore = "HighScore";
    private void Start()
    {
        Player.maxScore = PlayerPrefs.GetInt(_playerHighScore);
        _scoreText.text = $"<sprite=0>{Player.score}\nBest:{Player.maxScore}";
    }

    public void AddPoint()
    {
        Player.score++;
        if (Player.score >= PlayerPrefs.GetInt(_playerHighScore))
        {
            Player.maxScore = Player.score;
            PlayerPrefs.SetInt(_playerHighScore, Player.maxScore);
        }

        _scoreText.text = $"<sprite=0>{Player.score}\nBest:{Player.maxScore}";
    }
}
