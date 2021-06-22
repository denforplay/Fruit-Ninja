using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Canvas _popUpWindow;
    [SerializeField] private Cutting _cutting;
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private BlockManager _blockManager;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private HealthViewController _healthViewController;

    Vector3 _startPosition;
    Canvas _currentPopUp;
    public void RestartGame()
    {
        Destroy(_currentPopUp.gameObject);
        _scoreController.Start();
        _spawnerManager.StartSpawn();
    }

    public void PopUpRestart()
    {
        _spawnerManager.StopSpawn();
        _cutting.BreakGame();
        if (_blockManager.allBlocks.Count == 0)
        {
            _currentPopUp = Instantiate(_popUpWindow, transform);
            _currentPopUp.gameObject.SetActive(true);
            Image background = _currentPopUp.GetComponentInChildren<Image>();
            Vector3 windowPos = _currentPopUp.transform.position;
            RectTransform thisRect = _currentPopUp.GetComponent<RectTransform>();
            float posY = Camera.main.transform.position.y - thisRect.rect.width;
            _startPosition = new Vector3(windowPos.x, posY, windowPos.z);
            background.transform.position = _startPosition;
            background.transform.DOMove(windowPos, 2.0f).OnComplete(() => DOTween.Kill(background.transform));
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
