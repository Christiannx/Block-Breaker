using UnityEngine;

public class LoseCollider : MonoBehaviour {
    [SerializeField] Levelmanager manager;
    GameData data;
    void Start() => data = FindObjectOfType<GameData>();
    void OnTriggerEnter2D(Collider2D c) {
        if (!data.activePowerUps[GameData.PowerUpType.health]) {
            GetComponent<AudioSource>().Play();
            FindObjectOfType<AudioPlayerController>().GetComponent<AudioSource>().Stop();
        }
        manager.Lose();
    }
}
