using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
