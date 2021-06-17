using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Block
{
    [SerializeField] private ParticleSystem _bombParticle;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionSpeed;

    public float ExplosionRadius => _explosionRadius;

    private new void Start()
    {
        _iRotatable = new Rotate();
        _iScalable = new Scale();
        base.Start();
    }

    public override void Cut()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
            ExplodeCollision();
            var particle = Instantiate(_bombParticle, transform);
            particle.transform.SetParent(null);
            Destroy(particle.gameObject, particle.main.duration);
            Destroy(gameObject);
        }
    }

    private void ExplodeCollision()
    {
        foreach (Block blockToMove in _blockManager.allBlocks)
        {
            float colliderPositionX = blockToMove.gameObject.transform.position.x - this.transform.position.x;
            float colliderPositionY = blockToMove.gameObject.transform.position.y - this.transform.position.y;
            float colliderPoint = Mathf.Sqrt(Mathf.Pow(colliderPositionX, 2) + Mathf.Pow(colliderPositionY, 2));
            if (colliderPoint <= ExplosionRadius)
            {
                float newExplosionSpeed = (ExplosionRadius - colliderPoint) / ExplosionRadius * _explosionSpeed;
                float xSpeed = (colliderPositionX) * newExplosionSpeed;
                float ySpeed = (colliderPositionY) * newExplosionSpeed;
                blockToMove.AddSpeed(new Vector3(xSpeed, ySpeed));
            }
        }
    }
}
