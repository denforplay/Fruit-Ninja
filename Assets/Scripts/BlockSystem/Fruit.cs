using UnityEngine;
using DG.Tweening;

public class Fruit : Block
{
    [SerializeField] private ParticleSystem _blobParticleSystem;
    [SerializeField] private ParticleSystem _sliceParticleSystem;
    [SerializeField] private Color _fruitColor;

    private new void Awake()
    {
        base.Awake();
        _iRotatable = new Rotate();
        _iScalable = new Scale();
    }

    private Sprite[] GenerateHalfFruitSprite()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
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
        ParticleSystem currentCut = Instantiate(_sliceParticleSystem, transform);
        ParticleSystem currentBlobs = Instantiate(_blobParticleSystem, transform);
        currentBlobs.transform.SetParent(null);
        currentBlobs.Play();
        currentCut.Play();
        currentCut.transform.SetParent(null);
        Destroy(currentCut.gameObject, currentCut.main.duration);
        Destroy(currentBlobs.gameObject, 5.0f);
    }

    private void InstantiateRightFruitPart(Sprite rightPart)
    {
        Fruit secondPart = Instantiate(this);
        secondPart._spriteRenderer = _spriteRenderer;
        secondPart._spriteRenderer.sprite = rightPart;
        secondPart.AddSpeed(GetSpeed());
        secondPart.ReverseHorizontalSpeed();
        secondPart._isNotCutted = false;
    }

    public void RotateObject()
    {
        transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360);

    }

    public void ScaleObject()
    {
        transform.DOScale(new Vector3(0.5f, 0.5f), 5f);
    }
}
