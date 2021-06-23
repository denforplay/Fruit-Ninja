using DG.Tweening;
using UnityEngine;

public class HeartBonus : Block
{
    [SerializeField] private ParticleSystem _heartParticleCut;
    private HealthViewController _healthViewController;
    private new void Start()
    {
        _healthViewController = FindObjectOfType<HealthViewController>();
        base.Start();
    }

    public override void Cut()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
            ParticleSystem particle = Instantiate(_heartParticleCut, transform);
            particle.transform.SetParent(null);
            Destroy(particle, particle.main.duration);
            Heart heart = _healthViewController.FindEmptyHeart();
            transform.DOMove(heart.transform.position, 0.5f).OnComplete(() => Destroy(gameObject));
        }
    }

    private void OnDestroy()
    {
        _blockManager.RemoveHeartBonus(this);
    }
}
