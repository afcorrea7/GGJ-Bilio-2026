using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;
    
    [HideInInspector] public Vector2 moveInput; 
    [HideInInspector] public Vector2 aimInput;
    
    private Vector2 aimPosicion;
    private Rigidbody2D rb;
    private Animator animator;
    private PointerRotator pointer;
    private ThrowableHolder thisThrowableHolder;
    private PlayerInput playerInput;

    //Actions as variables so we don't have to type their name ["x"] every time
    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction attackAction;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        pointer = GetComponentInChildren<PointerRotator>();
        thisThrowableHolder = GetComponentInChildren<ThrowableHolder>();
    }

    void OnEnable(){
        playerInput = GetComponent<PlayerInput>();

        AssignActions();
        SubscribeToActionEvents();
    }

    void AssignActions(){
        moveAction = playerInput.actions["Move"];
        aimAction = playerInput.actions["Aim"];
        playerInput.actions.FindAction("Attack").performed += (context) => OnAttack(context);
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
        aimPosicion = Camera.main.ScreenToWorldPoint(aimInput);
        pointer.pointerposition = aimPosicion;
    }

    //Functions called by Player Intput Component -------------------
    public void OnMove(InputAction.CallbackContext ctx){
        Vector2 direction = ctx.ReadValue<Vector2>();
        moveInput = direction;
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = ctx.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        aimInput = mousePos;
    }

    public void OnAttack(InputAction.CallbackContext _)
    {
        Debug.Log("ATTAAAAAAAACK");
        thisThrowableHolder.UseThrowable();
    }


}
