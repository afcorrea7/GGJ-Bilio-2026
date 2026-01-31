using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 15f;
    public bool canMove = true;
    
    [HideInInspector] public Vector2 moveInput; 
    [HideInInspector] public Vector2 aimInput;
    
    private Rigidbody2D rb;
    private Animator animator;
    
    private PlayerInput playerInput;

    //Actions as variables so we don't have to type their name ["x"] every time
    private InputAction moveAction;
    private InputAction aimAction;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable(){
        playerInput = GetComponent<PlayerInput>();

        AssignActions();
        SubscribeToActionEvents();
    }

    void AssignActions(){
        moveAction = playerInput.actions["Move"];
        aimAction = playerInput.actions["Aim"];
    }

    void SubscribeToActionEvents(){
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        aimAction.performed += OnAim;
    }

    void FixedUpdate()
    {
        Movement();
        Aiming();
    }

    void Movement()
    {
        if (canMove)
        {
            Vector2 move = new Vector2(moveInput.x, moveInput.y).normalized;
            Vector3 targetVelocity = move * moveSpeed;
            rb.linearVelocity = new Vector2(targetVelocity.x, targetVelocity.y);
            // Update Animator with movement magnitude
            animator?.SetFloat("Movement", move.magnitude);
        }
    }

    void Aiming()
    {
        
    }

    //Functions called by Player Intput Component -------------------

    public void OnMove(InputAction.CallbackContext ctx){
        Vector2 direction = ctx.ReadValue<Vector2>();
        moveInput = direction;
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        aimInput = direction;
    }


}
