using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDeathManager : MonoBehaviour {

    private Vector2 spawnPos;
    private SoundsManager _sounds;

    private void Awake() {
        _sounds = transform.GetChild(1).GetComponent<SoundsManager>();
    }

    private void Start() {
        spawnPos = transform.position;

        // play spawn animation ???
        //GetComponent<Animator>().Play("Base.FireballSpawn");
    }

    public void Die() {
        // 1. stop player and disable control
        GetComponent<FireballPlayer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // 2. play death animation
        GetComponent<Animator>().Play("Base.FireballUnspawn");

        // 3. play death sound
        _sounds.PlayDieSound();
    }

    private void Respawn() {
        // 1. reposition player at spawn point and enable it
        transform.position = spawnPos;
        GetComponent<FireballPlayer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
