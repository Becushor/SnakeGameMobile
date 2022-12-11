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
    public GameObject eggPrefab = null;
    public GameObject goldEggPrefab = null;

    public BodyPart bodyPrefab = null;
    public SnakeHead snakeHead = null;

    public Sprite tailSprite = null;
    public Sprite bodySprite = null;

    void Start()
    {
        instance = this;
        CreateWalls();
        StartGame();
        CreateEgg();
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
        float z = -1f;

        Vector3 start = new Vector3(-Width, -Height, z);
        Vector3 finish = new Vector3(-Width, Height, z);
        CreateWall(start, finish);
        
        start = new Vector3(Width, -Height, z);
        finish = new Vector3(Width, Height, z);
        CreateWall(start, finish);

        start = new Vector3(-Width, -Height, z);
        finish = new Vector3(Width, -Height, z);
        CreateWall(start, finish);

        start = new Vector3(-Width, Height, z);
        finish = new Vector3(Width, Height, z);
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

    void CreateEgg(bool isGolden = false)
    {
        Vector3 position;
        position.x = -Width + Random.Range(1f, (Width * 2) - 2f);
        position.y = -Height + Random.Range(1f, (Height * 2) - 2f);
        position.z = -1f;

        if (isGolden)
            Instantiate(goldEggPrefab, position, Quaternion.identity);
        else
            Instantiate(eggPrefab, position, Quaternion.identity);
    }
}
