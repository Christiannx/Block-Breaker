using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    void Awake() {
        if (FindObjectsOfType<AudioPlayer>().Length > 1) 
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}