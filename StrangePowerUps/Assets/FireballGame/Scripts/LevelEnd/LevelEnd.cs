using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vec2UnityEvent : UnityEvent<Vector2> { }

public class LevelEnd : MonoBehaviour {

    private SpriteRenderer spriteOff;
    private SpriteRenderer spriteOn;
    private GameObject duplicateFireball;

    public Vec2UnityEvent OnLevelEnd;

    private void Awake() {
        spriteOff = transform.Find("SpriteOff").GetComponent<SpriteRenderer>();
        spriteOn = transform.Find("SpriteOn").GetComponent<SpriteRenderer>();
        duplicateFireball = transform.Find("FireballSprite").gameObject;
    }

    // initiate the level end animation
    private void OnCollisionEnter2D(Collision2D collision) {
        // 0. shine and sound
        SetShine(true);
        GetComponent<AudioSource>().Play();

        // 0.5. get player properties
        Vector2 pos = collision.collider.transform.position;
        Vector2 vel = collision.collider.GetComponent<FireballPlayer>().GetVelocity();

        // 1. disable player
        collision.collider.GetComponent<FireballPlayer>().Disable();

        // 2. spawn duplicate fireball sprite
        // set the values
        //animation .play or something
        duplicateFireball.GetComponent<LevelEndFireballSprite>().StartAnim(pos, vel);
        GetComponent<Animator>().Play("Base.FireballSprite");
    }

    private void SetShine(bool shine) {
        spriteOff.enabled = !shine;
        spriteOn.enabled = shine;
    }

    public void OnFireballAnimComplete() {
        OnLevelEnd.Invoke(transform.Find("Centre").transform.position);
    }
}