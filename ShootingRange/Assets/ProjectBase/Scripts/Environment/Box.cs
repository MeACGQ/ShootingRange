using UnityEngine;

public class Box : InteractbleBase
{
    public override void Interact()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0f, 15f));
    }
}
