using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreen : MonoBehaviour {
    string sceneToLoad;

    public void SetScene(string name) => sceneToLoad = name;

    public void Load() => SceneManager.LoadScene(sceneToLoad);

    public void Finish() => Destroy(transform.parent.gameObject);
}
