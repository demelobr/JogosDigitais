using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    /*
        1) Adicionar a mãozinha para ficar sem nenhuma ferramenta;
        2) Tirar o "for" que atualiza a HUD do update e colocar num método para ser chamado apenas quando trocar a ferramenta.
     
     */
    [Header("Itens")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;

    [Header("Tools")]
    public List<Image> toolsUI = new List<Image>();

    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private PlayerItens playerItens;
    private Player player;

    private bool isPaused;

    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        fishUIBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        waterUIBar.fillAmount = playerItens.currentWater / playerItens.waterLimit;
        woodUIBar.fillAmount = playerItens.totalWood / playerItens.woodLimit;
        carrotUIBar.fillAmount = playerItens.carrots / playerItens.carrotLimit;
        fishUIBar.fillAmount = playerItens.fishes / playerItens.fishesLimit;


        if(player.handlingObj > 0)
        {
            for (int i = 0; i < toolsUI.Count; i++)
            {
                if (i == player.handlingObj - 1)
                {
                    toolsUI[i].color = selectColor;
                }
                else
                {
                    toolsUI[i].color = alphaColor;
                }
            }
        }
        else
        {
            for (int i = 0; i < toolsUI.Count; i++)
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }

    public void pauseAndPlay()
    {
        //SceneManager.LoadScene("MenuScene");
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;

            isPaused = true;
        }
    }
}
