using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ChangeMouse : MonoBehaviour
{
    public Texture2D _prevSprite;
    public Texture2D _sprite;

    private void Start()
    {
        Cursor.SetCursor(_prevSprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(_sprite, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(_prevSprite, Vector2.zero, CursorMode.Auto);
    }
}
    