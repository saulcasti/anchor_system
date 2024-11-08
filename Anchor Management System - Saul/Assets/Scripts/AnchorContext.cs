using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnchorContext : MonoBehaviour
{
    [SerializeField]
    MeshRenderer anchorMeshRenderer;

    [SerializeField]
    Collider anchorCollider;


    private void Start(){}

    internal void SetColor(Color newColor)
    {
        anchorMeshRenderer.material.color = newColor;
    }

    internal void EnableCollider(bool value)
    {
        anchorCollider.enabled = value; 
    }
}
