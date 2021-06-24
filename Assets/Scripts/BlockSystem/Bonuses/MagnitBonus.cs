using UnityEngine;

public class MagnitBonus : Block
{
    [SerializeField] private ParticleSystem _magnitParticle;
    private new void Update()
    {
        base.Update();
        if (!IsNotCutted)
        {
            MagniteBlocks();
        }
    }
    public override void Cut()
    {
        if (_isNotCutted)
        {
            _isNotCutted = false;
            ParticleSystem particle = Instantiate(_magnitParticle, transform);
            particle.transform.SetParent(null);
            DisableAllBlocksGravity();
            this.DisableGravity();
            this.DisableSpeed();
            Destroy(particle.gameObject, particle.main.duration);
            Destroy(this.gameObject, particle.main.duration);
        }
    }

    public void MagniteBlocks()
    {
        Vector3 magnitePosition = this.transform.position;
        foreach (var block in _blockManager.allBlocks)
        {
            if (block != (block as MagnitBonus))
            {
                Vector3 blockPosition = block.transform.position;
                float deltaY = magnitePosition.y - blockPosition.y;
                float deltaX = magnitePosition.x - blockPosition.x;
                float tgalpha = deltaY / deltaX;
                if (block.GetSpeed().x > 0 && deltaX < -Radius || block.GetSpeed().x < 0 && deltaX > Radius)
                {
                    block.ReverseHorizontalSpeed();
                }

                if (block.GetSpeed().y < 0 && deltaY > Radius || block.GetSpeed().y > 0 && deltaY < Radius)
                {
                    block.ReverseVerticalSpeed();
                }

                float newYSpeed = block.GetSpeed().x * tgalpha;
                block.SetSpeed(new Vector3(block.GetSpeed().x, newYSpeed));
            }
        }
    }

    public void DisableAllBlocksGravity()
    {
        foreach (var block in _blockManager.allBlocks)
        {
            if (block != (block as MagnitBonus))
            {
                block.DisableGravity();
            }
        }
    }

    private void OnDestroy()
    {
        if (!this.IsNotCutted)
        {
            foreach (var block in _blockManager.allBlocks)
            {
                if (block != (block as MagnitBonus))
                {
                    block.EnableGravity();
                }
            }
        }

        _blockManager.RemoveMagnitBonus(this);
    }
}
