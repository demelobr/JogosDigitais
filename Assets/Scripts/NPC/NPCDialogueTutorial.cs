using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTutorial : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings dialogue;

    bool playerHit;

    private List<string> sentences = new List<string>();

    private void Start()
    {
        GetNPCInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControlTutorial.instance.Speech(sentences.ToArray());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetNPCInfo()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueControlTutorial.instance.lang)
            {
                case DialogueControlTutorial.language.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;
                case DialogueControlTutorial.language.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;
                case DialogueControlTutorial.language.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
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