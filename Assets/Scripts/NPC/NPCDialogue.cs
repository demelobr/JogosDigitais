using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings dialogue;

    bool playerHit;
    private List<string> sentences = new List<string>();
    private List<Sprite> sprites = new List<Sprite>(); // Adiciona a lista de sprites

    private void Start()
    {
        GetNPCInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControl.instance.Speech(
                sentences.ToArray(),
                sprites.ToArray(), // Passa a lista de sprites
                dialogue.speakerSprite // Passa o sprite padrão
            );
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetNPCInfo()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueControl.instance.lang)
            {
                case DialogueControl.language.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;
                case DialogueControl.language.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;
                case DialogueControl.language.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }
            sprites.Add(dialogue.dialogues[i].profile); // Adiciona o sprite correspondente
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
