using UnityEngine;

public class Sand : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
            GetComponent<SpriteRenderer>().enabled = false;            
        }
    }
}
