using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Direction currentDirection;
    private Vector2 _input;
    private bool _isMoving = false;
    Vector3 startPos, endPos;
    private float _time;

    public float speed;

    public Sprite up, down, left, right;

    private CanvasManager _canvas;
    public int hp = 10;

    public float raycastDistance;
    private BoxCollider2D _collider;

    public bool isWarping;

    public string sceneName;

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
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 upCastVector = new Vector2(transform.position.x, transform.position.y + (_collider.bounds.size.y / 2));
        Vector2 downCastVector = new Vector2(transform.position.x, transform.position.y - (_collider.bounds.size.y / 2));
        Vector2 rightCastVector = new Vector2(transform.position.x + (_collider.bounds.size.x / 2), transform.position.y);
        Vector2 leftCastVector = new Vector2(transform.position.x - (_collider.bounds.size.x / 2), transform.position.y);

        RaycastHit2D upCast = Physics2D.Raycast(upCastVector, Vector2.up, raycastDistance);
        RaycastHit2D downCast = Physics2D.Raycast(downCastVector, Vector2.down, raycastDistance);
        RaycastHit2D rightCast = Physics2D.Raycast(rightCastVector, Vector2.right, raycastDistance);
        RaycastHit2D leftCast = Physics2D.Raycast(leftCastVector, Vector2.left, raycastDistance);

        Debug.DrawRay(upCastVector, Vector2.up * raycastDistance, Color.red);
        Debug.DrawRay(downCastVector, Vector2.down * raycastDistance, Color.red);
        Debug.DrawRay(leftCastVector, Vector2.left * raycastDistance, Color.red);
        Debug.DrawRay(rightCastVector, Vector2.right * raycastDistance, Color.red);

        if (!_isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && leftCast.collider == null)
                _input.x = -0.95f;
            else if (Input.GetKeyDown(KeyCode.RightArrow) && rightCast.collider == null)
                _input.x = 0.95f;
            else if (Input.GetKeyDown(KeyCode.UpArrow) && upCast.collider == null)
                _input.y = 0.95f;
            else if (Input.GetKeyDown(KeyCode.DownArrow) && downCast.collider == null)
                _input.y = -0.95f;
            else
            {
                _input.x = 0;
                _input.y = 0;
            }

            if (Mathf.Abs(_input.x) > Mathf.Abs(_input.y))
                _input.y = 0;
            else
                _input.x = 0;

            if (_input != Vector2.zero)
            {
                if (_input.x < 0)
                    currentDirection = Direction.Left;
                if (_input.x > 0)
                    currentDirection = Direction.Right;
                if (_input.y < 0)
                    currentDirection = Direction.Down;
                if (_input.y > 0)
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
        _isMoving = true;
        startPos = entity.position;
        _time = 0.0f;

        endPos = new Vector3(startPos.x + _input.x, startPos.y + _input.y, startPos.z);

        while (_time < 1.0f && !isWarping)
        {
            _time += Time.deltaTime * speed;
            entity.position = Vector3.Lerp(startPos, endPos, _time);
            yield return null;
        }

        _isMoving = false;
        yield return 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
