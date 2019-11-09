using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] Sprite[] brokenTextures;
    [SerializeField] int health;
    [SerializeField] public bool indestructable = false;
    [Space]
    [SerializeField] int baseScore = 15;
    [SerializeField] int scoreVariation = 5;

    int timesHit = 0;
    SpriteRenderer brokenTexturesRenderer;
    Levelmanager manager;
    GameData data;

    void Start() {
        brokenTexturesRenderer = GetComponentInChildren<SpriteRenderer>();
        manager = FindObjectOfType<Levelmanager>();
        data = FindObjectOfType<GameData>();
    }

    public void OnCollisionEnter2D(Collision2D c) {
        if (!indestructable) {
            HandleCollision();
        }
    }

    void HandleCollision() {
        timesHit++;

        if (timesHit >= health) {
            AddScore();
            Destroy(gameObject);
        } else {
            switch (timesHit) {
                case 1:
                    switch (health) {
                        case 2:
                            brokenTexturesRenderer.sprite = brokenTextures[1]; break;
                        case 3:
                            brokenTexturesRenderer.sprite = brokenTextures[0]; break;
                        default: break;
                    } 
                    break;
                case 2:
                    GetComponentInChildren<SpriteRenderer>().sprite = brokenTextures[1];
                    break;
                default: break;
            }
        }
    }

    void AddScore() {
        var random = Random.Range(-scoreVariation, scoreVariation);
        var multiplier = data.activePowerUps[GameData.PowerUpType.multiplierBricks]? 2 : 1;
        var bricks = (15 * health + random) * multiplier;
        manager.bricks += bricks;
    }
}
