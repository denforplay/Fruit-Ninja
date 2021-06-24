using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : Block
{
    [SerializeField] private ParticleSystem _freezeParticle;
    [SerializeField] private float _freeseForce = 3f;

    public override void Cut()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
            ParticleSystem particle = Instantiate(_freezeParticle, transform);
            particle.transform.SetParent(null);
            FreeseFruits();
            this._spriteRenderer.color = new Color(0,0,0,0);
            Destroy(particle.gameObject, particle.main.duration);
            Destroy(this.gameObject, particle.main.duration);
        }
    }

    public void FreeseFruits()
    {
        foreach(var block in _blockManager.allBlocks)
        {
            block.isSlowed = true;
            block.SlowObject(_freeseForce);
        }
    }

    private void OnDestroy()
    {
        foreach (var block in _blockManager.allBlocks)
        {
            if (block.isSlowed)
            block.NormalizeObject(_freeseForce);
        }

        _blockManager.RemoveFreezeBonus(this);
    }
}
