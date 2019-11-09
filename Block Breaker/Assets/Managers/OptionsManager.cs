using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsManager : MonoBehaviour {
    GameData data;
    new AudioSource audio;
    Slider volumeSlider;
    void Start() {
        data = FindObjectOfType<GameData>();
        audio = FindObjectOfType<AudioPlayer>().GetComponent<AudioSource>();
        volumeSlider = FindObjectOfType<Slider>();
        volumeSlider.value = audio.volume;
    }

    public void Reset() {
        data.currentBall = GameData.BallType.blue;
        data.ballsPurchased = new List<GameData.BallType>(new GameData.BallType[] {GameData.BallType.blue});
        data.bricks = 0;
        data.gems = 0;
        data.levelsPurchased = new List<int>(new int[] {1});
        data.Save();
    }

    public void ChangeVolume() {
        audio.volume = volumeSlider.value;
    }
}
