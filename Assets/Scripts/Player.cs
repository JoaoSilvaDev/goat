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

    private CanvasManager _canvas;
    public int hp = 10;

    public float raycastDistance;
    private BoxCollider2D collider;

    enum Direction
    {
        Up,
        Right,
        Left,
        Down
    }

    void Start()
    {
        _canvas = FindObjectOfType<CanvasManager>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 upCastVector = new Vector2(transform.position.x, transform.position.y - (collider.bounds.size.y / 2));
        Vector2 downCastVector = new Vector2(transform.position.x, transform.position.y - (collider.bounds.size.y / 2));
        Vector2 rightCastVector = new Vector2(transform.position.x + (collider.bounds.size.x / 2), transform.position.y);
        Vector2 leftCastVector = new Vector2(transform.position.x + (collider.bounds.size.x / 2), transform.position.y);

        RaycastHit2D upCast = Physics2D.Raycast(upCastVector, Vector2.up, raycastDistance);
        RaycastHit2D downCast = Physics2D.Raycast(downCastVector, Vector2.down, raycastDistance);
        RaycastHit2D rightCast = Physics2D.Raycast(rightCastVector, Vector2.right, raycastDistance);
        RaycastHit2D leftCast = Physics2D.Raycast(leftCastVector, Vector2.left, raycastDistance);

        Debug.DrawRay(upCastVector, Vector2.up * raycastDistance, Color.red);
        Debug.DrawRay(downCastVector, Vector2.down * raycastDistance, Color.red);
        Debug.DrawRay(leftCastVector, Vector2.left * raycastDistance, Color.red);
        Debug.DrawRay(rightCastVector, Vector2.right * raycastDistance, Color.red);

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && leftCast.collider == null)
                input.x = -1;
            else if (Input.GetKeyDown(KeyCode.RightArrow) && rightCast.collider == null)
                input.x = 1;
            else if (Input.GetKeyDown(KeyCode.UpArrow) && upCast.collider == null)
                input.y = 1;
            else if (Input.GetKeyDown(KeyCode.DownArrow) && downCast.collider == null)
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

                hp -= 1;
                _canvas.UpdateHP();
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
}
