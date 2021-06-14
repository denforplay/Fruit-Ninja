using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _fader;

    private void Start()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(new Vector3(1, 1, 1), 0);
        _fader.DOScale(Vector3.zero, 0.5f).OnComplete(() => _fader.gameObject.SetActive(false));
    }

    public void OpenMenuScene()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(new Vector3(1,1,1), 0.5f).OnComplete(() => SceneManager.LoadScene(0));
    }

    public void OpenGameScene()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => SceneManager.LoadScene(1));
    }
}
