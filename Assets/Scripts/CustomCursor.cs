using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorArrow;

    private void Start()
    {
        Cursor.SetCursor(_cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

    }
}
