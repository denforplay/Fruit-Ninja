using UnityEngine;

public class Fruit : Block
{

    [SerializeField] private ParticleSystem _blobParticleSystem;
    [SerializeField] private ParticleSystem _sliceParticleSystem;

    private SpriteRenderer _spriteRenderer;
    private bool isNotCutted = true;
    private Color _fruitColor;
    public bool IsNotCutted => isNotCutted;
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
            InstantiateParticles();
            return new Sprite[] { leftPart, rightPart };
        }
        else
        {
            return null;
        }
    }

    public void CutFruit(BlockManager blockManager)
    {
        Sprite[] fruitParts = GenerateHalfFruitSprite();
        if (fruitParts != null)
        {
            blockManager.Remove(this);
            _spriteRenderer.sprite = fruitParts[0];
            InstantiateRightFruitPart(fruitParts[1]);
        }
    }

    private void InstantiateParticles()
    {
        _blobParticleSystem.startColor = _fruitColor;
        ParticleSystem currentCut = Instantiate(_sliceParticleSystem, transform);
        ParticleSystem currentBlobs = Instantiate(_blobParticleSystem, transform);
        currentBlobs.transform.SetParent(null);
        currentBlobs.Play();
        currentCut.transform.SetParent(null);
        Destroy(currentCut.gameObject, currentCut.main.duration);
        Destroy(currentBlobs.gameObject, 5.0f);
    }

    private void InstantiateRightFruitPart(Sprite rightPart)
    {
        Fruit secondPart = Instantiate(this);
        secondPart._spriteRenderer = this._spriteRenderer;
        secondPart._spriteRenderer.sprite = rightPart;
        secondPart.AddSpeed(this.GetSpeed());
        secondPart.ReverseHorizontalSpeed();
        secondPart._spriteRenderer.sprite = rightPart;
    }
}
