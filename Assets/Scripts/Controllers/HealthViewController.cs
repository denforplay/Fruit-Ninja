using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthViewController : MonoBehaviour
{
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private Sprite _noHeartSprite;
    [SerializeField] private Sprite _heartSprite;
    [SerializeField] private HealthController _healthController;

    private List<Heart> _hearts;

    public Action DeleteLifeEvent;
    public Action AllLifesDeletedEvent;
    public Action AddLifeEvent;

    public int HeartsCount => _hearts.Count(x => x.IsHeart);
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
                DeleteLifeEvent.Invoke();
                break;
            }
        }

        if (i <= 0)
        {
            AllLifesDeletedEvent.Invoke();
        }
    }

    public void InstantiateHearts()
    {
        _hearts = new List<Heart>();
        for (int i = 1; i <= _healthController.PlayerMaxHealth; i++)
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
                AddLifeEvent.Invoke();
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

