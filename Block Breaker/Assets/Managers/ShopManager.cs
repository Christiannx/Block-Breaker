using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI bricksLabel;
    [SerializeField] TextMeshProUGUI gemsLabel;

    ShopButton[] shopButtons;
    LevelButton[] levelButtons;
    PowerUpButton[] powerUpButtons;
    GameData data;
    Selector selector;

    void Start() {
        data = FindObjectOfType<GameData>();
        selector = FindObjectOfType<Selector>();
        shopButtons = FindObjectsOfType<ShopButton>();
        levelButtons = FindObjectsOfType<LevelButton>();
        powerUpButtons = FindObjectsOfType<PowerUpButton>();

        InvokeRepeating(nameof(UpdateScore), 0, 0.01f);

        foreach (var button in shopButtons) {
            if (data.ballsPurchased.Contains(button.ballType)) {
                button.GetComponent<Button>().interactable = false;
                button.GetComponentInChildren<TextMeshProUGUI>().text = "got";
            }
        }

        foreach (var button in levelButtons) {
            if (!data.levelsPurchased.Contains(button.level)) {
                button.GetComponent<Button>().interactable = false;
                button.priceLabel.SetActive(true);
            }

            /*if (button.level == 1) {
                button.GetComponent<Button>().interactable = true;
                button.priceLabel.SetActive(false);
            }*/
        }

        for (int i = 0; i < data.levelPrices.Count; i++) {
            levelButtons[i].priceLabel.GetComponentInChildren<TextMeshProUGUI>().text = data.levelPrices[levelButtons[i].level].ToString();
        }
    }

    void UpdateScore() {
        // Always add/sub 1 until label meets data value. if they are more than 100 apart, make the label 99 points away so it takes less time
        if (int.Parse(bricksLabel.text) < data.bricks) {
            if (data.bricks - int.Parse(bricksLabel.text) > 100)
                bricksLabel.text = (data.bricks - 99).ToString();
            bricksLabel.text = (int.Parse(bricksLabel.text) + 1).ToString();
        } else if (int.Parse(bricksLabel.text) > data.bricks) {
            if (int.Parse(bricksLabel.text) - data.bricks > 100)
                bricksLabel.text = (data.bricks + 99).ToString();
            bricksLabel.text = (int.Parse(bricksLabel.text) - 1).ToString();
        }

        if (int.Parse(gemsLabel.text) < data.gems) {
            if (data.gems - int.Parse(gemsLabel.text) > 100)
                gemsLabel.text = (data.gems - 99).ToString();
            gemsLabel.text = (int.Parse(gemsLabel.text) + 1).ToString();
        } else if (int.Parse(gemsLabel.text) > data.gems) {
            if (int.Parse(gemsLabel.text) - data.gems > 100)
                gemsLabel.text = (data.gems + 99).ToString();
            gemsLabel.text = (int.Parse(gemsLabel.text) - 1).ToString();
        }
    }

    public void PurchaseBall(string ballString) {
        GameData.BallType ball = data.StringToBall(ballString);
        if (data.ballPrices[ball] <= data.gems) {
            data.ballsPurchased.Add(ball);
            data.gems -= data.ballPrices[ball];
            foreach (var button in shopButtons) {
                if (button.ballType == ball) {
                    button.GetComponent<AudioSource>().Play();
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "got";
                }
            }
        }
        selector.UpdateSelection();
        data.Save();
    }

    public void SelectBall(GameData.BallType ball) {
        data.currentBall = ball;
        data.Save();
    }

    public void PurchaseLevel(int level) {
        if (data.levelPrices[level] <= data.bricks) {
            data.levelsPurchased.Add(level);
            data.bricks -= data.levelPrices[level];
            foreach (var button in levelButtons) {
                if (button.level == level) {
                    button.GetComponent<Button>().interactable = true;
                    button.priceLabel.SetActive(false);
                }
            }
        }
        data.Save();
    }

    public void PurchasePowerUp(string powerUpString) {
        var powerUp = data.StringToPowerUp(powerUpString);
        if (data.bricks >= 150) {
            data.activePowerUps[powerUp] = true;
            data.bricks -= 150;
            foreach (var button in powerUpButtons) {
                if (button.powerUpType.Equals(powerUp)){
                    button.GetComponent<AudioSource>().Play();
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "got";
                }
            }
        }
        data.Save();
    }
}
