using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Player _player;
    public Text hpText;

    public Animator levelTransition;
    public Animator levelNumber;
    public GameObject levelNumberText;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        UpdateHP();
        StartCoroutine(LevelTransitionAnimationHandler());
    }

    public void UpdateHP()
    {
        hpText.text = "hp: " + _player.hp;
    }

    IEnumerator LevelTransitionAnimationHandler()
    {
        levelTransition.SetTrigger("start");
        levelNumber.SetTrigger("start");
        yield return null;
    }
}
