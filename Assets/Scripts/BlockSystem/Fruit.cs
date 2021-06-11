using UnityEngine;

public class Fruit : Block
{
    private SpriteRenderer _spriteRenderer;
    private bool isNotCutted = true;
    [SerializeField] private Fruit _prefab;
    [SerializeField] private ParticleSystem _particleSystem;
    private Color _fruitColor;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _fruitColor = _spriteRenderer.color;
    }

    private Sprite[] GenerateHalfFruitSprite()
    {
        if (isNotCutted)
        {
            isNotCutted = false;
            Texture2D thisTexture = _spriteRenderer.sprite.texture;
            Rect leftFruitPart = new Rect(0, 0, thisTexture.width / 2, thisTexture.height);
            Rect rightFruitPart = new Rect(thisTexture.width / 2, 0, thisTexture.width / 2, thisTexture.height);
            Sprite leftPart = Sprite.Create(thisTexture, leftFruitPart, Vector2.one * 0.5f, 50);
            Sprite rightPart = Sprite.Create(thisTexture, rightFruitPart, Vector2.one * 0.5f, 50);
            return new Sprite[] { leftPart, rightPart };
        }
        else
        {
            return null;
        }
    }

    public void CutFruit()
    {
        Sprite[] fruitParts = GenerateHalfFruitSprite();
        if (fruitParts != null)
        {
            _spriteRenderer.sprite = fruitParts[0];
            Fruit secondPart = Instantiate(this);
            secondPart._spriteRenderer = this._spriteRenderer;
            secondPart._spriteRenderer.sprite = fruitParts[1];
            secondPart.AddSpeed(this.GetSpeed());
            secondPart.ReverseHorizontalSpeed();
            secondPart._spriteRenderer.sprite = fruitParts[1];
            _particleSystem.startColor = _fruitColor;
            ParticleSystem weqwe = Instantiate(_particleSystem, transform);
            weqwe.transform.SetParent(null);
            weqwe.Play();
            Destroy(weqwe.gameObject, 5.0f);
        }
    }
}
