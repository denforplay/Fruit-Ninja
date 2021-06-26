using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    const string _yourResult = "Your result\n      {0}\nBest\n      {1}";
    [SerializeField] private TextMeshProUGUI _playerScoreResult;
    [SerializeField] private Canvas _popUpWindow;
    [SerializeField] private Cutting _cutting;
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private BlockManager _blockManager;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private HealthViewController _healthViewController;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Player _player;

    Vector3 _startPosition;
    Canvas _currentPopUp;
    public void RestartGame()
    {
        Image background = _currentPopUp.GetComponentInChildren<Image>();
        background.transform.DOMove(_startPosition, _duration).OnComplete(() =>
        {
            Destroy(_currentPopUp.gameObject);
            _scoreController.Start();
            _spawnerManager.StartSpawn();
        });

    }

    public void PopUpRestart()
    {
        _spawnerManager.StopSpawn();
        _cutting.BreakGame();
        if (_blockManager.allBlocks.Count == 0)
        {
            _playerScoreResult.text = string.Format(_yourResult, _player.score, _player.maxScore);
            _currentPopUp = Instantiate(_popUpWindow, transform);
            _currentPopUp.gameObject.SetActive(true);
            Image[] background = _currentPopUp.GetComponentsInChildren<Image>();
            Vector3 windowPos = _currentPopUp.transform.position;
            RectTransform thisRect = _currentPopUp.GetComponent<RectTransform>();
            float posY = Camera.main.transform.position.y - thisRect.rect.width;
            _startPosition = new Vector3(windowPos.x, posY, windowPos.z);
            background[0].transform.position = _startPosition;
            background[0].transform.DOMove(windowPos, _duration).OnComplete(() => DOTween.Kill(background[0].transform));
        }
    }

    private void OnEnable()
    {
        try
        {
            _healthViewController.AllLifesDeletedEvent += PopUpRestart;
        }
        catch (NullReferenceException)
        {
        }
    }
}
