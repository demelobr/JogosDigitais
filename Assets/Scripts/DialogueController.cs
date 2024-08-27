using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    public GameObject npcCanvas; // Substitua isso pelo Canvas do NPC
    public GameObject firstCanvas; // Se tiver outro Canvas anterior
    public GameObject advanceButton; // Botão para avançar o diálogo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed, canvas should be active and focused");
            npcCanvas.SetActive(true); // Ativa o Canvas do NPC

            // Desativa o primeiro Canvas, se necessário
            if (firstCanvas != null && firstCanvas.activeInHierarchy)
            {
                firstCanvas.SetActive(false);
            }

            // Foca no botão de avançar diálogo
            EventSystem.current.SetSelectedGameObject(advanceButton);
        }

        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("Advance dialogue button pressed");
            // Código para avançar o diálogo
        }
    }
}
