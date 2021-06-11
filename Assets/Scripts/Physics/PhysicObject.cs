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

    public Vector3 GetSpeed()
    {
        return _speed;
    }
}
