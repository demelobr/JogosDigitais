using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private int castingPercentage;
    [SerializeField] private GameObject fishPrefab;

    private bool detectingPlayer;
    private PlayerItens playerItens;
    private PlayerAnim playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        playerAnim = playerItens.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.P))
        {
            playerAnim.OnCastingStarted();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1,100);

        if (randomValue <= castingPercentage)
        {
            Instantiate(fishPrefab, playerItens.transform.position + new Vector3(Random.Range(-2f, -1f), 0f, 0f), Quaternion.identity);
        }
        else
        {
            Debug.Log("Não pescou");
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
