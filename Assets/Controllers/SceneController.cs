using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Canvas _popUpWindow;
    [SerializeField] private Cutting _cutting;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Canvas currentPopUp = GetComponentInParent<Canvas>();
        Destroy(currentPopUp.gameObject);
        Player.health = 3;
    }

    public void PopUpRestart()
    {
        _cutting.OffCutting();
        Canvas currentPopUp = Instantiate(_popUpWindow, transform);
        Image background = currentPopUp.GetComponentInChildren<Image>();
        Vector3 windowPos = currentPopUp.transform.position;
        RectTransform thisRect = currentPopUp.GetComponent<RectTransform>();
        float posY = Camera.main.transform.position.y - thisRect.rect.width;
        background.transform.position = new Vector3(windowPos.x, posY, windowPos.z);
        background.transform.DOMove(windowPos, 2.0f);
        DOTween.Kill(currentPopUp.transform);
    }
}
