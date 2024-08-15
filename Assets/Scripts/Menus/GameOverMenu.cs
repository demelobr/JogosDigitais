using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject painelMenuGameOver;

    public void voltarMenuPrincipal()
    {
        SceneManager.LoadScene(sceneName);
    }
}
