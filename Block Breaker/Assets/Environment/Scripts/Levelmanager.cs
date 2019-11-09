using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Levelmanager : MonoBehaviour {
    Block[] blocks;
    Ball ball;
    Paddle paddle;

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
    [Space]
    [SerializeField] Button pauseButton;
    [SerializeField] TextMeshProUGUI bricksLabel;
    [SerializeField] TextMeshProUGUI gemsLabel;
    [Space]
    [SerializeField] float updateScoreSpeed;
    [SerializeField] Ball ballPrefab;

    [HideInInspector] public int bricks;
    [HideInInspector] public int gems;
    bool hasEnded = false;
    GameData data;

    void Start() {
        // Add only the destructable blocks to array
        blocks = GameObject.FindObjectsOfType<Block>();
        var destructableBlocks = new List<Block>();
        foreach (var block in blocks)
            if (!block.indestructable)
                destructableBlocks.Add(block);
        blocks = destructableBlocks.ToArray();

        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        data = FindObjectOfType<GameData>();

        // Update score every 0.1 seconds
        InvokeRepeating(nameof(UpdateScore), 0, updateScoreSpeed);
    }

    void Update() {
        // Win if all destroyable blocks are destroyed
        bool allBlocksDestroyed = true;
        foreach (var block in blocks)
            if (block != null)
                allBlocksDestroyed = false;
        
        if (allBlocksDestroyed && !hasEnded) {
            Win();
        }
    }

    void UpdateScore() {
        /*if (int.Parse(bricksLabel.text) < bricks) 
            bricksLabel.text = (int.Parse(bricksLabel.text) + 1).ToString();
        else if (int.Parse(bricksLabel.text) > bricks)
            bricksLabel.text = (int.Parse(bricksLabel.text) - 1).ToString();
            
        if (int.Parse(gemsLabel.text) < gems)
            gemsLabel.text = (int.Parse(gemsLabel.text) + 1).ToString();
        else if (int.Parse(gemsLabel.text) > gems)
            gemsLabel.text = (int.Parse(gemsLabel.text) - 1).ToString();*/
            // Always add/sub 1 until label meets data value. if they are more than 100 apart, make the label 99 points away so it takes less time
        if (int.Parse(bricksLabel.text) < bricks) {
            if (bricks - int.Parse(bricksLabel.text) > 50)
                bricksLabel.text = (bricks - 49).ToString();
            bricksLabel.text = (int.Parse(bricksLabel.text) + 1).ToString();
        } else if (int.Parse(bricksLabel.text) > bricks) {
            if (int.Parse(bricksLabel.text) - bricks > 50)
                bricksLabel.text = (bricks + 49).ToString();
            bricksLabel.text = (int.Parse(bricksLabel.text) - 1).ToString();
        }

        if (int.Parse(gemsLabel.text) < gems) {
            if (gems - int.Parse(gemsLabel.text) > 50)
                gemsLabel.text = (gems - 49).ToString();
            gemsLabel.text = (int.Parse(gemsLabel.text) + 1).ToString();
        } else if (int.Parse(gemsLabel.text) > gems) {
            if (int.Parse(gemsLabel.text) - gems > 50)
                gemsLabel.text = (gems + 49).ToString();
            gemsLabel.text = (int.Parse(gemsLabel.text) - 1).ToString();
        }
    }

    public void Pause() {
        paddle.canMove = false; // TODO make paddle move smoothly to mouse
        ball.Pause();
        CancelInvoke(nameof(ContinueBall));

        pauseScreen.GetComponent<Animator>().SetBool("Pausing", true);
    }

    public void Resume() {
        pauseScreen.GetComponent<Animator>().SetBool("Pausing", false);
        paddle.canMove = true;
        Invoke(nameof(ContinueBall), 1.3f); // Make ball move after animation has finished
    }

    void ContinueBall() => ball.Resume();

    void NewBall() {
        var ballInstance = Instantiate(ballPrefab);
        ballInstance.paddle = paddle;
        ball = ballInstance;
    }
    
    public void Lose() {
        if (data.activePowerUps[GameData.PowerUpType.health]) {
            Destroy(ball);
            Invoke(nameof(NewBall), 1);
            data.activePowerUps[GameData.PowerUpType.health] = false;
        } else {
            paddle.canMove = false;
            loseScreen.GetComponent<Animator>().SetTrigger("Lose");
            winScreen.GetComponent<AudioSource>().Play();
            FindObjectOfType<AudioPlayerController>().GetComponent<AudioSource>().Stop();
            if (data.gems < 1)
                GameObject.Find("Save Button").SetActive(false);
        }
    }

    public void Win() {
        hasEnded = true;
        Destroy(ball.gameObject);
        paddle.canMove = false;
        pauseButton.interactable = false;
        winScreen.GetComponent<Animator>().SetTrigger("Appear");
        winScreen.GetComponent<AudioSource>().Play();
        FindObjectOfType<AudioPlayerController>().GetComponent<AudioSource>().Stop();
    
        data.bricks += bricks;
        data.gems += gems;
        data.Save();
    }

    public void SaveHalf() {
        if (gems >= 1) {
            gems -= 1;

            data.bricks += bricks/2;
            data.gems += gems/2;
            data.Save();
        } else if (data.gems >= 1) {
            data.gems -= 1;

            data.bricks += bricks/2;
            data.gems += gems/2;
            data.Save();
        }
    }
}
