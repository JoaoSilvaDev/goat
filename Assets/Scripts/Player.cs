using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Direction currentDirection;
    Vector2 input;
    bool isMoving = false;
    Vector3 startPos, endPos;
    float time;

    public float speed;

    public Sprite up, down, left, right;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                input.x = -1;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                input.x = 1;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                input.y = 1;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                input.y = -1;
            else
            {
                input.x = 0;
                input.y = 0;
            }

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if (input != Vector2.zero)
            {
                if (input.x < 0)
                    currentDirection = Direction.Left;
                if (input.x > 0)
                    currentDirection = Direction.Right;
                if (input.y < 0)
                    currentDirection = Direction.Down;
                if (input.y > 0)
                    currentDirection = Direction.Up;

                switch (currentDirection)
                {
                    case Direction.Up:
                        GetComponent<SpriteRenderer>().sprite = up;
                        break;
                    case Direction.Down:
                        GetComponent<SpriteRenderer>().sprite = down;
                        break;
                    case Direction.Left:
                        GetComponent<SpriteRenderer>().sprite = left;
                        break;
                    case Direction.Right:
                        GetComponent<SpriteRenderer>().sprite = right;
                        break;
                }

                StartCoroutine(Move(transform));
            }
        }
    }

    IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        time = 0.0f;

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

        while (time < 1.0f)
        {
            time += Time.deltaTime * speed;
            entity.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }

    enum Direction
    {
        Up,
        Right,
        Left,
        Down
    }
}
