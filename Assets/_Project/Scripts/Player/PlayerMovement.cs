using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerMovement : MonoBehaviour //monobehaviour pertenece al unity engine player movement hereda de monobehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float runSpeed = 6f;

    private float currentSpeed;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float rotationSpeed = 720f; //grados por segundo

    private bool isRunning; 
    private PlayerAnimator playerAnimator;
    private Vector2 movementInput;
    private Vector3 moveDirection;
    public Vector3 MoveDirection => moveDirection;
    private CharacterController characterController;
    private PlayerInputHandler playerInputHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() //awake, antes de start, convencion para obtener referencias 
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        
    }

    // Update is called once per frame
    void Update() //update proviene de monobehaviour
    {
        ReadInput();
       
        Rotate();

        Move();

        UpdateAnimation();
    }

    private void ReadInput()
    {
        movementInput = playerInputHandler.MoveInput;
        isRunning = playerInputHandler.RunPressed;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        if (MoveDirection == Vector3.zero)
        {
            currentSpeed = 0f;
        }
        else if ( isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
   
    }
    private void Rotate()
    {
        if (MoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
    private void Move()
    {
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
    
    private float GetNormalizedMovementSpeed()
    {
        return currentSpeed / runSpeed;
    }

    private void UpdateAnimation()
    {
        playerAnimator.SetMovementSpeed(GetNormalizedMovementSpeed());
    }
}
