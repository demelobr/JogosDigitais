using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
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

    [Header("Settings")]
    public float typingSpeed; // Velocidade da fala

    // Variáveis de controle
    public bool isShowing; // Se a janela está visível
    private int index; // Índice para saber a quantidade de falas
    private string[] sentences; // Array de sentenças
    private Sprite[] currentNpcSprites; // Array de sprites para o NPC atual
    private Sprite defaultSprite; // Sprite padrão

    public static DialogueControl instance;

    // Awake é chamado antes de todos os Start() na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
    }

    public void Speech(string[] txt, Sprite[] npcSprites, Sprite defaultSprite)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            this.defaultSprite = defaultSprite;
            currentNpcSprites = npcSprites;

            // Configura o sprite inicial
            if (npcSprites != null && npcSprites.Length > 0 && npcSprites[0] != null)
            {
                profileSprite.sprite = npcSprites[0];
            }
            else
            {
                profileSprite.sprite = defaultSprite; // Usa o sprite padrão
            }

            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }

    IEnumerator TypeSentence()
    {
        // Troca a imagem para cada frase
        if (currentNpcSprites != null && currentNpcSprites.Length > index && currentNpcSprites[index] != null)
        {
            profileSprite.sprite = currentNpcSprites[index];
        }
        else
        {
            profileSprite.sprite = defaultSprite;
        }

        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

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
}
