using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiezeManager : MonoBehaviour
{
    public int index;
    public ChessClub ch;

    public void OnPress()
    {
        ch.PiezePress(index, this.gameObject);
    }
}
