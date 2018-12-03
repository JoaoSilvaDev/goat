using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundRandomTile : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] variations = new Sprite[3];

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        float rand = Mathf.PerlinNoise(transform.position.x + 7 * sceneIndex, transform.position.y) * 10 - 3;
        int index = (int)rand;
        Debug.Log(index);
        sr.sprite = variations[index];
    }
}
