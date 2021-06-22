using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    private const int GAME_SCENE_ID = 1;

    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private RectTransform _fader;
    [SerializeField] private float _duration = 2.0f;
    private Vector3 scaleTo = new Vector3(1, 1, 1);

    private void Start()
    {
        _fader.gameObject.SetActive(true);
        _fader.DOScale(scaleTo, 0);
        if (SceneManager.GetActiveScene().buildIndex == GAME_SCENE_ID)
        {
            _fader.DOScale(Vector3.zero, _duration).OnComplete(() => _spawnerManager.StartGame());
        }
        else
        {
            _fader.DOScale(Vector3.zero, _duration);
        }
    }
}
