using UnityEngine;

public class Gem : MonoBehaviour {
    Levelmanager manager;
    GameData data;
    bool gemGiven = false;
    void Start() {
        manager = FindObjectOfType<Levelmanager>();
        data = FindObjectOfType<GameData>();
    }
    void OnTriggerEnter2D(Collider2D c) {
        if (!gemGiven) {
            if (c.GetComponent<Ball>()) {
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("Pickup");
                manager.gems += data.activePowerUps[GameData.PowerUpType.multiplierGems]? 2 : 1;
                gemGiven = true;
            }
        }
    }

    void DestroyGem() => Destroy(gameObject);
}
