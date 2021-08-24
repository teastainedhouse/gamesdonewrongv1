using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("UdemyMatch3");
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
