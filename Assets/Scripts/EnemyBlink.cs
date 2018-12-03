using UnityEngine;

public class EnemyBlink : Enemy
{
    public override void Move()
    {
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
        GetComponent<Animator>().SetBool("attack", !GetComponent<BoxCollider2D>().enabled);
    }
}
