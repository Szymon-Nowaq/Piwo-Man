using UnityEngine;

public class Jaguar : MonoBehaviour
{
    public hindusMovement movement { get; private set; }

    public JaguarHome home { get; private set; }

    public JaguarScatter scatter { get; private set; }

    public JaguarChase chase { get; private set; }

    public JaguarFrightened frightened { get; private set; }

    public JaguarBehavior initialBehavior;
    public Transform target;
    public int pktPokonanieJaguara = 100;
    

    private void Awake()
    {
        this.movement = GetComponent<hindusMovement>();
        this.home = GetComponent<JaguarHome>();
        this.scatter = GetComponent<JaguarScatter>();
        this.chase = GetComponent<JaguarChase>();
        this.frightened = GetComponent<JaguarFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }
        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Student"))
        {
            if (this.frightened.enabled)
            {
               // FindObjectOfType<GameManager>().JaguarPokonany(this);
            }else
            {
                FindObjectOfType<GameManager>().StudentZgon();
            }
        }
    }



}
