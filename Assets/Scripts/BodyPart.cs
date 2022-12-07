using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    Vector2 dPosition; //d stands for delta, meaning the change in position

    public BodyPart following = null;

    private bool isTail = false;

    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetMovement(Vector2 movement)
    {
        dPosition = movement;
    }

    public void UpdatePosition()
    {
        gameObject.transform.position += (Vector3)dPosition;
    }

    public void UpdateDirection()
    {
        if (dPosition.y > 0)
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (dPosition.y < 0)
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        else if (dPosition.x < 0)
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
        else if (dPosition.x > 0)
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -90);
    }

    public void TurnIntoTail()
    {
        isTail = true;
        spriteRenderer.sprite = GameController.instance.tailSprite;
    }

    public void TurnIntoBodyPart()
    {
        isTail = false;
        spriteRenderer.sprite = GameController.instance.bodySprite;
    }
}
