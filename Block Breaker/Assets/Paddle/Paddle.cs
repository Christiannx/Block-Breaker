using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float smoothSpeed;

    public bool canMove = true;
    GameData data;

    void Start() {
        data = FindObjectOfType<GameData>();
        if (data.activePowerUps[GameData.PowerUpType.paddle]) {
            var pos = transform.localScale;
            pos.x += 0.2f;
            transform.localScale = pos;
            data.activePowerUps[GameData.PowerUpType.paddle] = false;
        }
    }

    public void Update() {
        // Paddle x follows mouse if there is no win / loose screen
        if (canMove) {
            var mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            mouseX = Mathf.Clamp(mouseX, minX, maxX); // limit movement to play scene
            var newPos = Vector3.Lerp(transform.position, new Vector3(mouseX, transform.position.y), smoothSpeed); // make paddle follow smoothly
            transform.position = newPos;
        }
    }
}