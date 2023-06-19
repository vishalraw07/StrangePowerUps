using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    private static BGM _instance;

    [SerializeField] private AudioSource click_sound;

    private void Awake() {

        if (_instance == null) {
            DontDestroyOnLoad(gameObject);
            _instance = this;

        } else {
            Destroy(gameObject);
        }
    }

    public static BGM GetInstance() {
        return _instance;
    }

    public void ClickSound() {
        click_sound.Play();
    }

}
