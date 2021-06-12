using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Canvas _popUpWindow;
    [SerializeField] private Cutting _cutting;
    [SerializeField] private SpawnerManager _spawnerManager;

    public void RestartGame()
    {
        _spawnerManager.StartSpawn();
        Canvas currentPopUp = GetComponentInParent<Canvas>();
        Destroy(currentPopUp.gameObject);
    }

    public void PopUpRestart()
    {
        _spawnerManager.StopSpawn();
        _cutting.BreakGame();
        Canvas currentPopUp = Instantiate(_popUpWindow, transform);
        currentPopUp.gameObject.SetActive(true);
        Image background = currentPopUp.GetComponentInChildren<Image>();
        Vector3 windowPos = currentPopUp.transform.position;
        RectTransform thisRect = currentPopUp.GetComponent<RectTransform>();
        float posY = Camera.main.transform.position.y - thisRect.rect.width;
        background.transform.position = new Vector3(windowPos.x, posY, windowPos.z);
        background.transform.DOMove(windowPos, 2.0f);
    }
}
