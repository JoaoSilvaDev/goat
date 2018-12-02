using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Player _player;
    public Text hpText;

    public Animator levelBeginTransition;
    public Animator levelEndTransition;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
        UpdateHP();
        StartCoroutine(BeginLevelSequence());
    }

    public void UpdateHP()
    {
        hpText.text = "hp: " + _player.hp;
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
