using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool _selectHasPlayed;

    public void Play()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
        if (!_selectHasPlayed)
        {
            FindObjectOfType<AudioManager>().Play("Select");
            _selectHasPlayed = true;
        }
        FindObjectOfType<AudioManager>().Play("Goat 1");
        FindObjectOfType<AudioManager>().Stop("Title Screen");
    }

    public void Exit()
    {
        if (!_selectHasPlayed)
        {
            FindObjectOfType<AudioManager>().Play("Select");
            _selectHasPlayed = true;
        }
        Application.Quit();
    }
}
