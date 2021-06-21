using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneHandler : MonoBehaviour
{
    private Vector3 scaleTo = new Vector3(1, 1, 1);

    [SerializeField] private RectTransform _fader;
    [SerializeField] private float _duration = 2.0f;

    private void Start()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(scaleTo, 0);
        _fader.DOScale(Vector3.zero, _duration);
    }

    public void OpenMenuScene()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(scaleTo, _duration).OnComplete(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });
    }

    public void OpenGameScene()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(scaleTo, _duration).OnComplete(() =>
            UnityEngine.SceneManagement.SceneManager.LoadScene(1));
    }
}
