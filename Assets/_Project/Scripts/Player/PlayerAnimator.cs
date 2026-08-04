using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackTriggerHash =
            Animator.StringToHash("Attack");
    private static readonly int UnsheatheTriggerHash =
            Animator.StringToHash("Unsheathe");
    private static readonly int SheatheTriggerHash =
            Animator.StringToHash("Sheathe");
    private static readonly int AttackAfterUnsheatheHash =
            Animator.StringToHash("AttackAfterUnsheathe");

    public void SetMovementSpeed(float normalizedSpeed)
    {
        animator.SetFloat(SpeedHash, normalizedSpeed, 0.1f,
    Time.deltaTime);
    }

    public void PlayAttack()
    {

        animator.SetTrigger(AttackTriggerHash);

    }
    public void PlayUnsheathe(bool value)
    {
        Debug.Log("PlayUnsheathe()");
        SetAttackAfterUnsheathe(value);
        animator.SetTrigger(UnsheatheTriggerHash);
    }

    public void PlaySheathe()
    {
        animator.SetTrigger(SheatheTriggerHash);
    }

    public void SetAttackAfterUnsheathe(bool value)
    {
        animator.SetBool(AttackAfterUnsheatheHash, value);
    }
}
