using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public float snakeSpeed = 0.5f;

    public BodyPart bodyPrefab = null;
    public SnakeHead snakeHead = null;

    public Sprite tailSprite = null;
    public Sprite bodySprite = null;

    void Start()
    {
        instance = this;
        StartGame();
    }

    void Update()
    {
        
    }

    void StartGame()
    {
        snakeHead.ResetSnake();
    }
}
