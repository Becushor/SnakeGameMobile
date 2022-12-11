using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    const float Width = 3.7f;
    const float Height = 7f;

    public float snakeSpeed = 1;

    public GameObject rockPrefab = null;

    public BodyPart bodyPrefab = null;
    public SnakeHead snakeHead = null;

    public Sprite tailSprite = null;
    public Sprite bodySprite = null;

    void Start()
    {
        instance = this;
        CreateWalls();
        StartGame();
    }

    void Update()
    {
        
    }

    void StartGame()
    {
        snakeHead.ResetSnake();
    }

    void CreateWalls()
    {
        Vector3 start = new Vector3(-Width, -Height, 0);
        Vector3 finish = new Vector3(-Width, Height, 0);
        CreateWall(start, finish);
        
        start = new Vector3(Width, -Height, 0);
        finish = new Vector3(Width, Height, 0);
        CreateWall(start, finish);

        start = new Vector3(-Width, -Height, 0);
        finish = new Vector3(Width, -Height, 0);
        CreateWall(start, finish);

        start = new Vector3(-Width, Height, 0);
        finish = new Vector3(Width, Height, 0);
        CreateWall(start, finish);
    }

    void CreateWall(Vector3 start, Vector3 finish)
    {
        float distance = Vector3.Distance(start, finish);
        int numberOfRocks = (int)(distance * 3f);
        Vector3 delta = (finish - start) / numberOfRocks;

        Vector3 position = start;

        for (int i = 0; i <= numberOfRocks; i++)
        {
            float scale = Random.Range(1.5f, 2f);
            float rotation = Random.Range(0, 360f);
            CreateRock(position, scale, rotation);
            position += delta;
        }
    }

    void CreateRock(Vector3 position, float scale, float rotation)
    {
        GameObject rock = Instantiate(rockPrefab, position, Quaternion.Euler(0, 0, rotation));
        rock.transform.localScale = new Vector3(scale, scale, 1);
    }
}
