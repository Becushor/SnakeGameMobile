using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : BodyPart
{
    Vector2 movement;

    void Start()
    {
        SwipeControls.OnSwipe += SwipeDetection;
    }

    void Update()
    {
        SetMovement(movement);
        UpdateDirection();
        UpdatePosition();
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
}
