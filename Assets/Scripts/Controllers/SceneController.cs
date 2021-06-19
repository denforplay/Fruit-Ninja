using DG.Tweening;
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

    public void RestartGame()
    {
        _scoreController.Start();
        _spawnerManager.StartSpawn();
        Canvas currentPopUp = GetComponentInParent<Canvas>();
        Destroy(currentPopUp.gameObject);
    }

    public void PopUpRestart()
    {
        _spawnerManager.StopSpawn();
        _cutting.BreakGame();
        if (_blockManager.allBlocks.Count == 0)
        {
            Canvas currentPopUp = Instantiate(_popUpWindow, transform);
            currentPopUp.gameObject.SetActive(true);
            Image background = currentPopUp.GetComponentInChildren<Image>();
            Vector3 windowPos = currentPopUp.transform.position;
            RectTransform thisRect = currentPopUp.GetComponent<RectTransform>();
            float posY = Camera.main.transform.position.y - thisRect.rect.width;
            background.transform.position = new Vector3(windowPos.x, posY, windowPos.z);
            DOTween.KillAll();
            background.transform.DOMove(windowPos, 2.0f);
        }
    }

    private void OnEnable()
    {
        _healthViewController.AllLifesDeletedEvent += PopUpRestart;
    }
}
