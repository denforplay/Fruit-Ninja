using System.Collections.Generic;
using UnityEngine;
public class HealthController : MonoBehaviour
{
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private Sprite _noHeartPrefab;
    private List<Heart> _hearts;

    void Start()
    {
        InstantiateHearts();
    }

    void Update()
    {
        
    }

    public void DeleteHeart()
    {
        for (int i = _hearts.Count - 1; i >= 0; i--)
        {
            if (_hearts[i].IsHeart == true)
            {
                _hearts[i].DeleteHeart();
                _hearts[i].GetSpriteRenderer.sprite = _noHeartPrefab;
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
            float xPos = Camera.main.transform.position.x + Camera.main.orthographicSize - 2 * heart.Radius * i;
            float yPos = Camera.main.transform.position.y + Camera.main.orthographicSize - heart.Radius;
            Vector3 heartPosition = new Vector3(xPos, yPos);
            heart.transform.position = heartPosition;
            _hearts.Add(heart);
        }
    }
}
