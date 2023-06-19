using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    [SerializeField] private float smoothness = 0.1f;
    [SerializeField] private int cameraAdvance = 2;

    [SerializeField] private GameObject player;
    [SerializeField] private int facing = 1;

    [SerializeField] private float leftLimit = 0;
    [SerializeField] private float rightLimit = 0;
    [SerializeField] private float topLimit = 0;
    [SerializeField] private float bottomLimit = 0;

    private void LateUpdate() {
        Vector3 p = player.transform.position;
        facing = player.GetComponent<FireballPlayer>().GetFacing();
        p.x += facing * cameraAdvance;

        if (p.x < leftLimit) { p.x = leftLimit; }
        if (p.x > rightLimit) { p.x = rightLimit; }
        if (p.y > topLimit) { p.y = topLimit; }
        if (p.y < bottomLimit) { p.y = bottomLimit; }

        p.z = -1;

        //_vel.x += 60 * fAir * (x * vMax - _vel.x) * Time.deltaTime;
        transform.position += 60 * smoothness * (p - transform.position) * Time.deltaTime;
    }
}
