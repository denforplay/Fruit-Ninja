using UnityEngine;
using DG.Tweening;

public class Fruit : Block
{
    private const float PIXEL_PER_UNIT = 50;

    [SerializeField] private ParticleSystem _blobParticleSystem;
    [SerializeField] private ParticleSystem _sliceParticleSystem;
    [SerializeField] private Color _fruitColor;

    private new void Start()
    {
        _blockAnimator.IsAnimated = true;
        base.Start();
    }

    private Sprite[] GenerateHalfFruitSprite()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
            Texture2D thisTexture = _spriteRenderer.sprite.texture;
            float helperRange = thisTexture.width / Random.Range(7, 12);
            float randomXPos = Random.Range(0 + helperRange, thisTexture.width - helperRange);

            float rightPivot = (thisTexture.width - randomXPos) / thisTexture.width;
            float leftPivot = 1.0f - rightPivot;

            Rect leftFruitPart = new Rect(0, 0, randomXPos, thisTexture.height);
            Rect rightFruitPart = new Rect(randomXPos, 0, thisTexture.width - randomXPos, thisTexture.height);
            Sprite leftPart = Sprite.Create(thisTexture, leftFruitPart, Vector2.one * leftPivot, PIXEL_PER_UNIT);
            Sprite rightPart = Sprite.Create(thisTexture, rightFruitPart, Vector2.one * rightPivot, PIXEL_PER_UNIT);
            InstantiateParticles();
            return new Sprite[] { leftPart, rightPart };
        }
        else
        {
            return null;
        }
    }

    public override void Cut()
    {
        Sprite[] fruitParts = GenerateHalfFruitSprite();
        if (fruitParts != null)
        {
            _spriteRenderer.sprite = fruitParts[0];
            InstantiateRightFruitPart(fruitParts[1]);
        }
    }

    private void InstantiateParticles()
    {
        _blobParticleSystem.startColor = _fruitColor;
        ParticleSystem currentBlobs = Instantiate(_blobParticleSystem, transform);
        currentBlobs.transform.SetParent(null);
        currentBlobs.Play();
        Destroy(currentBlobs.gameObject, currentBlobs.main.duration);
    }

    private void InstantiateRightFruitPart(Sprite rightPart)
    {
        Fruit secondPart = Instantiate(this);
        secondPart._spriteRenderer = _spriteRenderer;
        secondPart._spriteRenderer.sprite = rightPart;
        secondPart.AddSpeed(GetSpeed());
        if (secondPart.GetSpeed().x > 0)
        {
            secondPart.ReverseHorizontalSpeed();
        }
        else
        {
            this.ReverseHorizontalSpeed();
        }

        secondPart._isNotCutted = false;
        _blockManager.Add(secondPart);
    }

    private void OnDestroy()
    {
        _blockManager.RemoveFruit(this);
    }
}
