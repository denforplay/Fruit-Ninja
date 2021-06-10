using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    [SerializeField] private GameObject _bladeTrail;
    private bool isCutting;
    private GameObject _currentBladeTrial;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
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

        if (isCutting)
        {
            UpdateCutting();
        }
    }

    private void UpdateCutting()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cam.nearClipPlane;
        this.transform.position = cam.ScreenToWorldPoint(mousePosition);

        CutBlocks();
    }

     //float colliderPositionX = this.transform.position.x - block.gameObject.transform.position.x;
     //       float colliderPositionY = this.transform.position.y - block.gameObject.transform.position.y;
     //       float colliderPoint = Mathf.Sqrt(Mathf.Pow(colliderPositionX, 2) + Mathf.Pow(colliderPositionY, 2));
     //       if (colliderPoint <= block.Radius)
     //       {
     //           Debug.Log("Fruit is slicing");
     //           Destroy(block.gameObject);
     //       }
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
        isCutting = true;
    }

    private void StopCutting()
    {
        isCutting = false;
        Destroy(_currentBladeTrial);
    }
}
