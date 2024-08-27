using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string sceneName; // Nome da cena que será carregada

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Verifica se o objeto que entrou no gatilho é o jogador
        {
            SceneManager.LoadScene(sceneName); // Carrega a cena especificada
        }
    }
}
