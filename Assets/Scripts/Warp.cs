using UnityEngine;

public class Warp : MonoBehaviour
{
    public GameObject target;
    public bool canEnter = true;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Goat");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player && canEnter)
        {
            _player.GetComponent<Player>().isWarping = true;
            target.GetComponent<Warp>().canEnter = false;
            _player.transform.position = target.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _player.GetComponent<Player>().isWarping = false;
            canEnter = true;
        }            
    }
}
