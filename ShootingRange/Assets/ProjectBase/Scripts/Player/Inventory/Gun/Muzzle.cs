using UnityEngine;

public class Muzzle : MonoBehaviour
{
    Camera mainCamera;
    Magazine magazine;

    Ray ray;

    [SerializeField] private bool seeGunRay;
    [SerializeField] private GameObject firePoint;

    private void Awake()
    {
        mainCamera = Camera.main;
        magazine = GetComponent<Magazine>();
    }

    public virtual void Fire(float _fireDistance)
    {
        RaycastHit hit;

        if (magazine.currentBullets > 0)
        {
            ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit, _fireDistance))
            {
                Debug.Log(hit.collider.name);
            }
            
            magazine.currentBullets--;
        }

        if (seeGunRay)
        {
            Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * _fireDistance, Color.green);

            Debug.DrawRay(ray.origin, ray.direction * _fireDistance, Color.green);
        }
    }

}
