using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas canvas;
    public Canvas oldcanvas;
    public int speedMoney, rotateMoney, speedInc, rotateInc;
    public Image speedI;
    public Image rotI;

    
    public void Menu()
    {
        Time.timeScale = 0;
        canvas.enabled = true;
        oldcanvas.enabled = false;
        UpdateMenu();

    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
        oldcanvas.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpeedUp()
    {
        int cost = speedMoney + (PlayerPrefs.GetInt("Speed") * speedInc);
        if (PlayerPrefs.GetInt("Money") >= cost)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost);
            PlayerPrefs.SetInt("Speed", PlayerPrefs.GetInt("Speed") + 1);
        }
        else
        {
            StartCoroutine(FlashRedCoroutine(speedI));
        }

        UpdateMenu();

    }

    public void RotateUp()
    {
        int cost = speedMoney + (PlayerPrefs.GetInt("Rotate") * rotateInc);
        if (PlayerPrefs.GetInt("Money") >= cost)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost);
            PlayerPrefs.SetInt("Rotate", PlayerPrefs.GetInt("Rotate") + 1);
        }
        else
        {
            StartCoroutine(FlashRedCoroutine(rotI));
        }

        UpdateMenu();
    }

    public void UpdateMenu()
    {
        TextMeshProUGUI money = canvas.transform.Find("Money").GetComponent<TextMeshProUGUI>();
        money.text = "Money: " + PlayerPrefs.GetInt("Money");

        TextMeshProUGUI high = canvas.transform.Find("highScore").GetComponent<TextMeshProUGUI>();
        high.text = "High Score: " + PlayerPrefs.GetInt("High");

        int costS = speedMoney + (PlayerPrefs.GetInt("Speed") * speedInc);
        TextMeshProUGUI speedcount = canvas.transform.Find("SpeedCount").GetComponentInChildren<TextMeshProUGUI>();
        speedcount.text = costS.ToString();
        TextMeshProUGUI speedtxt = canvas.transform.Find("Speedtext").GetComponent<TextMeshProUGUI>();
        speedtxt.text = "Speed: " + (PlayerPrefs.GetInt("Speed") + 1);

        int costR = rotateMoney + (PlayerPrefs.GetInt("Rotate") * rotateInc);
        TextMeshProUGUI rotatecount = canvas.transform.Find("RotateCount").GetComponentInChildren<TextMeshProUGUI>();
        rotatecount.text = costR.ToString();
        TextMeshProUGUI rotatetxt = canvas.transform.Find("Rotatetext").GetComponent<TextMeshProUGUI>();
        rotatetxt.text = "Rotate: " + (PlayerPrefs.GetInt("Rotate") + 1);
    }

    public void MenuReset()
    {
        PlayerPrefs.SetInt("Speed", 0);
        PlayerPrefs.SetInt("Rotate", 0);
        PlayerPrefs.SetInt("Money", 100);
        UpdateMenu();
    }
    
        IEnumerator FlashRedCoroutine(Image image)
    {
        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0;
        float totalTime = 0.5f;

        while (elapsedTime < totalTime)
        {
            image.color = Color.Lerp(Color.white, Color.red, elapsedTime / totalTime);
            elapsedTime = Time.realtimeSinceStartup - startTime;
            yield return null;
        }

        startTime = Time.realtimeSinceStartup;
        elapsedTime = 0;
        totalTime = 1f;

        while (elapsedTime < totalTime)
        {
            image.color = Color.Lerp(Color.red, Color.white, elapsedTime / totalTime);
            elapsedTime = Time.realtimeSinceStartup - startTime;
            yield return null;
        }
    }
}
