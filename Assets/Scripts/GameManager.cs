using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Vector3 mousePos { get; private set; }
   
    public void setMousePos(Vector3 pos)
    {
        mousePos = pos;
    }
}
