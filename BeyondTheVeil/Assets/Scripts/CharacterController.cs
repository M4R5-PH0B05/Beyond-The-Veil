using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// This defines the player mask state
    /// </summary>
    private enum MaskState  
    {
        none = 0,
        doubleJump = 1,
        grapple = 2,
        wallTangibility = 3
    }


    /// <summary>
    /// The player direction
    /// </summary>
    private Vector2 m_playerDirection;

    /// <summary>
    /// The move input action
    /// </summary>
    private InputAction m_move;
    /// <summary>
    /// The jump input action
    /// </summary>
    private InputAction m_jump;
    /// <summary>
    /// The Players Rigidbody2D
    /// </summary>
    private Rigidbody2D RB2D;

    /// <summary>
    /// The currently selectedMask
    /// </summary>
    private MaskState m_maskState;

    private bool m_canjump = false;
    private float m_jumpcooldown = 0.1f;
    private float m_jumptimeout;

    /// <summary>
    /// Defined for the velocity player jumps 
    /// </summary>
    [SerializeField] private float m_jumpSpeed;
   
    /// <summary>
    /// Defined for the velocity player moves 
    /// </summary>
    [SerializeField] private float m_moveSpeed;
    /// <summary>
    /// 
    /// </summary>
    private int m_jumpcounter = 1;

    void Awake()
    {
        m_move = InputSystem.actions.FindAction("Move");
        m_jump = InputSystem.actions.FindAction("Jump");
        // Gets players rigidbody
        RB2D = GetComponent < Rigidbody2D > ();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //adds the move to position
        transform.position += new Vector3(m_playerDirection.x * m_moveSpeed, m_playerDirection.y * m_moveSpeed, 0);
    }

    /// <header> Charecter Inputs </header>

    /// <summary>
    /// This defines how the player moves
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleMove(InputAction.CallbackContext ctx)
    {
        // makes it so player cant jump with W key
        if (ctx.performed && ctx.ReadValue<Vector2>().y <= 0)
        {
            
                m_playerDirection = ctx.ReadValue<Vector2>();
            
        }
        else if (ctx.canceled)
        {
            m_playerDirection = Vector2.zero;
        }
       
    }

    /// <summary>
    /// This defines how the player jumps
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleJump(InputAction.CallbackContext ctx)
    {
        
        if (RB2D.linearVelocityY < 0.001 && RB2D.linearVelocityY > -0.001)
        {
            if (m_maskState == MaskState.doubleJump)
            {
                m_jumpcounter = 2;
            }
            else if (m_maskState != MaskState.doubleJump)
            {
                m_jumpcounter = 1;
            }
        }
        if (m_jumpcounter > 0 && Time.time > m_jumptimeout)
        {
            m_jumpcounter--;
            RB2D.AddForce(new Vector2(0, 15 * m_jumpSpeed), ForceMode2D.Impulse);
            m_jumptimeout = Time.time + m_jumpcooldown;
            

            
        }
          
    }

    public void HandleMaskSwitchDJump(InputAction.CallbackContext ctx)
    {
        m_maskState = MaskState.doubleJump;
        Debug.Log("double jump");
    }

    public void HandleMaskSwitchGrapple(InputAction.CallbackContext ctx)
    {
        m_maskState = MaskState.grapple;
        Debug.Log("grapple");
    }

    public void HandleMaskSwitchWall(InputAction.CallbackContext ctx)
    {
        m_maskState = MaskState.wallTangibility;
        Debug.Log("wall");
    }

}
