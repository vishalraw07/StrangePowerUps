using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    [SerializeField] float vJump = 12;

    private void OnTriggerEnter2D(Collider2D collision) {
        GetComponent<Animator>().SetTrigger("Jump");

        collision.GetComponent<FireballPlayer>().Jump(vJump);

        GetComponent<AudioSource>().Play();
    }
}
