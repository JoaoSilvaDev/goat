using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Player _player;
    public Text hpText;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void UpdateHP()
    {
        if (_player.hp > 0)        
            hpText.text = "hp: " + (_player.hp - 1);
    }
}
