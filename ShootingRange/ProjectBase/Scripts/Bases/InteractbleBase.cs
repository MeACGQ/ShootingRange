using UnityEngine;

public abstract class InteractbleBase : MonoBehaviour, IInteractble
{
    Outline currentOutline;

    private void Awake()
    {
        currentOutline = GetComponent<Outline>();
    }

    public virtual void Interact()
    {

    }

    public virtual void GlowObject(bool isGlowing)
    {
        if (currentOutline == null) return;

        currentOutline.enabled = isGlowing;
    }

    public virtual void ClearOutline(Outline currentOutline)
    {
        currentOutline.enabled = false;
    }
}