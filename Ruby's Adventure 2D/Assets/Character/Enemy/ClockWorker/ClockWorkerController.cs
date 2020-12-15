using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockWorkerController : MonoBehaviour
{
    // Serialize Fields
    [SerializeField]
    private float moveSpeed = 0.5f;
    [SerializeField]
    private float moveInterval = 2f;
    [SerializeField]
    private float hurtPlayerPoint = 1f;
    [SerializeField]
    private Rigidbody2D myRigd;
    [SerializeField]
    private Animator myAnimator;
    [SerializeField]
    private AudioSource myAudioSource;
    [SerializeField]
    private ParticleSystem smokeEffect;

    // Private Fields
    private Vector2 lookDirection = new Vector2(1f, 0f);
    private Coroutine coroutineAutoMove;

    // Unity Message
    private void Update()
    {
        if(coroutineAutoMove == null)
        {
            coroutineAutoMove = StartCoroutine(CoroutineAutoMove());
        }
        UpdateAnimatorParam();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var controller = other.gameObject.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-hurtPlayerPoint);
        }
    }

    public void Fix()
    {
        // Update Physics
        myRigd.simulated = false;
        // Update Animation
        myAnimator.SetTrigger("IsFixed");
        // Update Effect
        smokeEffect.Stop();
    }
    
    public void PlaySound(AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);
    }

    // Private Methods
    private void UpdateAnimatorParam()
    {
        myAnimator.SetFloat("MoveX", lookDirection.x);
        myAnimator.SetFloat("MoveY", lookDirection.y);
    }
    private IEnumerator CoroutineAutoMove()
    {
        // TODO: Refactor (repeat code so much)
        // Move Left
        float timer = Time.time;
        float endTimer = timer + moveInterval;
        while (timer < endTimer)
        {
            MoveLeft();
            timer += Time.deltaTime;
            yield return null;
        }

        // Move Right
        timer = Time.time;
        endTimer = timer + moveInterval;
        while (timer < endTimer)
        {
            MoveRight();
            timer += Time.deltaTime;
            yield return null;
        }

        // Move Up
        timer = Time.time;
        endTimer = timer + moveInterval;
        while (timer < endTimer)
        {
            MoveUp();
            timer += Time.deltaTime;
            yield return null;
        }

        // Move Down
        timer = Time.time;
        endTimer = timer + moveInterval;
        while (timer < endTimer)
        {
            MoveDown();
            timer += Time.deltaTime;
            yield return null;
        }
        // Stop coroutine to start new one
        StopCoroutine(coroutineAutoMove);
        coroutineAutoMove = null;
    }
    private void ResetSpeed()
    {
        lookDirection = Vector2.zero;
    }
   
    private void Move()
    {
        var moveDistance = new Vector2(lookDirection.x * moveSpeed, lookDirection.y * moveSpeed);
        myRigd.MovePosition((Vector2)transform.position + moveDistance);
    }
    private void MoveLeft()
    {
        ResetSpeed();
        lookDirection.x = -1;
        Move();
    }
    private void MoveRight()
    {
        ResetSpeed();
        lookDirection.x = 1;
        Move();
    }
    private void MoveUp()
    {
        ResetSpeed();
        lookDirection.y = 1;
        Move();
    }
    private void MoveDown()
    {
        ResetSpeed();
        lookDirection.y = -1;
        Move();
    }
}
