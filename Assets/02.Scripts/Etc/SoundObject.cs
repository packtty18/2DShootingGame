using UnityEngine;

public class SoundObject : MonoBehaviour
{
     private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = _clip;
        _audio.Play();
        Destroy(gameObject, _clip.length);
    }
}
