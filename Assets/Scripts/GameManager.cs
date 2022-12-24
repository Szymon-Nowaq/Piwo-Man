using UnityEngine;


public class GameManager : MonoBehaviour
{
    public JaguarNew[] jaguary;
    public Transform student; // tutaj powinien byc typ Student, ale wtedy nie da siê dodac obiektu z poziomu unity do GM
    public Transform alkohole;

    public int score { get; private set; }
    public int lives { get; private set; }
    public int maxLives = 5;
    public int jaguarMultiplier { get; private set; } = 1;
    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        if ((lives <= 0 && Input.anyKeyDown) || Input.GetKeyDown(KeyCode.R))
        {
            NewGame();
        }
    }
    private void NewGame()
    {
        SetLives(5);
        SetScore(0);
        NewRound();
    }
    private void NewRound()
    {
        foreach (Transform alkohol in this.alkohole)
            alkohol.gameObject.SetActive(true);
        ResetState();
    }

    private void ResetState()
    {
        ResetJaguarMultiplier();
        for (int i = 0; i < this.jaguary.Length; i++)
            this.jaguary[i].gameObject.SetActive(true);
        this.student.gameObject.SetActive(true);
        FindObjectOfType<Student>().ResetStudent();
    }
    private void GameOver()
    {
        foreach (Transform alkohol in this.alkohole)
            alkohol.gameObject.SetActive(false);
        for (int i = 0; i < this.jaguary.Length; i++)
            this.jaguary[i].gameObject.SetActive(false);
        this.student.gameObject.SetActive(false);
    }
    private void SetScore(int newScore)
    {
        this.score = newScore;
        FindObjectOfType<BarPoints>().setPoints(this.score);
    }
    private void SetLives(int newLives)
    {
        if (newLives > maxLives)
            this.lives = maxLives;
        else
            this.lives = newLives;
        FindObjectOfType<BarHealth>().setHealth(this.lives);
    }

    public void JaguarPokonany(JaguarNew jaguar)
    {
        SetScore(this.score + (jaguar.pktPokonanieJaguara * this.jaguarMultiplier));
        this.jaguarMultiplier++;
    }

    public void StudentZgon()
    {
        this.student.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 5);
        }
        else
        {
            GameOver();
        }
    }

    public void PiwoWypite(Piwko piwo)
    {
        piwo.gameObject.SetActive(false);
        SetScore(this.score + piwo.points);
        if (!CzyStolJestPusty())
        {
            this.student.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 4.0f);
        }
    }

    public void WodkaWypita(Wodeczka wodka)
    {
        wodka.gameObject.SetActive(false);
        if (!CzyStolJestPusty())
        {
            this.student.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 4.0f);
        }
        CancelInvoke();
        Invoke(nameof(ResetJaguarMultiplier), 8.0f);
    }

    public void JagerWypity(Jager jager)
    {
        jager.gameObject.SetActive(false);
        SetLives(this.lives + jager.jagerHealth);
        Debug.Log("jager");
        if (!CzyStolJestPusty())
        {
            this.student.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 4.0f);
        }
    }

    private bool CzyStolJestPusty()
    {
        foreach (Transform alkohol in this.alkohole)
        {
            if (alkohol.gameObject.activeSelf)
                return true;
        }
        return false;
    }
    private void ResetJaguarMultiplier()
    {
        jaguarMultiplier = 1;
    }
}
