using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    [SerializeField] private GameObject _bladeTrail;
    private bool _isCutting;
    private GameObject _currentBladeTrial;
    Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
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

    private void UpdateCutting()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _mainCamera.nearClipPlane;
        this.transform.position = _mainCamera.ScreenToWorldPoint(mousePosition);

        CutBlocks();
    }

    private void CutBlocks()
    {
        foreach(Block block in BlockManager._allBlocks)
        {
            float colliderPositionX = this.transform.position.x - block.gameObject.transform.position.x;
            float colliderPositionY = this.transform.position.y - block.gameObject.transform.position.y;
            float colliderPoint = Mathf.Sqrt(Mathf.Pow(colliderPositionX, 2) + Mathf.Pow(colliderPositionY, 2));
            if (colliderPoint <= block.Radius)
            {
                Destroy(block.gameObject);
            }
        }
    }

    private void StartCutting()
    {
        _currentBladeTrial = Instantiate(_bladeTrail, transform);
        _isCutting = true;
    }

    private void StopCutting()
    {
        _isCutting = false;
        Destroy(_currentBladeTrial);
    }
}
