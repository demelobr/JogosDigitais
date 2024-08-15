using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public float radius;
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public LayerMask layer;
    public bool isDead;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;

    private Player player;
    private bool isPlayerDetected;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && isPlayerDetected)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                animControl.PlayAnim(2);
            }
            else
            {
                animControl.PlayAnim(1);
            }

            float deltaX = player.transform.position.x - transform.position.x;

            if (deltaX > 0)
            {
                transform.eulerAngles = new Vector2(0,0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (hit != null)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
            animControl.PlayAnim(0);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
