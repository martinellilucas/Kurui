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
    private PlayerAnimator playerAnimator;
    private bool guardTriggered;
    private bool attackTriggered;
    private bool lockTriggered;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        playerCombat = GetComponent<PlayerCombat>();
        combatTargeting = GetComponent<CombatTargeting>();
        playerAnimator = GetComponent<PlayerAnimator>();
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
            CombatTarget excludedTarget = combatTargeting.CurrentTarget;

            //intentamos cambiar a otro enemigo
            if (combatTargeting.TryTargetNearestEnemy(excludedTarget))
            {
                return;
            }
            //no habia
            combatTargeting.ClearTarget();
            ChangeState(PlayerState.Guard);
            playerAnimator.SetCombat(false);
            return;
        }
        
        //no teniamos target, intentamos conseguir uno
        if (!combatTargeting.TryTargetNearestEnemy())
            return;

        if(currentState == PlayerState.Exploration)
        {
            ChangeState(PlayerState.Combat);
            playerAnimator.SetCombat(true);
            playerCombat.Unsheathe();
        }
        else if(currentState == PlayerState.Guard)
        {
            ChangeState(PlayerState.Combat);
            playerAnimator.SetCombat(true);

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
            playerAnimator.SetCombat(false);
            combatTargeting.ClearTarget();
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
