using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public float snakeSpeed = 1;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
