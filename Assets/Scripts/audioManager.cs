using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip Bgmusic;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = Bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
