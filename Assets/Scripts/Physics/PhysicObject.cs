using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    [SerializeField] private float _gravityScale;
    private Vector3 _gravityAcceleration;
    private Vector3 _speed;
    private float _topCameraPoint;


    protected void Awake()
    {
        _gravityAcceleration.y = _gravityScale;
        _topCameraPoint = Camera.main.transform.position.y + Camera.main.orthographicSize;
    }

    protected void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        while (transform.position.y >= _topCameraPoint && _speed.y > 0)
        {
            if (_gravityAcceleration.y == 0)
            {
                _gravityAcceleration = new Vector3(0, _gravityScale, 0);
            }
            _speed -= _gravityAcceleration * Time.deltaTime;
        }

        transform.position += _speed * Time.deltaTime;
        _speed -= _gravityAcceleration * Time.deltaTime;
    }

    public void AddSpeed(Vector3 speedToAddition)
    {
        _speed += speedToAddition;
    }

    public void ReverseHorizontalSpeed()
    {
        _speed.x*=-1;
    }
    
    public void ReverseVerticalSpeed()
    {
        _speed.y *= -1;
    }

    public void SetSpeed(Vector3 speedToSet)
    {
        _speed = speedToSet;
    }

    public Vector3 GetSpeed()
    {
        return _speed;
    }

    public void DisableGravity()
    {
        _gravityAcceleration = Vector3.zero;
    }

    public void EnableGravity()
    {
        _gravityAcceleration = new Vector3(0, _gravityScale, 0);
    }

    public void DisableSpeed()
    {
        _speed = Vector3.zero;
    }

    public void SlowObject(float divider)
    {
        _speed /= divider;
        _gravityAcceleration /= divider;
    }

    public void NormalizeObject(float divider)
    {
        _speed *= divider;
        _gravityAcceleration *= divider;
    }
}
