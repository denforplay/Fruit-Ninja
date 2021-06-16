using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Canvas _popUpWindow;
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private Sprite _noHeartSprite;
    [SerializeField] private Sprite _heartSprite;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private Player _player;
    private List<Heart> _hearts;

    public int HeartsCount => _hearts.Count;
    private void DeleteAllHurts()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            Destroy(_hearts[i].gameObject);
        }

        _hearts.Clear();
    }

    public void DeleteHeart()
    {
        int i;
        for (i = _hearts.Count - 1; i >= 0; i--)
        {
            if (_hearts[i].IsHeart == true)
            {
                _hearts[i].Cut();
                _hearts[i].GetSpriteRenderer.sprite = _noHeartSprite;
                _player.health--;
                break;
            }
        }

        if (i <= 0)
        {
            DeleteAllHurts();
            _sceneController.PopUpRestart();
        }
    }

    public void InstantiateHearts()
    {
        _hearts = new List<Heart>();
        for (int i = 1; i <= _player.maxhealth; i++)
        {
            Heart heart = Instantiate(_heartPrefab);
            float xPos = Camera.main.transform.position.x + Camera.main.orthographicSize * 2 - 3 * heart.Radius * i;
            float yPos = Camera.main.transform.position.y + Camera.main.orthographicSize - heart.Radius;
            Vector3 heartPosition = new Vector3(xPos, yPos);
            heart.transform.position = heartPosition;
            _hearts.Add(heart);
        }
    }

    public void AddHeart()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (_hearts[i].GetSpriteRenderer.sprite == _noHeartSprite)
            {
                _hearts[i].GetSpriteRenderer.sprite = _heartSprite;
                _hearts[i].SetHeartActive();
                _player.health++;
                break;
            }
        }
    }

    public Heart FindEmptyHeart()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (_hearts[i].GetSpriteRenderer.sprite == _noHeartSprite)
            {
                return _hearts[i];
            }
        }

        return null;
    }

}
