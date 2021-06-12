using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Canvas _popUpWindow;
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private Sprite _noHeartPrefab;
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private SceneController _sceneController;
    private List<Heart> _hearts;

    void Start()
    {
        InstantiateHearts();
    }

    public void DeleteHeart()
    {
        int i;
        for (i = _hearts.Count - 1; i >= 0; i--)
        {
            if (_hearts[i].IsHeart == true)
            {
                _hearts[i].DeleteHeart();
                _hearts[i].GetSpriteRenderer.sprite = _noHeartPrefab;
                if (i == 0)
                {
                    _spawnerManager.StopSpawn();
                    _sceneController.PopUpRestart();
                }
                break;
            }
        }
    }

    private void InstantiateHearts()
    {
        _hearts = new List<Heart>();
        for (int i = 0; i < Player.health; i++)
        {
            Heart heart = Instantiate(_heartPrefab);
            float xPos = Camera.main.transform.position.x + Camera.main.orthographicSize * 2 - 2 * heart.Radius * i;
            float yPos = Camera.main.transform.position.y + Camera.main.orthographicSize - heart.Radius;
            Vector3 heartPosition = new Vector3(xPos, yPos);
            heart.transform.position = heartPosition;
            _hearts.Add(heart);
        }
    }
}
