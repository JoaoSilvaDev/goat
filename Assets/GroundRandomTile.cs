using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundRandomTile : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] variations = new Sprite[4];

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        float rand = Mathf.PerlinNoise(transform.position.x * sceneIndex, transform.position.y) * 10 * variations.Length;
        int index = (int)rand / 10;

        sr.sprite = variations[index-1];
    }
}
