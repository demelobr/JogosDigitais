using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] public float totalHealth;  //ADD
    [SerializeField] public float currentHealth;  //ADD


    public bool isPaused;
    public bool isDead;

    private Rigidbody2D rig;
    private PlayerItens playerItens;

    private float initialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isJumping;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private Vector2 _direction;

    [HideInInspector] public int handlingObj;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool isJumping
    {
        get { return _isJumping; }
        set { _isJumping = value; }
    }

    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }

    private void Start()
    {
        currentHealth = totalHealth; //ADD
        rig = GetComponent<Rigidbody2D>();
        playerItens = GetComponent<PlayerItens>();
        initialSpeed = speed;
    }

    private void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 3;
            }

            OnInput();
            OnRunnig();
            OnRolling();
            OnJumping();
            OnCutting();
            OnDigging();
            OnWatering();
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            OnMove();
        }
    }

    #region Movement

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }

    void OnRunnig() 
    {
        bool isMoving = Input.GetKey(KeyCode.UpArrow) ||
                        Input.GetKey(KeyCode.DownArrow) ||
                        Input.GetKey(KeyCode.LeftArrow) ||
                        Input.GetKey(KeyCode.RightArrow) ||
                        Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.D);

        if (Input.GetKeyDown(KeyCode.LeftShift) && isMoving)
        {
            speed = runSpeed;
            _isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //speed = rollSpeed;
            speed = 0;
            _isRolling = true;
        } 
        else if (Input.GetMouseButtonUp(1))
        {
            speed = initialSpeed;
            _isRolling = false;
        }
    }

    void OnJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //speed = jumpSpeed;
            speed = 0;
            _isJumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = initialSpeed;
            _isJumping = false;
        }
    }

    void OnCutting()
    {
        if (handlingObj == 1)
        {

            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
    }

    void OnDigging()
    {
        if (handlingObj == 2)
        {

            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
    }

    void OnWatering()
    {
        if (handlingObj == 3)
        {

            if (Input.GetMouseButtonDown(0) && playerItens.currentWater > 0)
            {
                isWatering = true;
                speed = 0;
            }
            else if (Input.GetMouseButtonUp(0) || playerItens.currentWater < 0)
            {
                isWatering = false;
                speed = initialSpeed;
            }

            if (isWatering)
            {
                playerItens.currentWater -= 0.01f;
            }
        }
    }

    #endregion
}
