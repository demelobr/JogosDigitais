using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControlTutorial : MonoBehaviour
{
    [System.Serializable]
    public enum language
    {
        pt,
        eng,
        spa
    }

    public language lang;

    [Header("Components")]
    public GameObject dialogueObj; // Janela do dialogo
    public Image profileSprite; // Sprite do perfil
    public Text speechText; // Texto da fala
    public Text actorNameText; // Nome do npc
    public GameObject tools; // Janela de ferramentas
    public GameObject questObj; // Janela da quest
    public Text textQuest; // Texto da quest
    public GameObject collect;

    [Header("Settings")]
    public float typingSpeed; // Velocidade da fala

    // Variáveis de controle
    public bool isShowing; // Se a janela está visível
    private int index; // Índice para saber a quantidade de falas
    private string[] sentences; // Array de senteças

    public static DialogueControlTutorial instance;

    // Awake é chamado antes de todos os Start() na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Apresentar a frase/fala letra por letra
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Pular para próxima frase/fala
    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }

    public void NextSentenceTutorial()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
            if (index == 4 && GameManager.instance.ossudosDerrotados < 1) 
            {
                tools.SetActive(true);
                questObj.SetActive(true);
            }

            if (index == 1 && GameManager.instance.ossudosDerrotados >= 10) 
            {
                questObj.SetActive(false);
            }

            if (index == 2 && GameManager.instance.ossudosDerrotados >= 1) 
            {
                collect.SetActive(true);
            }

            if (index == 0 && PlayerItens.instance.totalWood >= 10)
            {
                SceneManager.LoadScene("Fase 3");
            }
        }
    }

    // Chamar a fala do npc
    public void Speech(string[] txt)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
