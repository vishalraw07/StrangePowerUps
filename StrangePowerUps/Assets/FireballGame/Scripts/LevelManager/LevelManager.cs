using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //[SerializeField] private string nextLevel = ""; // => change it into a string
    [SerializeField] private int nextLevel = 0;
    [SerializeField] private Transform player;
    [SerializeField] private LevelEnd lvlEnd;
    [SerializeField] private CamCircleWiper wipe;

    private void Awake() {
        lvlEnd.OnLevelEnd.AddListener(StartCircleWipe);
        wipe.OnWipeComplete.AddListener(TransitionScene);

        Application.targetFrameRate = 60;
    }

    // in Start() => start the wipe also 
    private void Start() {
        wipe.StartCircleUnwipe(player.position);
    }

    // Below functions related to COMPLETION OF LEVEL
    public void StartCircleWipe(Vector2 centre) {
        wipe.StartCircleWipe(centre);
    }

    public void TransitionScene() {
        SceneManager.LoadScene(LevelData.GetScenePathAt(nextLevel));
    }
}
