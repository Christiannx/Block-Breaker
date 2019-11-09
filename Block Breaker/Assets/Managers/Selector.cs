using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour {
    [SerializeField] Button[] selectButtons;
    
    GameData data;

    void Start() {
        data = FindObjectOfType<GameData>();
        UpdateSelection();
    }

    public void Select(string ballString) {
        GameData.BallType ball = data.StringToBall(ballString);
        data.currentBall = ball;
        UpdateSelection();
    }

    public void UpdateSelection() {
        // Selects the current ball
        int currentBallIndex = int.Parse(data.BallToString(data.currentBall)) - 1;
        selectButtons[currentBallIndex].interactable = false;
        foreach (var button in selectButtons) {
            if (button != selectButtons[currentBallIndex]) {
                button.interactable = true;
            }
        }

        for (int i = 0; i < selectButtons.Length; i++) {
            // Sets the button active/inactive whether ballsPurchased contains the ball
            selectButtons[i].gameObject.SetActive(data.ballsPurchased.Contains(data.StringToBall((i + 1).ToString())));
        }

    }
}
