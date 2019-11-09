using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] public Paddle paddle;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector2 startingVelocity;
    [Header("Sprites")]
    [SerializeField] Sprite blueSprite;
    [SerializeField] Sprite greenSprite;
    [SerializeField] Sprite redSprite;
    [SerializeField] Sprite brownSprite;
    [SerializeField] Sprite greySprite;

    new Rigidbody2D rigidbody;
    bool hasStarted = false;
    bool resumed = true;
    bool resetting = false;
    Vector3 savedVelocity;
    GameData gameData;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        gameData = FindObjectOfType<GameData>();
        
        // Set selected sprite
        var spriteRenderer = GetComponent<SpriteRenderer>();
        switch (gameData.currentBall) {
            case GameData.BallType.blue:    spriteRenderer.sprite = blueSprite;     break;
            case GameData.BallType.green:   spriteRenderer.sprite = greenSprite;    break;
            case GameData.BallType.red:     spriteRenderer.sprite = redSprite;      break;
            case GameData.BallType.brown:   spriteRenderer.sprite = brownSprite;    break;
            case GameData.BallType.grey:    spriteRenderer.sprite = greySprite;     break;
            default:                        spriteRenderer.sprite = blueSprite;     break;
        }
    }

    void Update() {
        if (!hasStarted) {
            transform.position = paddle.transform.position + offset;

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                hasStarted = true;
                GetComponent<Rigidbody2D>().velocity = startingVelocity;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space) && !resetting) {
                resetting = true;
                Invoke(nameof(Reset), 2);
            }
        }
    }
    void Reset() {
        hasStarted = false;
        resetting = false;
    }

    void OnCollisionEnter2D(Collision2D c) {
        GetComponent<AudioSource>().Play();
    }

    public void Pause() {
        if (resumed) {
            savedVelocity = rigidbody.velocity;
            rigidbody.Sleep();
            resumed = false;
        }
    }

    public void Resume() {
        resumed = true;
        rigidbody.WakeUp();
        rigidbody.velocity = savedVelocity;
    }
}
