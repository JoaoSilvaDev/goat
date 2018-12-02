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
    private bool _nextIsGoal = false;
    public bool nextIsWolf = false;

    public string losingSceneName;
    public string winningSceneName;

    private GameManager _gameManager;
    public WarpManager warpManager;

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
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (hp <= 0)
            StartCoroutine(GameOver());
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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (leftCast.collider == null)
                    _input.x = -0.95f;
                else
                {
                    if (leftCast.collider.CompareTag("Walls"))
                        _input.x = 0.0f;
                    else if (leftCast.collider.CompareTag("Goal"))
                    {
                        _nextIsGoal = true;
                        nextIsWolf = false;
                        _input.x = -0.95f;
                    }
                    else if (leftCast.collider.CompareTag("Warp"))
                    {
                        nextIsWolf = false;
                        _input.x = -0.95f;
                    }
                    else if (leftCast.collider.CompareTag("Sand"))
                    {
                        nextIsWolf = false;
                        _input.x = -0.95f;
                    }
                    else if (leftCast.collider.CompareTag("Enemy"))
                    {
                        nextIsWolf = true;
                        _input.x = -0.95f;
                    }
                }                    
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (rightCast.collider == null)
                    _input.x = 0.95f;
                else
                {
                    if (rightCast.collider.CompareTag("Walls"))
                        _input.x = 0.0f;
                    else if (rightCast.collider.CompareTag("Goal"))
                    {
                        _nextIsGoal = true;
                        nextIsWolf = false;
                        _input.x = 0.95f;
                    }
                    else if (rightCast.collider.CompareTag("Warp"))
                    {
                        nextIsWolf = false;
                        _input.x = 0.95f;
                    }
                    else if (rightCast.collider.CompareTag("Sand"))
                    {
                        nextIsWolf = false;
                        _input.x = 0.95f;
                    }
                    else if (rightCast.collider.CompareTag("Enemy"))
                    {
                        nextIsWolf = true;
                        _input.x = 0.95f;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (upCast.collider == null)
                    _input.y = 0.95f;
                else
                {
                    if (upCast.collider.CompareTag("Walls"))
                        _input.y = 0.0f;
                    else if (upCast.collider.CompareTag("Goal"))
                    {
                        _nextIsGoal = true;
                        nextIsWolf = false;
                        _input.y = 0.95f;
                    }
                    else if (upCast.collider.CompareTag("Warp"))
                    {
                        nextIsWolf = false;
                        _input.y = 0.95f;
                    }
                    else if (upCast.collider.CompareTag("Sand"))
                    {
                        nextIsWolf = false;
                        _input.y = 0.95f;
                    }
                    else if (upCast.collider.CompareTag("Enemy"))
                    {                        
                        nextIsWolf = true;
                        _input.y = 0.95f;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (downCast.collider == null)
                    _input.y = -0.95f;
                else
                {
                    if (downCast.collider.CompareTag("Walls"))
                        _input.y = 0.0f;
                    else if (downCast.collider.CompareTag("Goal"))
                    {
                        _nextIsGoal = true;
                        nextIsWolf = false;
                        _input.y = -0.95f;
                    }
                    else if (downCast.collider.CompareTag("Warp"))
                    {
                        nextIsWolf = false;
                        _input.y = -0.95f;
                    }
                    else if (downCast.collider.CompareTag("Sand"))
                    {
                        nextIsWolf = false;
                        _input.y = -0.95f;
                    }
                    else if (downCast.collider.CompareTag("Enemy"))
                    {
                        nextIsWolf = true;
                        _input.y = -0.95f;
                    }
                }
            }
            else
            {
                _input.x = 0.0f;
                _input.y = 0.0f;
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

                if (!_nextIsGoal)
                {
                    hp -= 1;
                    _canvas.UpdateHP();
                }

                if (nextIsWolf)
                    _collider.enabled = false;
            }
        }
    }

    IEnumerator Move(Transform entity)
    {
        _isMoving = true;
        startPos = entity.position;
        _time = 0.0f;

        endPos = new Vector3(startPos.x + _input.x, startPos.y + _input.y, startPos.z);
        _gameManager.CallForMove();

        while (_time < 1.0f && !isWarping && hp >= 0)
        {            
            _time += Time.deltaTime * speed;
            entity.position = Vector3.Lerp(startPos, endPos, _time);
            yield return null;
        }

        _collider.enabled = true;
        _isMoving = false;
        yield return 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(GameOver());
        }
        if (collision.CompareTag("Goal"))
        {
            StartCoroutine(Winning());
        }
        if (collision.CompareTag("Warp") && warpManager.activatedWarp.GetComponent<Warp>().canEnter)
        {
            StartCoroutine(WarpEnter(collision.gameObject));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sand"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.layer = LayerMask.NameToLayer("Default");
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            collision.gameObject.tag = "Walls";
        }
        if (collision.CompareTag("Warp"))
        {
            StartCoroutine(WarpExit(collision.gameObject));            
        }
    }

    IEnumerator GameOver()
    {
        //play animation
        _isMoving = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(losingSceneName, LoadSceneMode.Single);
    }

    IEnumerator Winning()
    {
        //play animation
        _isMoving = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(winningSceneName, LoadSceneMode.Single);
    }


    IEnumerator WarpEnter(GameObject activatedWarp)
    {
        warpManager.activatedWarp = activatedWarp;
        //play animation
        yield return new WaitForSeconds(0.2f/*animation to end*/);
        print(activatedWarp.name + " - Enter");
        isWarping = true;
        warpManager.target.GetComponent<Warp>().canEnter = false;
        warpManager.activatedWarp = warpManager.target;
        transform.position = warpManager.target.transform.position;
    }

    IEnumerator WarpExit(GameObject activatedWarp)
    {
        //play animation
        yield return new WaitForSeconds(0.2f/*animation to end*/);
        print(activatedWarp.gameObject.name + " - Exit");
        isWarping = false;
        warpManager.activatedWarp = warpManager.target;
        warpManager.activatedWarp.GetComponent<Warp>().canEnter = true;
    }
}
