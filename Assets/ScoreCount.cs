using TMPro;
using UnityEngine;


public class ScoreCount : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public GameObject missile;
    public float spawnTime;
    private float timer;
    public int missilecount =1;

  

    private void Update()
    {
        score = PlayerPrefs.GetInt("Count");
        scoreText.text = "Score: " + score;
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            SpawnMissile(missilecount);
             missilecount++;
            timer = 0;
        }
    }

    public void SpawnMissile(int missilecounts)
    {
        float randomX = UnityEngine.Random.Range(0, 100);
        float randomY = UnityEngine.Random.Range(0, 100);
        Vector3 randomPos = new Vector3(randomX, randomY, 0);
        GameObject newMissile = Instantiate(missile, randomPos, Quaternion.Euler(0, 0, 0));
        HomingMissile homingMissile = newMissile.GetComponent<HomingMissile>();
        homingMissile.speed += missilecounts *3f;
        homingMissile.turnSpeed += missilecounts *0.3f;
    }
}