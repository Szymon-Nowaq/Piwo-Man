using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public JaguarNew[] jaguary;
    public Student student; 
    public Transform alkohole;
    public GameObject gameOver, win;
    public Time gameTime;
    public Movement movement { get; private set; }
    public enum Level { easy, medium, hard };
    public static Level level = Level.hard;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int maxLives = 5;
    public static int statsDeaths = 0, statsKilled = 0, statsBeers = 0 ;
    public static float statsDistance = 0;
    public bool gameOverbool = false;
    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        movement = GetComponent<Movement>();
        if ((gameOverbool && Input.GetKey(KeyCode.N)) || Input.GetKeyDown(KeyCode.R))
        {
            NewGame();
        }
        if (gameOverbool && Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown(KeyCode.K))
            StudentZgon();
    }
    private void NewGame()
    {
        gameOverbool = false;
        gameOver.SetActive(false);
        win.SetActive(false);
        statsDeaths = 0; 
        statsKilled = 0;
        statsDistance = 0; 
        statsBeers = 0;
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
        FindObjectOfType<CameraFollow>().ResetCamera();
        for (int i = 0; i < this.jaguary.Length; i++)
        {
            this.jaguary[i].gameObject.SetActive(true);
            jaguary[i].ResetJaguar();
        }
        this.student.gameObject.SetActive(true);
        this.student.ResetStudent();
    }
    private void GameOver()
    {
        statsDistance = statsDistance * Time.fixedDeltaTime * student.movement.speed;
        Debug.Log(statsDistance);
        foreach (Transform alkohol in this.alkohole)
            alkohol.gameObject.SetActive(false);
        for (int i = 0; i < this.jaguary.Length; i++)
            this.jaguary[i].gameObject.SetActive(false);
        this.student.gameObject.SetActive(false);
        gameOver.SetActive(true);
        Invoke(nameof(gameOverBool), 1);
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
        statsKilled++;
        jaguar.SetHome();
    }

    public void StudentZgon()
    {
        statsDeaths++;
        this.student.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
            Invoke(nameof(ResetState), 5);
        else
            GameOver();
    }

    public void PiwoWypite(Piwko piwo)
    {
        statsBeers++;
        piwo.gameObject.SetActive(false);
        SetScore(this.score + piwo.points);
        if (!CzyStolJestPusty())
        {
            this.student.gameObject.SetActive(false);
            win.SetActive(true);
            Invoke(nameof(gameOverBool), 1);
        }
    }

    public void WodkaWypita(Wodeczka wodka)
    {
        wodka.gameObject.SetActive(false);
        for (int i = 0; i < this.jaguary.Length; i++)
            this.jaguary[i].SetFrightened();
        if (!CzyStolJestPusty())
        {
            this.student.gameObject.SetActive(false);
            win.SetActive(true);
            Invoke(nameof(gameOverBool), 1);
        }
    }

    public void JagerWypity(Jager jager)
    {
        jager.gameObject.SetActive(false);
        SetLives(this.lives + jager.jagerHealth);
        Debug.Log("jager");
        if (!CzyStolJestPusty())
        {
            this.student.gameObject.SetActive(false);
            win.SetActive(true);
            Invoke(nameof(gameOverBool), 1);
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

    public void gameOverBool()
    {
        gameOverbool = true;
    }

    public static void DistancePlus()
    {
        statsDistance++;
    }
}
