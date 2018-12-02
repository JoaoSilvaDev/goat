using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Player _player;
    public Text hpText;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        UpdateHP();
    }

    public void UpdateHP()
    {
        hpText.text = "hp: " + _player.hp;
    }
}
