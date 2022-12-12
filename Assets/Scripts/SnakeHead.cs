using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : BodyPart
{
    Vector2 movement;

    private BodyPart tail = null;

    List<BodyPart> parts = new List<BodyPart>();

    public int partsToAdd = 0;

    const float TimeToAddBodyPart = 0.1f;
    float addTimer = TimeToAddBodyPart;

    void Start()
    {
        SwipeControls.OnSwipe += SwipeDetection;
    }

    override public void Update()
    {
        if (!GameController.instance.alive) return;

        base.Update();

        SetMovement(movement);
        UpdateDirection();
        UpdatePosition();

        if (partsToAdd > 0)
        {
            addTimer -= Time.deltaTime;
            if (addTimer <= 0)
            {
                addTimer = TimeToAddBodyPart;
                AddBodyPart();
                partsToAdd--;
            }
        }
    }

    void AddBodyPart()
    {
        if (tail == null)
        {
            Vector3 newPosition = transform.position;
            newPosition.z += 0.01f;

            BodyPart newPart = Instantiate(GameController.instance.bodyPrefab, newPosition, Quaternion.identity);
            newPart.following = this;
            tail = newPart;
            newPart.TurnIntoTail();

            parts.Add(newPart);
        }
        else
        {
            Vector3 newPosition = tail.transform.position;
            newPosition.z += 0.01f;

            BodyPart newPart = Instantiate(GameController.instance.bodyPrefab, newPosition, tail.transform.rotation);
            newPart.following = tail;
            newPart.TurnIntoTail();
            tail.TurnIntoBodyPart();
            tail = newPart;

            parts.Add(newPart);
        }
    }

    void SwipeDetection(SwipeControls.SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeControls.SwipeDirection.Up:
                MoveUp();
                break;
            case SwipeControls.SwipeDirection.Down:
                MoveDown();
                break;
            case SwipeControls.SwipeDirection.Left:
                MoveLeft();
                break;
            case SwipeControls.SwipeDirection.Right:
                MoveRight();
                break;
        }
    }

    void MoveUp()
    {
        movement = GameController.instance.snakeSpeed * Time.deltaTime * Vector2.up;
    }

    void MoveDown()
    {
        movement = GameController.instance.snakeSpeed * Time.deltaTime * Vector2.down;
    }

    void MoveLeft()
    {
        movement = GameController.instance.snakeSpeed * Time.deltaTime * Vector2.left;
    }

    void MoveRight()
    {
        movement = GameController.instance.snakeSpeed * Time.deltaTime * Vector2.right;
    }

    public void ResetSnake()
    {
        foreach (BodyPart part in parts)
        {
            Destroy(part.gameObject);
        }
        parts.Clear();

        tail = null;
        MoveUp();

        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0); //up
        gameObject.transform.position = new Vector3(0, 0, -8); //center

        partsToAdd = 5;
        addTimer = TimeToAddBodyPart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Egg egg = collision.GetComponent<Egg>();

        if (egg)
        {
            Debug.Log("Hit egg");
            EatEgg(egg);
        }
        else
        {
            Debug.Log("Hit obstacle");
            GameController.instance.GameOver();
        }

    }

    private void EatEgg(Egg egg)
    {
        partsToAdd = 5;
        addTimer = 0;

        GameController.instance.EggEaten(egg);
    }
}
