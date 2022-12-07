using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : BodyPart
{
    Vector2 movement;

    private BodyPart tail = null;

    const float TimeToAddBodyPart = 0.1f;
    float addTimer = TimeToAddBodyPart;

    public int partsToAdd = 0;

    void Start()
    {
        SwipeControls.OnSwipe += SwipeDetection;
    }

    void Update()
    {
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
            BodyPart newPart = Instantiate(GameController.instance.bodyPrefab, transform.position, Quaternion.identity);
            newPart.following = this;
            tail = newPart;
            newPart.TurnIntoTail();
        }
        else
        {
            Vector3 newPosition = tail.transform.position;
            newPosition.z += 0.01f;
            BodyPart newPart = Instantiate(GameController.instance.bodyPrefab, newPosition, Quaternion.identity);
            newPart.following = tail;
            newPart.TurnIntoTail();
            tail.TurnIntoBodyPart();
            tail = newPart;
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
        movement = Vector2.up * GameController.instance.snakeSpeed * Time.deltaTime;
    }

    void MoveDown()
    {
        movement = Vector2.down * GameController.instance.snakeSpeed * Time.deltaTime;
    }

    void MoveLeft()
    {
        movement = Vector2.left * GameController.instance.snakeSpeed * Time.deltaTime;
    }

    void MoveRight()
    {
        movement = Vector2.right * GameController.instance.snakeSpeed * Time.deltaTime;
    }

    public void ResetSnake()
    {
        tail = null;

        MoveUp();

        partsToAdd = 5;
        addTimer = TimeToAddBodyPart;
    }
}
