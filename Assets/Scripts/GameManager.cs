using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int ossudosDerrotados = 0; // Contador de inimigos
    public Text contadorText; // Referência ao texto na UI

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementarOssudosDerrotados()
    {
        ossudosDerrotados++;
        AtualizarContador();
    }

    private void AtualizarContador()
    {
        contadorText.text = "Ossudos: " + ossudosDerrotados + "/10";
    }
}

