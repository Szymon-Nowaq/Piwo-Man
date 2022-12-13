using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Jaguar[] jaguary;
    public Transform student; // tutaj powinien byc typ Student, ale wtedy nie da siê dodac obiektu z poziomu unity do GM
    public Transform alkohole;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
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
        for (int i = 0; i < this.jaguary.Length; i++)
            this.jaguary[i].gameObject.SetActive(true);
        this.student.gameObject.SetActive(true);
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
    }
    private void SetLives(int newLives)
    {
        this.lives = newLives;
    }

    public void JaguarPokonany(Jaguar jaguar)
    {
        SetScore(this.score + jaguar.pktPokonanieJaguara);
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
}
