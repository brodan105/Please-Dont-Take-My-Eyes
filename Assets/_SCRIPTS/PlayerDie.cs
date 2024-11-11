using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PlayerDie : MonoBehaviour
{
    public static PlayerDie instance;

    [Header("References")]
    [SerializeField] ParticleSystem deathParticlePrefab;
    public Transform playerSpawn;
    public Animator fadePanel;
    [SerializeField] GameObject playerGFX;
    [SerializeField] GameObject runParticle;

    [Header("Properties")]
    [SerializeField] float respawnTime = 5f;

    CinemachineImpulseSource impulseSource;

    public bool hasDied = false;

    private void Awake()
    {
        instance = this;

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Death" && !hasDied)
        {
            Die();
            hasDied = true;
        }
    }

    private void Respawn()
    {
        // Set timer to 0
        TimeController.instance.ResetTimer();

        // Set playerFirstMove to true so it'll check the players movement to start timer
        PlayerMovement.instance.playerFirstMove = true;

        // Move character to spawn
        transform.position = playerSpawn.position;

        // Reset player movement
        PlayerMovement p = GetComponent<PlayerMovement>();
        p.StartMovement();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Enable Run Particle
        runParticle.SetActive(true);

        // Start fade in
        StartCoroutine(fadeInDelay());

        // Enable player body
        playerGFX.GetComponent<SpriteRenderer>().enabled = true;

        hasDied = false;
    }

    private void Die()
    {
        // Stop speedrun timer
        TimeController.instance.StopTimer();

        // Disable player controls
        PlayerMovement p = GetComponent<PlayerMovement>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        p.StopMovement();

        // Shake camera
        CameraShakeManager.instance.CameraShake(impulseSource);

        // Spawn death particle
        Instantiate(deathParticlePrefab, transform.position, transform.rotation);

        // Increase vignette to 100 (use animator)
        fadePanel.SetTrigger("FadeOut");

        // Disable player body
        playerGFX.GetComponent<SpriteRenderer>().enabled = false;

        // Disable Run Particle
        runParticle.SetActive(false);

        // Play respawn coroutine to count down to reset player
        StartCoroutine(respawnTimer());
    }

    #region Timers
    private IEnumerator fadeInDelay()
    {
        yield return new WaitForSeconds(3f);

        // Decrease Vignette to default (animator)
        fadePanel.SetTrigger("FadeIn");

        // Reset check for input
        PlayerMovement.instance.CheckForInputDelay();
    }

    private IEnumerator respawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        // Respawn player
        Respawn();
    }
    #endregion
}
