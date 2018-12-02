using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemiesParents;
    Enemy[] enemies = new Enemy[10];    

    void Start()
    {
        if (enemiesParents.transform.childCount > 0)        
            enemies = enemiesParents.GetComponentsInChildren<Enemy>();
    }

    public void CallForMove()
    {
        if(enemiesParents.transform.childCount > 0)
            foreach(Enemy e in enemies)
                e.Move();
    }
}
