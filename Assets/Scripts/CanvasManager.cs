using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    private Player _player;
    public Text hpText;

    public Animator levelBeginTransition;
    public Animator levelEndTransition;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        UpdateHP(_player.hp);
        StartCoroutine(BeginLevelSequence());

        Scene scene = SceneManager.GetActiveScene();

        levelBeginTransition.GetComponentInChildren<Text>().text = scene.name;
        int levelIndex =  int.Parse (scene.name.Substring(scene.name.Length - 2));
        int nextLevel = levelIndex + 1;
        if(nextLevel < 10)            
            levelEndTransition.GetComponentInChildren<Text>().text = "Level 0" + nextLevel.ToString();
        else
            levelEndTransition.GetComponentInChildren<Text>().text = "Level " + nextLevel.ToString();
        
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void UpdateHP(float hp)
    {
        hpText.text = "x" + hp;
    }

    public void InitEndLevelSequence()
    {
        StartCoroutine(EndLevelSequence());
    }

    IEnumerator BeginLevelSequence()
    {
        _player.isMoving = true;
        levelBeginTransition.SetTrigger("start");
        yield return new WaitForSeconds(1.0f);
        _player.isMoving = false;
    }

    IEnumerator EndLevelSequence()
    {
        levelEndTransition.gameObject.SetActive(true);
        levelEndTransition.SetTrigger("start");        
        yield return null;
    }
}
