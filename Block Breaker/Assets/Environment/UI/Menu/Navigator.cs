using UnityEngine;

public class Navigator : MonoBehaviour {
    [SerializeField] RectTransform content;
    [Space]
    [SerializeField] RectTransform shopPosition;
    [SerializeField] RectTransform levelsPosition;
    [SerializeField] RectTransform equipPosition;
    [Space]
    [SerializeField] Animator shopTitle;
    [SerializeField] Animator levelsTitle;
    [SerializeField] Animator equipTitle;
    [Space]
    [SerializeField] float speed;

    Vector2 desiredPosition;

    void Start() => GoToLevels();

    void Update() {
        var newPos = Vector3.Lerp(content.localPosition, desiredPosition, speed);
        content.localPosition = newPos;
    }

    public void GoToShop() {
        desiredPosition = shopPosition.localPosition;
        shopTitle.SetBool("selected", true);
        levelsTitle.SetBool("selected", false);
        equipTitle.SetBool("selected", false);
    }

    public void GoToLevels() {
        desiredPosition = levelsPosition.localPosition;
        shopTitle.SetBool("selected", false);
        levelsTitle.SetBool("selected", true);
        equipTitle.SetBool("selected", false);
    }

    public void GoToEquip() {
        desiredPosition = equipPosition.localPosition;
        shopTitle.SetBool("selected", false);
        levelsTitle.SetBool("selected", false);
        equipTitle.SetBool("selected", true);
    }
}  
