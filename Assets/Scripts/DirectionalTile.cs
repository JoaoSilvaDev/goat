using UnityEngine;

public class DirectionalTile : MonoBehaviour
{
    public GameObject colliders;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colliders.transform.Rotate(0, 0, 180);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colliders.transform.Rotate(0, 0, 180);
    }
}
