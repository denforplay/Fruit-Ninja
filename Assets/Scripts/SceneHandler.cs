using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _fader;
    [SerializeField] private float _duration = 0.1f; 
    private void Start()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(new Vector3(1, 1, 1), 0);
        _fader.DOScale(Vector3.zero, _duration).OnComplete(() => _fader.gameObject.SetActive(false));
    }

    public void OpenMenuScene()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(new Vector3(1,1,1), _duration).OnComplete(() => SceneManager.LoadScene(0));
    }

    public void OpenGameScene()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(new Vector3(1, 1, 1), _duration).OnComplete(() => SceneManager.LoadScene(1));
    }
}
