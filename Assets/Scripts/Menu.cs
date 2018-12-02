using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
