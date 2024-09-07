using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTutorial : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings initialDialogue;
    public DialogueSettings secondDialogue;
    public DialogueSettings finalDialogue;
    public DialogueSettings currentDialogue;

    bool playerHit;

    private List<string> sentences = new List<string>();

    private void Start()
    {
        currentDialogue = initialDialogue;
        GetNPCInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControlTutorial.instance.Speech(sentences.ToArray());
        }

        if (GameManager.instance.ossudosDerrotados >= 10)
        {
            sentences.Clear();
            currentDialogue = secondDialogue;
            GetNPCInfo();
        }

        if (PlayerItens.instance.totalWood >= 10) 
        {
            sentences.Clear();
            currentDialogue = finalDialogue;
            GetNPCInfo();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetNPCInfo()
    {
        // sentences.Clear(); // Limpa as sentenças antigas

        // Verifica se o jogador derrotou 10 Ossudos e troca o diálogo
        // if (GameManager.instance.ossudosDerrotados >= 10)
        // {
        //     currentDialogue = secondDialogue;
        // }

        for (int i = 0; i < currentDialogue.dialogues.Count; i++)
        {
            switch (DialogueControlTutorial.instance.lang)
            {
                case DialogueControlTutorial.language.pt:
                    sentences.Add(currentDialogue.dialogues[i].sentence.portuguese);
                    break;
                case DialogueControlTutorial.language.eng:
                    sentences.Add(currentDialogue.dialogues[i].sentence.english);
                    break;
                case DialogueControlTutorial.language.spa:
                    sentences.Add(currentDialogue.dialogues[i].sentence.spanish);
                    break;
            }
        }
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        
        if (hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}