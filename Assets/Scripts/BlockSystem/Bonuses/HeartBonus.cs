using DG.Tweening;
using UnityEngine;

public class HeartBonus : Block
{
    [SerializeField] private ParticleSystem _heartParticleCut;
    private HealthController _healthController;

    private new void Start()
    {
        _healthController = FindObjectOfType<HealthController>();
        _iScalable = new Scale();
        _iRotatable = new NoRotate();
        base.Start();
    }

    public override void Cut()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
            ParticleSystem particle = Instantiate(_heartParticleCut, transform);
            particle.transform.SetParent(null);
            Heart heart = _healthController.FindEmptyHeart();
            this.transform.DOMove(heart.transform.position, 1.0f).OnComplete(() => Destroy(gameObject));
            Destroy(particle, particle.main.duration);
        }
    }

    private void OnDestroy()
    {
        _blockManager.RemoveHeartBonus(this);
    }
}
