using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablleHealth : MonoBehaviour
{
    [SerializeField]
    private float healPoint = 1f;
    [SerializeField]
    private AudioClip collectedAudioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.GetComponent<RubyController>();
        if(controller != null)
        {
            controller.ChangeHealth(healPoint);
            controller.PlaySound(collectedAudioClip);
            Destroy(gameObject);
        }
    }
}
