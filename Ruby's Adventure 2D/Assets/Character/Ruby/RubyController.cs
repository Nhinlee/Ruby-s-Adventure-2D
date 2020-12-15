using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Serialize Field
    [SerializeField]
    private HealthBarUI healbarUI;
    [SerializeField]
    private float timeInVincible = 2.0f;
    [SerializeField]
    private float maxHealth = 5;
    [SerializeField]
    private Animator myAnimator;
    [SerializeField]
    private Rigidbody2D myRigid;
    [SerializeField]
    private AudioSource myAudioSource;
    [SerializeField]
    private ProjectTile projectTilePrefab;
    [SerializeField]
    private RubyMovement myMovement;
    [SerializeField]
    private AudioClip getHurtAudioClip;
    [SerializeField]
    private float launchSpeed;

    // Property
    public float CurrentHealth { get; private set; }

    // Fields
    private float invincibleTimer = 0f;
    private bool isInvincible = false;

    // Unity Message (Events)
    private void Start()
    {
        CurrentHealth = maxHealth;
        healbarUI.SetValue(CurrentHealth / maxHealth);
    }

    private void Update()
    {
        AutoDescreaseTimer();
        Launch();
        TalkToNPC();
    }

    // Public Methods
    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            // Logic
            if (isInvincible) return;
            isInvincible = true;
            invincibleTimer = timeInVincible;
            // Animation
            myAnimator.SetTrigger("Hitting");
            // Sound
            PlaySound(getHurtAudioClip);
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, maxHealth);
        healbarUI.SetValue(CurrentHealth / maxHealth);
    }

    public void PlaySound(AudioClip audioClip)
    {
        myAudioSource.PlayOneShot(audioClip);
    }

    // Private Methods
    private void AutoDescreaseTimer()
    {
        // Descrease Time on Timer
        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer <= 0f) isInvincible = false;
    }

    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var projectTileGameObject = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTileGameObject.Launch(myMovement.LookDirection, launchSpeed);
            myAnimator.SetTrigger("Launch");
        }
    }
    private void TalkToNPC()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(myRigid.position + Vector2.up * 0.2f,
                myMovement.LookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                var NPCController = hit.collider.gameObject.GetComponent<NPCJambiController>();
                if(NPCController != null)
                {
                    NPCController.ShowDialog();
                }
            }
        }
    }

}
