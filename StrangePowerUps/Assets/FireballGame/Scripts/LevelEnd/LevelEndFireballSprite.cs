using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[ExecuteInEditMode]
public class LevelEndFireballSprite : MonoBehaviour {

    private Vector2 centre;
    private Vector2 p1;
    private Vector2 v1;
    private Vector2 vTangent;

    [Range(0, 1)]
    [SerializeField] private float t = 0;

    private void Awake() {
        centre = transform.parent.Find("Centre").transform.position;
    }

    public void Update() {
        Vector2 pPrev = transform.position;

        float r = 1 - ((t * 2 - 1) * (t * 2 - 1));

        //transform.position = p1 + t * (centre - p1)
        //transform.position = p1 + t * (centre - p1) + r * (p1 - centre);
        transform.position = p1 + t * (centre - p1) + r * (v1 + vTangent);

        Vector2 direction = (Vector2)transform.position - pPrev;
        transform.rotation = Quaternion.Euler(0, 0, -90 - Vector2.SignedAngle(direction.normalized, Vector2.right));

        float scale = Mathf.Clamp01((centre - (Vector2)transform.position).magnitude);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public void StartAnim(Vector2 pos, Vector2 vel) {
        p1 = pos;
        v1 = -vel / 8;
        vTangent = new Vector2(v1.y, -v1.x);
    }

}
