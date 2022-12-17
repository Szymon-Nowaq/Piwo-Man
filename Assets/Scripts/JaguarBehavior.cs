using UnityEngine;


[RequireComponent(typeof(Jaguar))]
public abstract class JaguarBehavior : MonoBehaviour
{
    public Jaguar jaguar { get; private set; }
    public float duration;

    private void Awake()
    {
        this.jaguar = GetComponent<Jaguar>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;


        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }

    

}
