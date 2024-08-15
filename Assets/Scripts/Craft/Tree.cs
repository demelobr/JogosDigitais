using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private ParticleSystem leafs;

    private float treeHealth;
    private double totalWood;
    private bool isCut;

    private void Start()
    {
        treeHealth = Random.Range(3, 10);
        totalWood = System.Math.Ceiling((((treeHealth - 3) * (3 - 1)) / (10 - 3)) + 1);
    }

    public void OnHit()
    {
        treeHealth--;
        anim.SetTrigger("isHit");
        leafs.Play();

        if (treeHealth <= 0)
        {
            for (int i = 0; i < (int)totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation);
            }
            anim.SetTrigger("cut");
            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isCut)
        {
            OnHit();
        }
    }
}
