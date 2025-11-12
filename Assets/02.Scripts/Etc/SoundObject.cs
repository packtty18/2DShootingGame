using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundObject : MonoBehaviour
{
     private AudioSource audio;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        Destroy(gameObject, clip.length);
    }
}
