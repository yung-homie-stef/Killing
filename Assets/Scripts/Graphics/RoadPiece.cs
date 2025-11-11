using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class RoadPiece : MonoBehaviour
{
    [SerializeField] private bool _isUsingManualOffset = false;
    [SerializeField] private float _offsetX = 0.0f;
    [SerializeField] private float _offsetY = 0.0f;

    private Material _material = null;


    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            Material m = mr.material;
            if (m != null)
            {
                _material = new Material(m);
                mr.material = _material;
            }
        }
    }

    private void Update()
    {
        if (_material != null)
        {
            _material.SetFloat("_Manual_Offset", _isUsingManualOffset ? 1.0f : 0.0f);
            _material.SetFloat("_xOffset", _offsetX);
            _material.SetFloat("_yOffset", _offsetY);
        }
    }
}
