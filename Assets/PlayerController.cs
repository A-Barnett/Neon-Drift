using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float angularSpeed;
    public float driftForce;
    public ParticleSystem particleSystem;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        speed += (PlayerPrefs.GetInt("Speed") * 2);
        angularSpeed += (PlayerPrefs.GetInt("Rotate")*0.05f);
        var emission = particleSystem.emission;
        emission.rateOverTime = Math.Clamp((30 + (PlayerPrefs.GetInt("Speed") * 10)),0,100);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            ParticleSystem trail = gameObject.GetComponentInChildren<ParticleSystem>();
            Collider2D collider2 = gameObject.GetComponent<Collider2D>();
            Destroy(trail);
            Destroy(sprite);
            Destroy(collider2);
           StartCoroutine(ReloadScene());
        }
    }
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+PlayerPrefs.GetInt("Count"));
        if (PlayerPrefs.GetInt("Count") > PlayerPrefs.GetInt("High"))
        {
            PlayerPrefs.SetInt("High",PlayerPrefs.GetInt("Count"));
        }
        PlayerPrefs.SetInt("Count",0);
        GameObject mainMenu = GameObject.Find("Menu");
        MainMenu main = mainMenu.GetComponentInChildren<MainMenu>();
        main.Menu();
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float reset = Input.GetAxis("Reset");
        if (vertical > 0 && particleSystem)
        {
            Vector2 direction = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.right;
            Vector2 sideways = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.up * horizontal * driftForce;
            rb.AddForce(direction * vertical * speed + sideways);
            particleSystem.Play();
        }
        else if(particleSystem)
        {
            particleSystem.Stop();
            
        }
        if (horizontal != 0)
        {
            rb.AddTorque(-horizontal*angularSpeed);
        }
        if (reset > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
