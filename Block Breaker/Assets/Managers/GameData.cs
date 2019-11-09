using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class GameData : MonoBehaviour {
    public enum BallType { blue, green, red, brown, grey}
    public enum PowerUpType { health, paddle, multiplierBricks, multiplierGems }

    public BallType currentBall = BallType.blue;
    public List<BallType> ballsPurchased = new List<BallType>(new BallType[] {BallType.blue});
    public List<int> levelsPurchased = new List<int>(new int[] {1});

    public Dictionary<BallType, int> ballPrices = new Dictionary<BallType, int>();
    public Dictionary<int, int> levelPrices = new Dictionary<int, int>();
    public Dictionary<PowerUpType, bool> activePowerUps = new Dictionary<PowerUpType, bool>();

    public int bricks = 0;
    public int gems = 0;

    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Spiele\Block Breaker\blockbreaker.data";

    void Awake() {
        if (FindObjectsOfType<GameData>().Length > 1) {
            Destroy(this.gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);

            ballPrices.Add(BallType.blue, 0);
            ballPrices.Add(BallType.green, 10);
            ballPrices.Add(BallType.red, 15);
            ballPrices.Add(BallType.brown, 20);
            ballPrices.Add(BallType.grey, 25);

            levelPrices.Add(1, 0);
            levelPrices.Add(2, 2000);
            levelPrices.Add(3, 3500);
            levelPrices.Add(4, 5000);
            levelPrices.Add(5, 7000);
            levelPrices.Add(6, 10000);

            activePowerUps.Add(PowerUpType.health, false);
            activePowerUps.Add(PowerUpType.paddle, false);
            activePowerUps.Add(PowerUpType.multiplierBricks, false);
            activePowerUps.Add(PowerUpType.multiplierGems, false);

            if (!File.Exists(path)) {
                Save();
            } else
                Load();
        }
    }

    public void Save() {
        string content = "";
        void Dot() => content += ".";

        content += BallToString(currentBall);   // current ball
        Dot();
        foreach (BallType ball in ballsPurchased)   // balls purchased
            content += BallToString(ball);      // ...
        Dot();
        content += bricks.ToString();           // bricks
        Dot();
        content += gems.ToString();             // gems
        Dot();
        if (!levelsPurchased.Contains(1))       // check if level 1 is bought
            levelsPurchased.Add(1);
        foreach (int level in levelsPurchased)  // levels
            content += level.ToString();

        File.WriteAllText(path, content);
    } 

    public void Load() {
        string content = File.ReadAllText(path);
        string[] contentSplit = content.Split('.');
        
        currentBall = StringToBall(contentSplit[0]);            // current ball
        ballsPurchased = new List<BallType>();                  // purchased balls
        foreach (char c in contentSplit[1])                     //  ...
            ballsPurchased.Add(StringToBall(c.ToString()));     //  ...
        bricks = int.Parse(contentSplit[2]);                    // bricks
        gems = int.Parse(contentSplit[3]);                      // gems
        foreach (char c in contentSplit[4])                     // levels
            levelsPurchased.Add(int.Parse(c.ToString()));
        if (!levelsPurchased.Contains(1))
            levelsPurchased.Add(1);
    }

    public BallType StringToBall(string s)
    {
        switch (s)
        {
            case "1": return BallType.blue;
            case "2": return BallType.green;
            case "3": return BallType.red;
            case "4": return BallType.brown;
            case "5": return BallType.grey;
        }
        return BallType.blue;
    }

    public string BallToString(BallType ball)
    {
        switch (ball)
        {
            case BallType.blue: return "1";
            case BallType.green: return "2";
            case BallType.red: return "3";
            case BallType.brown: return "4";
            case BallType.grey: return "5";
        }
        return "1";
    }

    public PowerUpType StringToPowerUp(string powerup) {
        switch (powerup) {
            case "health": return PowerUpType.health;
            case "paddle": return PowerUpType.paddle;
            case "multiplierBricks": return PowerUpType.multiplierBricks;
            case "multiplierGems": return PowerUpType.multiplierGems;
        }
        return PowerUpType.paddle;
    }
}
// Order of saving: currentBall -> ballsPurchased -> bricks -> gems