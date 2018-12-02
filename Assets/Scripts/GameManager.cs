using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemies;

    public void CallForMove()
    {
        enemies.GetComponentInChildren<Enemy>().Move();
    }
}
