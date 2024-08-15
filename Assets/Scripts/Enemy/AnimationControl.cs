using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private Player player;
    private PlayerAnim playerAnim;
    private Animator anim;
    private Skeleton skeleton;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnim>();
        player = playerAnim.GetComponentInParent<Player>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (!skeleton.isDead && !player.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                playerAnim.OnHut();
            }
        }
    }

    public void OnHut()
    {
        skeleton.currentHealth--;
        skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;

        if (skeleton.currentHealth < 1 && !skeleton.isDead)
        {
            float previousTotalHealth = skeleton.totalHealth;
            skeleton.isDead = true;
            anim.SetTrigger("death");
            Destroy(skeleton.gameObject, 1.5f);
            GameObject instance =  Instantiate(playerAnim.skeletonPrefab, transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f), transform.rotation);
            instance.GetComponent<Skeleton>().totalHealth = Random.Range(3, 6 + 1);
        }
        else
        {
            anim.SetTrigger("hut");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

}
