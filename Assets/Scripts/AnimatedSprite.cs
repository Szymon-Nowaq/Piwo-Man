using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } // zmienna ktora przechowuje sprity, typ nazywa sie tak samo jak component
    public Sprite[] sprites; // tablica animacji, dlugosc i zawartosc ustalamy przez unity, jak nam sie bedzie chcialo pobawic to mozna ja zrobic bardziej smooth
    public float animationTime = 0.25f; // co ile bedzie zmiana animacji
    public int animationFrame {get; private set;} // indeksy tablicy
    public bool loop = true; // czy chcemy aby animacja byla w petli
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>(); // nwm co to hindus kaza³
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime); // powtarzamy funkcje Advance ca³y czas, co ustalony czas przed i po 
    }

    private void Advance()
    {
        if(!this.spriteRenderer.enabled) // jakies sprawdzenie czy wszystko jest ok, cos w styli file.good
            return;
        this.animationFrame++; // zmiana sprita
        if(this.animationFrame >= this.sprites.Length && this.loop) // jak dojdziemy do konca tablicy i animacja zapetlona to zaczynamy od zera
            this.animationFrame = 0;
        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) // czy indeks jest git
            this.spriteRenderer.sprite = this.sprites[this.animationFrame]; // podmiana z ustalonej tablicy do componentu "sprite renderer" i komórki "sprite" ³adnie to widac w unity
    }

    private void Restart()
    {
        this.animationFrame = -1; // hindus powiedzial ze moze kiedys bedziemy chcieli przerwac animaqcje i zaczac od nowa 
        Advance();
    }
}
