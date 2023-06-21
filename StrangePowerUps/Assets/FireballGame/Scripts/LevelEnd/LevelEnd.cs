using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vec2UnityEvent : UnityEvent<Vector2> { }

public class LevelEnd : MonoBehaviour {

    [SerializeField] private SpriteRenderer spriteOff;
    [SerializeField] private SpriteRenderer spriteOn;
    [SerializeField] private GameObject duplicateFireball;

    public Vec2UnityEvent OnLevelEnd;

    private void Awake() {
        //spriteOff = transform.Find("SpriteOff").GetComponent<SpriteRenderer>();
        //spriteOn = transform.Find("SpriteOn").GetComponent<SpriteRenderer>();
        //duplicateFireball = transform.Find("FireballSprite").gameObject;
        // No need to find components since they are already referenced in the Inspector.
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
        OnLevelEnd.Invoke(transform.position);
    }
}