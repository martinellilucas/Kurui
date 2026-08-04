using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform handSocket;
    [SerializeField] private Transform hipSocket;
    [SerializeField] private Vector3 hipLocalPosition;
    [SerializeField] private int damage = 10;
    private WeaponHitbox weaponHitbox;
    public int Damage => damage;
    private void Awake()
    {
        weaponHitbox = GetComponentInChildren<WeaponHitbox>();
    }
    public void Equip()
    {
        transform.SetParent(handSocket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Unequip()
    {
        transform.SetParent(hipSocket);
        transform.localPosition = hipLocalPosition;
        transform.localRotation = Quaternion.identity;
    }
}
