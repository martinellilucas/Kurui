using Unity.U2D.Physics;
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
    private CombatTargeting combatTargeting;
    private bool guardTriggered;
    private bool attackTriggered;
    private bool lockTriggered;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        playerCombat = GetComponent<PlayerCombat>();
        combatTargeting = GetComponent<CombatTargeting>();
        
    }

    // Update is called once per frame
    void Update()
    {
        guardTriggered = inputHandler.GuardTriggered;
        attackTriggered = inputHandler.AttackTriggered;
        lockTriggered = inputHandler.LockTriggered;
        HandleGlobalInput();
        HandleStateTransitions();
      
       
    }

    private void HandleGlobalInput()
    {
        if (!lockTriggered)
            return;
    
        if (combatTargeting.CurrentTarget!= null)
        {        
            combatTargeting.ClearTarget();
            return;
        }
      
        if (!combatTargeting.TryTargetNearestEnemy())
            return;

        if(currentState == PlayerState.Exploration)
        {
            ChangeState(PlayerState.Combat);
            playerCombat.Unsheathe();
        }
        else if(currentState == PlayerState.Guard)
        {
            ChangeState(PlayerState.Combat);
        }
    
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
        if (attackTriggered)
        {
            playerCombat.TryAttack(PlayerCombat.AttackType.Attack01);
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
