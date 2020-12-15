using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigid;
    [SerializeField]
    private AudioClip hitEnemyAudioClip;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.gameObject.GetComponent<ClockWorkerController>();
        if(enemy != null)
        {
            enemy.PlaySound(hitEnemyAudioClip);
            enemy.Fix();
        }
        Destroy(gameObject);
    }
    public void Launch(Vector2 direction, float force)
    {
        myRigid.AddForce(direction * force);
        StartCoroutine(CoroutineAutoDestroy());
    }

    private IEnumerator CoroutineAutoDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
