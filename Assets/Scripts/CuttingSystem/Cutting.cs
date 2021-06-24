using UnityEngine;

public class Cutting : MonoBehaviour
{
    [SerializeField] private GameObject _bladeTrail;
    [SerializeField] private float _minCutVelocity = 0.1f;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private BlockManager _blockManager;

    private bool _isPlaying = true;
    private bool _isCutting;
    private GameObject _currentBladeTrial;
    Camera _mainCamera;
    private Vector2 _previousPosition;
    private Vector2 _defaultPosition = new Vector2(0, 0);

    private void Start()
    {
        _mainCamera = Camera.main;
        _previousPosition = _defaultPosition;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCutting();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopCutting();
            }

            if (_isCutting)
            {
                UpdateCutting();
            }
        }
    }

    private void UpdateCutting()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _mainCamera.nearClipPlane;
        Vector2 newPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        float velocity = (newPosition - _previousPosition).magnitude * Time.deltaTime;
        this.transform.position = newPosition;
        if (velocity > _minCutVelocity && _previousPosition != _defaultPosition)
        {
            CutBlocks();
        }

        _previousPosition = newPosition;
    }

    private void CutBlocks()
    {
        foreach(Block block in _blockManager.allBlocks)
        {
            float colliderPositionX = this.transform.position.x - block.gameObject.transform.position.x;
            float colliderPositionY = this.transform.position.y - block.gameObject.transform.position.y;
            float colliderPoint = Mathf.Sqrt(Mathf.Pow(colliderPositionX, 2) + Mathf.Pow(colliderPositionY, 2));
            if (colliderPoint <= block.Radius)
            {
                if (block is Fruit && block.IsNotCutted)
                {
                    _scoreController.AddPoint(block);
                }

                block.Cut();
                break;
            }
        }
    }

    private void StartCutting()
    {
        _currentBladeTrial = Instantiate(_bladeTrail, transform);
        _isCutting = true;
    }

    public void StopCutting()
    {
        _isCutting = false;
        _previousPosition = _defaultPosition;
        Destroy(_currentBladeTrial);
    }

    public void StartGame()
    {
        _isPlaying = true;
    }

    public void BreakGame()
    {
        this.StopCutting();
        _isPlaying = false;
    }
}
