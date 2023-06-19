using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTroll : MonoBehaviour {

    [SerializeField] GameObject waterPrompt1;
    [SerializeField] GameObject waterPrompt2;
    [SerializeField] GameObject waterHaha;

    private void OnTriggerEnter2D(Collider2D collision) {
        waterPrompt1.SetActive(false);
        waterPrompt2.SetActive(true);
        waterHaha.SetActive(true);
    }
}
