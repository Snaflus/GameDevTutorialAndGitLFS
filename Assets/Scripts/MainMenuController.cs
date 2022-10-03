using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame(int charIndex)
    {
        GameManager.instance.CharIndex = charIndex;

        SceneManager.LoadScene("Gameplay");
    }
}
