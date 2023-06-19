using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource die;

    public void PlayJumpSound() {
        jump.Play();
    }

    public void PlayDashSound() {
        dash.Play();
    }

    public void PlayDieSound() {
        die.Play();
    }
}
