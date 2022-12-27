using UnityEngine;
using static JaguarNew;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } // zmienna ktora przechowuje sprity, typ nazywa sie tak samo jak component
    public Sprite[] sprites;
    public Sprite white;// tablica animacji, dlugosc i zawartosc ustalamy przez unity, jak nam sie bedzie chcialo pobawic to mozna ja zrobic bardziej smooth
    public float animationTime = 0.25f; // co ile bedzie zmiana animacji
    public int animationFrame {get; private set;} // indeksy tablicy
    public int i = 1;
    public bool loop = true, isJagFri = false; // czy chcemy aby animacja byla w petli
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
        if (!isJagFri)
        {
            if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) // czy indeks jest git
                this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }// podmiana z ustalonej tablicy do componentu "sprite renderer" i komórki "sprite" ³adnie to widac w unity
        else
        {
            if (i == 2)
                i = 1;
            if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) // czy indeks jest git
                if (i == 1)
                {
                    if (((animationFrame + 1) % 2) == 0)
                        this.spriteRenderer.sprite = this.white;
                    else
                        this.spriteRenderer.sprite = null;
                }
                else
                {
                    if (((animationFrame + 1) % 2) == 1)
                        this.spriteRenderer.sprite = this.white;
                    else
                        this.spriteRenderer.sprite = null;
                }
        }
    }

    private void Restart()
    {
        this.animationFrame = -1; // hindus powiedzial ze moze kiedys bedziemy chcieli przerwac animaqcje i zaczac od nowa 
        Advance();
    }
}
