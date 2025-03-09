using UnityEngine;

public class RenderTrail : MonoBehaviour
{
    public TrailRenderer trail;
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.SetPosition(0,new Vector2(10,10));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
