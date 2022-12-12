using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    const float Width = 3.7f;
    const float Height = 7f;

    public float snakeSpeed = 1;

    public bool alive = true;
    public bool waitingToPlay = true;

    public GameObject rockPrefab = null;
    public GameObject eggPrefab = null;
    public GameObject goldEggPrefab = null;

    public BodyPart bodyPrefab = null;
    public SnakeHead snakeHead = null;

    public Sprite tailSprite = null;
    public Sprite bodySprite = null;

    List<Egg> eggs = new List<Egg>();

    void Start()
    {
        instance = this;
        CreateWalls();
        CreateEgg();
        alive = false;
    }

    void Update()
    {
        if (waitingToPlay)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended) //press on the screen just ended
                    StartGamePlay();
            }
            if (Input.GetMouseButtonDown(0))
                StartGamePlay();
        }
    }

    public void StartGamePlay()
    {
        waitingToPlay = false;
        alive = true;

        KillOldEggs();
        snakeHead.ResetSnake();
    }

    public void GameOver()
    {
        alive = false;
        waitingToPlay = true;
    }

    public void EggEaten(Egg egg)
    {
        Destroy(egg.gameObject);
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

        Egg egg = null;

        if (isGolden)
            egg = Instantiate(goldEggPrefab, position, Quaternion.identity).GetComponent<Egg>();
        else
            egg = Instantiate(eggPrefab, position, Quaternion.identity).GetComponent<Egg>();

        eggs.Add(egg);
    }

    void KillOldEggs()
    {
        foreach (Egg egg in eggs)
        {
            Destroy(egg.gameObject);
        }
        eggs.Clear();
    }
}
