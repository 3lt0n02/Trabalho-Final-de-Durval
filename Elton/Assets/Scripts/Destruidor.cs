using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruidor : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somDestruido;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TocarSomDestruido();
            Destroy(gameObject);
        }
    }

    private void TocarSomDestruido()
    {
        if (audioSource != null && somDestruido != null)
        {
            audioSource.clip = somDestruido;
            audioSource.Play();
        }
    }
}
