using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _fader;
    [SerializeField] private float _duration = 2.0f;
    private Vector3 scaleTo = new Vector3(1, 1, 1);

    private void Start()
    {

        _fader.gameObject.SetActive(true);
        _fader.DOScale(scaleTo, 0);
        _fader.DOScale(Vector3.zero, _duration);
    }
}
