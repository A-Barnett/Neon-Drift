using UnityEngine;

public class StarField : MonoBehaviour
{
    public GameObject star;
    public int numOfStars;
    private Transform[] stars;

    void Start()
    {
        stars = new Transform[numOfStars];
        for (int i = 0; i < numOfStars; i++)
        {
            GameObject newStar = Instantiate(star, transform);
            newStar.transform.position = new Vector3(Random.Range(-30f, 130f), Random.Range(-16.85f, 116.85f), 1f);
            stars[i] = newStar.transform;
        }
    }
}