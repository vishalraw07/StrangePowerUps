using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CamCircleWiper : MonoBehaviour {

    [SerializeField] private Shader circleShader;
    [SerializeField] private float circleRadius = 2;
    private Material material;

    public UnityEvent OnWipeComplete;

    private void Awake() {
        material = new Material(circleShader);
        material.SetFloat("_Aspect", GetComponent<Camera>().aspect);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        material.SetFloat("_Radius", circleRadius);
        Graphics.Blit(source, destination, material);
    }

    public void StartCircleWipe(Vector2 centre) {
        material.SetVector("_Centre", GetComponent<Camera>().WorldToViewportPoint(centre));
        GetComponent<Animator>().Play("Base.CircleWipe");
    }

    public void StartCircleUnwipe(Vector2 centre) {
        material.SetVector("_Centre", GetComponent<Camera>().WorldToViewportPoint(centre));
        GetComponent<Animator>().Play("Base.CircleUnwipe");
    }

    private void OnAnimComplete() {
        OnWipeComplete.Invoke();
    }
}
