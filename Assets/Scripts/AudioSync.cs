using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSync : MonoBehaviour
{
    public float offset = 0f;

    private float delayTimer = 0f;
    private AudioSource source;
    private bool started = false;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!started)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= offset)
            {
                source.Play();
                started = true;
            }
        }
    }
}
