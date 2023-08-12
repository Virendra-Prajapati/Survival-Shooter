using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    private const string PLAYER_HEALTH = "PlayerHealth";

    public int startingHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    int currentHealth;

    public int CurrentHealth { get { return currentHealth; } }

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    private void Start()
    {
        DatabaseManager.Instance.OnLoadGame += DatabaseManager_OnLoadGame;
        DatabaseManager.Instance.OnSaveGame += DatabaseManager_OnSaveGame;
    }

    private void DatabaseManager_OnSaveGame(object sender, System.EventArgs e)
    {
        PlayerPrefs.SetInt(PLAYER_HEALTH, currentHealth);
    }

    private void DatabaseManager_OnLoadGame(object sender, System.EventArgs e)
    {
        if (PlayerPrefs.HasKey(PLAYER_HEALTH))
        {
            currentHealth = PlayerPrefs.GetInt(PLAYER_HEALTH);
            healthSlider.value = currentHealth;
        }
    }

    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }

    
}
