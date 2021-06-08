using UnityEngine;

public class Block : PhysicObject
{
    Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (!_renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        
    }
}
