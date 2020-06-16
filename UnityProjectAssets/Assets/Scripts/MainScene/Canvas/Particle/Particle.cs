using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private Transform canvas = null;

    /*
     * Unity method :   Start - Private
     * Description  :   1) The scale value of particle system is set as canvas width-height / 100.
     */
    private void Start()
    {
        // See description 1 for information.
        RectTransform rectTransformCanvas = canvas.GetComponent<RectTransform>();
        ParticleSystem particle = transform.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = particle.shape;
        shapeModule.scale = new Vector3(rectTransformCanvas.sizeDelta.x / 100, rectTransformCanvas.sizeDelta.y / 100, 1);
    }
}
