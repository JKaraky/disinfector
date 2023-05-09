using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightItem : MonoBehaviour
{
    private Color startcolor;
    private MeshRenderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<MeshRenderer>();
    }
    void OnMouseEnter()
    {
        startcolor = objectRenderer.material.color;
        objectRenderer.material.color = Color.red;
    }
    void OnMouseExit()
    {
        objectRenderer.material.color = startcolor;
    }
}
