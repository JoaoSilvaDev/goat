using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 _input;
    public bool isMoving = false;
    Vector3 startPos, endPos;
    private float _time;

    public float speed;

    private CanvasManager _canvas;
    public int hp = 10;

    public float raycastDistance;
    private BoxCollider2D _collider;

    public bool isWarping;
    private bool _nextIsGoal = false;
    public bool leftEnemy = false;
    public bool rightEnemy = false;
    public bool upEnemy = false;
    public bool downEnemy = false;

    private string _losingSceneName;
    private string _winningSceneName;

    private GameManager _gameManager;
    public WarpManager warpManager;

    public GoalManager goal;

    public Sprite sandSprite;

    private bool _deathHasPlayed;
    private bool _keyHasPlayed;
    private bool _teleportHasPlayed;
    private bool _goalHasPlayed;

    void Start()
    {
        _canvas = FindObjectOfType<CanvasManager>();
        _collider = GetComponent<BoxCollider2D>();
        _gameManager = FindObjectOfType<GameManager>();

        goal = FindObjectOfType<GoalManager>();

        Scene scene = SceneManager.GetActiveScene();

        int levelIndex =  int.Parse (scene.name.Substring(scene.name.Length - 2));
        int nextLevel = levelIndex + 1;

        _losingSceneName = scene.name;

        if(nextLevel < 10)
            _winningSceneName = "Level 0" + nextLevel.ToString();
        else
            _winningSceneName = "Level " + nextLevel.ToString();
    }

    void Update()
    {
        if (hp == 0)
        {
            StartCoroutine(GameOver());
            _input = Vector2.zero;
        }            
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(GameOver());
        if(Input.GetKeyDown(KeyCode.O))
            StartCoroutine(Winning());
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
        
        
        if (!isMoving)
        {                                  
            if (leftCast.collider != null)
            {
                if (leftCast.collider.CompareTag("Enemy"))
                    leftEnemy = true;                                
                else if (leftCast.collider.CompareTag("Warp"))
                {
                    leftEnemy = false;
                    _input.x = -0.95f;
                }
                else if (leftCast.collider.CompareTag("Sand"))
                {
                    leftEnemy = false;
                    _input.x = -0.95f;
                }
            }

            if (rightCast.collider != null)
            {
                if (rightCast.collider.CompareTag("Enemy"))
                    rightEnemy = true;                                
                else if (rightCast.collider.CompareTag("Warp"))
                {
                    rightEnemy = false;
                    _input.x = 0.95f;
                }
                else if (rightCast.collider.CompareTag("Sand"))
                {
                    rightEnemy = false;
                    _input.x = 0.95f;
                }
            }

            if (upCast.collider != null)
            {
                if (upCast.collider.CompareTag("Enemy"))
                    upEnemy = true;                                
                else if (upCast.collider.CompareTag("Warp"))
                {
                    upEnemy = false;
                    _input.y = 0.95f;
                }
                else if (upCast.collider.CompareTag("Sand"))
                {
                    upEnemy = false;
                    _input.y = 0.95f;
                }
            }

            if (downCast.collider != null)
            {
                if (downCast.collider.CompareTag("Enemy"))
                    downEnemy = true;                                
                else if (downCast.collider.CompareTag("Warp"))
                {
                    downEnemy = false;
                    _input.y = -0.95f;
                }
                else if (downCast.collider.CompareTag("Sand"))
                {
                    downEnemy = false;
                    _input.y = -0.95f;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (leftCast.collider == null)
                {
                    leftEnemy = false;
                    _input.x = -0.95f;
                }
                else if (leftCast.collider.CompareTag("Walls"))
                    _input.x = 0.0f;
                else if (leftCast.collider.CompareTag("Goal"))
                {
                    _nextIsGoal = true;
                    leftEnemy = false;
                    _input.x = -0.95f;
                }

                if (rightCast.collider == null)
                    rightEnemy = false;
                if (upCast.collider == null)
                    upEnemy = false;
                if (downCast.collider == null)
                    downEnemy = false;
                if (leftEnemy)
                    leftEnemy = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (rightCast.collider == null)
                {
                    rightEnemy = false;
                    _input.x = 0.95f;
                }
                else if (rightCast.collider.CompareTag("Walls"))
                    _input.x = 0.0f;
                else if (rightCast.collider.CompareTag("Goal"))
                {
                    _nextIsGoal = true;
                    rightEnemy = false;
                    _input.x = 0.95f;
                }

                if (leftCast.collider == null)
                    leftEnemy = false;
                if (upCast.collider == null)
                    upEnemy = false;
                if (downCast.collider == null)
                    downEnemy = false;
                if (rightEnemy)
                    rightEnemy = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (upCast.collider == null)
                {
                    upEnemy = false;
                    _input.y = 0.95f;
                }
                else if (upCast.collider.CompareTag("Walls"))
                    _input.y = 0.0f;
                else if (upCast.collider.CompareTag("Goal"))
                {
                    _nextIsGoal = true;
                    upEnemy = false;
                    _input.y = 0.95f;
                }

                if (rightCast.collider == null)
                    rightEnemy = false;
                if (leftCast.collider == null)
                    leftEnemy = false;
                if (downCast.collider == null)
                    downEnemy = false;
                if (upEnemy)
                    upEnemy = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (downCast.collider == null)
                {
                    downEnemy = false;
                    _input.y = -0.95f;
                }
                else if (downCast.collider.CompareTag("Walls"))
                    _input.y = 0.0f;
                else if (downCast.collider.CompareTag("Goal"))
                {
                    _nextIsGoal = true;
                    downEnemy = false;
                    _input.y = -0.95f;
                }

                if (leftCast.collider == null)
                    leftEnemy = false;
                if (rightCast.collider == null)
                    rightEnemy = false;
                if (upCast.collider == null)
                    upEnemy = false;
                if (downEnemy)
                    downEnemy = false;
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
                    GetComponent<SpriteRenderer>().flipX = false;
                if (_input.x > 0)
                    GetComponent<SpriteRenderer>().flipX = true;

                StartCoroutine(Move(transform));

                if (!_nextIsGoal)
                {
                    hp -= 1;
                    _canvas.UpdateHP(hp);
                }
                else
                {
                    _canvas.UpdateHP(hp - 1);
                }
            }

            if (leftEnemy || rightEnemy || upEnemy || downEnemy)
                _collider.enabled = false;
        }                  
    }

    IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        _time = 0.0f;

        endPos = new Vector3(startPos.x + _input.x, startPos.y + _input.y, startPos.z);
        _gameManager.CallForMove();

        FindObjectOfType<AudioManager>().Play("Move");

        while (_time < 1.0f && !isWarping && hp >= 0)
        {
            GetComponent<Animator>().SetBool("jump", true);            
            _time += Time.deltaTime * speed;
            entity.position = Vector3.Lerp(startPos, endPos, _time);
            yield return null;
        }
        
        GetComponent<Animator>().SetBool("jump", false);
        _collider.enabled = true;
        isMoving = false;
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
            if (!_teleportHasPlayed)
            {
                FindObjectOfType<AudioManager>().Play("Teleport");
                _teleportHasPlayed = true;
            }
            StartCoroutine(WarpEnter(collision.gameObject));
        }
        if (collision.CompareTag("Key"))
        {
            if (!_keyHasPlayed)
            {
                FindObjectOfType<AudioManager>().Play("Key");
                _keyHasPlayed = true;
            }
            Destroy(collision.gameObject);
            goal.ActivateGoal();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sand"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.layer = LayerMask.NameToLayer("Default");
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = sandSprite;
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
        GetComponent<Player>().enabled = false;
        GetComponent<Animator>().SetTrigger("dead");
        if (!_deathHasPlayed)
        {
            FindObjectOfType<AudioManager>().Play("Death");
            _deathHasPlayed = true;
        }        
        yield return new WaitForSeconds(1.5f);        
        SceneManager.LoadScene(_losingSceneName, LoadSceneMode.Single);
    }

    IEnumerator Winning()
    {
        GetComponent<Animator>().SetTrigger("nextLevel");
        if (!_goalHasPlayed)
        {
            FindObjectOfType<AudioManager>().Play("Goal");
            _goalHasPlayed = true;
        }
        yield return new WaitForSeconds(1.0f);
        _canvas.InitEndLevelSequence();
        isMoving = true;
        yield return new WaitForSeconds(1.0f);
        if (_winningSceneName == "Level 15")
        {
            FindObjectOfType<AudioManager>().Stop("Goat 1");
            FindObjectOfType<AudioManager>().Play("Goat 2");
        }
        else if (_winningSceneName == "Level 29")
        {
            FindObjectOfType<AudioManager>().Stop("Goat 2");
            FindObjectOfType<AudioManager>().Play("Goat 3");
        }
        SceneManager.LoadScene(_winningSceneName, LoadSceneMode.Single);
    }


    IEnumerator WarpEnter(GameObject activatedWarp)
    {
        warpManager.activatedWarp = activatedWarp;        
        yield return new WaitForSeconds(0.2f);
        isWarping = true;
        warpManager.target.GetComponent<Warp>().canEnter = false;
        warpManager.activatedWarp = warpManager.target;
        transform.position = warpManager.target.transform.position;
    }

    IEnumerator WarpExit(GameObject activatedWarp)
    {
        yield return new WaitForSeconds(0.2f);
        isWarping = false;
        warpManager.activatedWarp = warpManager.target;
        warpManager.activatedWarp.GetComponent<Warp>().canEnter = true;
    }
}
