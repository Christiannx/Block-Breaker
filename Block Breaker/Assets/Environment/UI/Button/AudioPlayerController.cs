using UnityEngine;

public class AudioPlayerController : MonoBehaviour {
    new AudioSource audio;
    public void PlayAudio() {
        audio = FindObjectOfType<AudioPlayer>().GetComponent<AudioSource>();
        Invoke(nameof(Play), 0.9f);
    }
    public void PauseAudio() {
        audio = FindObjectOfType<AudioPlayer>().GetComponent<AudioSource>();
        Invoke(nameof(Pause), 0.5f);
    }
    public void Pause() => audio.Pause();
    public void Play() => audio.Play();
}