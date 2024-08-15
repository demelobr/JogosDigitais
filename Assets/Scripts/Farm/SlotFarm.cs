using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    /*
        1) Aumentar a área de colisão para poder pegar água em todo o lago.
     */
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private float waterAmount;
    [SerializeField] private bool detectingWater;

    private bool isPlayer;

    PlayerItens playerItens;

    private int initialDigAmount;
    private float currentWater;
    
    private bool dugHole;
    private bool plantedCarrot;

    private void Start()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (dugHole)
        {
            if (detectingWater)
            {
                currentWater += 0.01f;
            }

            if (currentWater >= waterAmount && !plantedCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRender.sprite = carrot;
                plantedCarrot = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && plantedCarrot && isPlayer)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRender.sprite = hole;
                playerItens.carrots++;
                currentWater = 0f;
                plantedCarrot = false;
            }
        }
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount/2)
        {
            spriteRender.sprite = hole;
            dugHole = true;
        }

//        if (digAmount <= 0)
//        {
//            spriteRender.sprite = carrot;
//        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }
        if (collision.CompareTag("Water"))
        {
            detectingWater = true;
        }
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detectingWater = false;
        }
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
