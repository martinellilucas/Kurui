using UnityEngine;


    public class AnimationEventReceiver : MonoBehaviour
    {
        private PlayerCombat playerCombat;
        private PlayerAnimator playerAnimator;

        private void Awake()
        {
            playerCombat = GetComponentInParent<PlayerCombat>();
            playerAnimator = GetComponentInParent<PlayerAnimator>();
        
        }

    public void FinishAttack()
    {
        playerCombat.FinishAttack();
        playerAnimator.SetAttackAfterUnsheathe(false);

    }
}

