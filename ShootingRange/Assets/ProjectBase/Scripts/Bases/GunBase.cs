using Unity.VisualScripting;
using UnityEngine;

public abstract class GunBase : ItemBase, IGun
{
    [Header("Gun Settings")]
    [SerializeField] private int totalBullets;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private int currentBullets;


    [Header("Fire Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireDistance;
    [SerializeField] bool seeGunRay = false;

    public override void UseItem()
    {
        Fire();
    }

    Ray ray;

    public virtual void Fire()
    {
        RaycastHit hit;

        if (currentBullets > 0)
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit, fireDistance))
            {
                Debug.Log(hit.collider.name);
            }

            currentBullets--;
        }
    }
    public virtual void Reload()
    {
        if (totalBullets > 0)
        {
            int magazine;

            magazine = magazineCapacity - currentBullets;

            if (totalBullets - magazine >= 0)
                totalBullets = totalBullets - magazine;
            else
                totalBullets = 0;

            currentBullets += magazine;
        }
    }

    private void FixedUpdate()
    {
        if (seeGunRay)
        {
            Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * fireDistance, Color.green);

            Debug.DrawRay(ray.origin, ray.direction * fireDistance, Color.green);
        }
    }

    public virtual void GetAim()
    {

    }
}
