using Unity.VisualScripting;
using UnityEngine;

public abstract class GunBase : ItemBase, IGun
{
    [Header("Gun Components")]
    [SerializeField] private Magazine magazine;
    [SerializeField] private Muzzle muzzle;
    
    [Header("Gun Settings")]
    [SerializeField] public int totalBullets;

    [Header("Fire Settings")]
    [SerializeField] private float fireDistance;

    public override void UseItem()
    {
        Fire();
    }

    public virtual void Fire()
    {
        muzzle.Fire(fireDistance);
    }
    public virtual void Reload()
    {
        int newBullets = magazine.Reload(totalBullets);

        totalBullets = newBullets;
    }

    public virtual void GetAim()
    {

    }
}
