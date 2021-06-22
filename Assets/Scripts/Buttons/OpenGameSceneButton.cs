using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OpenGameSceneButton : MonoBehaviour
{
    private const int GAME_SCENE_KEY = 1;
    [SerializeField] private RectTransform _fader;
    [SerializeField] private float _duration = 2.0f;
    private Vector3 scaleTo = new Vector3(1, 1, 1);

    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public void OpenGameScene()
    {
        _button.enabled = false;
        _fader.gameObject.SetActive(true);
        _fader.DOScale(Vector3.zero, 0);
        _fader.DOScale(scaleTo, _duration).OnComplete(() =>
        SceneManager.LoadScene(GAME_SCENE_KEY));
    }
}
