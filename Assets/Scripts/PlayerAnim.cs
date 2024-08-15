using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] public GameObject skeletonPrefab;
    
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;


    private Player player;
    private Animator anim;
    private Casting cast;

    private bool isHutting;
    private float recoveryTime = 1f;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cast = FindObjectOfType<Casting>(); // Só posso fazer isso pois só existe um elemento desse tipo na cena.
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();

        if (isHutting)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= recoveryTime)
            {
                isHutting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movemente

    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.isRolling)
            {
                anim.SetTrigger("isRoll");
            }
            else if (player.isJumping)
            {
                anim.SetTrigger("isJumping");
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
        }
        else
        {
            if (player.isJumping)
            {
                anim.SetTrigger("isJumping");
            }
            else
            {
                anim.SetInteger("transition", 0);
            }
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.isCutting)
        {
            anim.SetInteger("transition", 3);
        }

        if (player.isDigging)
        {
            anim.SetInteger("transition", 4);
        }


        if (player.isWatering)
        {
            anim.SetInteger("transition", 5);
        }
    }

    void OnRun()
    {
        if (player.isRunning)
        {
            anim.SetInteger("transition", 2);
        }
    }

    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hut = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hut != null)
        {
            hut.GetComponentInChildren<AnimationControl>().OnHut();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion

    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("hammering", true);
        transform.eulerAngles = new Vector2(0, 0);
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("hammering", false);
    }

    public void OnHut()
    {
        if (!isHutting)
        {
            player.currentHealth--;
            
            if (player.currentHealth < 1)
            {
                player.isDead = true;
                anim.SetTrigger("death");
                //Invoke("PlayerRespaw", 1.5f);
                Invoke("GameOverMenu", 1.5f);
            }
            else
            {
                anim.SetTrigger("hut");
                isHutting = true;
            }
        }
    }

    public void GameOverMenu()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void PlayerRespaw()
    {
        player.currentHealth = 10;
        player.transform.position += new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
        player.isDead = false;
    }

}
