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
            Heart heart = _healthViewController.FindEmptyHeart();
            Destroy(gameObject);
            Destroy(particle, particle.main.duration);
        }
    }

    private void OnDestroy()
    {
        _blockManager.RemoveHeartBonus(this);
    }
}
