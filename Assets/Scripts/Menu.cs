using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Play("Goat 1");
        FindObjectOfType<AudioManager>().Stop("Title Screen");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
