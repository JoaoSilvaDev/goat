using UnityEngine;

public class DirectionalTile : MonoBehaviour
{
    public GameObject colliders;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colliders.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
        }
    }
}
