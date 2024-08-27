using UnityEngine;

public class ArvoreTransparente : MonoBehaviour
{
    public float transparencyLevel = 0.5f; // Nível de transparência (0.5 = 50% transparente)
    private SpriteRenderer treeSpriteRenderer;
    private Color originalColor;

    void Start()
    {
        treeSpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = treeSpriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Define a transparência da árvore
            Color transparentColor = originalColor;
            transparentColor.a = transparencyLevel;
            treeSpriteRenderer.color = transparentColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Restaura a cor original da árvore
            treeSpriteRenderer.color = originalColor;
        }
    }
}
