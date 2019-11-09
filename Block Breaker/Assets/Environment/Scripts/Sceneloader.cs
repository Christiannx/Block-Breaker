using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Sceneloader : MonoBehaviour {
    [SerializeField] GameObject transitionScreen;

    GameData data;
    new AudioPlayerController audio;

    void Start() {
        data = FindObjectOfType<GameData>();
        audio = FindObjectOfType<AudioPlayerController>();
    }
    
    public void LoadSceneTransition(string name) {
        try { // If scene is a level scene, check if its alreasy bought, else go to menu level scene
            int levelNumber = int.Parse(name.Split(' ')[1]);
            if (data.levelsPurchased.Contains(levelNumber)) {
                StartTransition();
            } else {
                name = "Level Menu Scene";
                audio.PlayAudio();
                StartTransition();
            }
        } catch { // scene is no level scene
            StartTransition();
        }

        void StartTransition() {
            var instance = Instantiate(transitionScreen);
            instance.GetComponentInChildren<TransitionScreen>().SetScene(name);
            DontDestroyOnLoad(instance);
        }
    }

    public void LoadScene(string name) => SceneManager.LoadScene(name);

    public void LoadNextLeven() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void Quit() => Application.Quit();
}
