using Unity.VisualScripting;
using UnityEngine;

public abstract class GunBase : ItemBase, IGun
{
    [Header("Gun Settings")]
    [SerializeField] public int totalBullets;
    [SerializeField] public int magazineCapacity;
    [SerializeField] public int currentBullets;


    [Header("Fire Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireDistance;
    [SerializeField] bool seeGunRay = false;

    Animator gunAnimator;

    private void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

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

            Debug.Log(currentBullets + " " + totalBullets);
            Debug.Log("Fire girildi");
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
        Debug.Log("Mermi : " + totalBullets);
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
