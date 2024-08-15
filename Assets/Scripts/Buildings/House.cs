using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    /*
     1) Ajustar o player para sempre fazer a animção para a direita.
    */
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject colliderHouse;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;

    private bool detectingPlayer;
    private Player player;
    private PlayerAnim playerAnim;
    private PlayerItens playerItens;

    private float timeCount;
    private bool isBegining;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItens = player.GetComponent<PlayerItens>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.C) && playerItens.totalWood >= woodAmount)
        {
            playerItens.totalWood -= woodAmount;
            isBegining = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            playerAnim.transform.position = point.position;
            player.isPaused = true;
        }

        if (isBegining)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= timeAmount)
            {
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused = false;
                colliderHouse.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
