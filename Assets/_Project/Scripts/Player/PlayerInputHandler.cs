using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public bool RunPressed { get; private set; }

    public bool GuardTriggered { get; private set; }

    public bool AttackTriggered { get; private set; }

    private PlayerInputActions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }
    
    private void UpdateInputs()
    {
        MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
        RunPressed = inputActions.Player.Run.IsPressed();
        GuardTriggered = inputActions.Player.Guard.WasPressedThisFrame();
        AttackTriggered = inputActions.Player.Attack.WasPressedThisFrame();
    }

   
}
