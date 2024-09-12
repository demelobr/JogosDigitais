using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Toten : MonoBehaviour
{

    [SerializeField] private GameObject toten;
    [SerializeField] private float totenHealth;

    private bool isHitting;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void OnHit()
    {
        totenHealth--;

        if (totenHealth <= 0)
        {
            isHitting = true;
            SceneManager.LoadScene("YouWin");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isHitting)
        {
            OnHit();
        }
    }

}
