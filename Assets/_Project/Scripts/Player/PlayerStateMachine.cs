using UnityEngine;
    public enum PlayerState
    {
        Exploration,
        Guard,
        Combat
    };

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState currentState;
    public PlayerState CurrentState => currentState;
    private PlayerInputHandler inputHandler;
    private PlayerCombat playerCombat;
    
    private bool guardTriggered;
    private bool attackTriggered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        playerCombat = GetComponent<PlayerCombat>();
        
    }

    // Update is called once per frame
    void Update()
    {
        guardTriggered = inputHandler.GuardTriggered;
        attackTriggered = inputHandler.AttackTriggered;
        HandleStateTransitions();
       
    }
    private void HandleStateTransitions()
    {
        switch (currentState)
        {
            case PlayerState.Exploration:
                HandleExplorationTransitions();    
            break;
            case PlayerState.Guard:
                HandleGuardTransitions();
                break;
            case PlayerState.Combat:
                HandleCombatTransitions();
                break;
            
        }
    }
    private void HandleExplorationTransitions()
    {

        if (guardTriggered)
        {
            ChangeState(PlayerState.Guard);
            playerCombat.Unsheathe();
            
        }
        else if (attackTriggered)
            {
            ChangeState(PlayerState.Guard);
            playerCombat.UnsheatheAndAttack();

        }
        

    }
    
    private void HandleGuardTransitions()
    {
       
        if (guardTriggered)
        {
            ChangeState(PlayerState.Exploration);
            playerCombat.Sheathe();
        }
        if (attackTriggered)
        {
        
            playerCombat.TryAttack(PlayerCombat.AttackType.Attack01);
        }
    }
    private void HandleCombatTransitions()
    {
       if (guardTriggered)
        {
            ChangeState(PlayerState.Exploration);
            playerCombat.Sheathe();
        }
    }
    private void ChangeState(PlayerState newState)
    {
        if (currentState == newState)
        {
            return;
        }
        ExitState(currentState);
        currentState = newState;
        EnterState(currentState);
    }
    private void ExitState(PlayerState state)
    {

    }
    private void EnterState(PlayerState state)
    {

    }
}
