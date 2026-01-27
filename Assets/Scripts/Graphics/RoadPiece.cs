using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[ExecuteInEditMode]
public class RoadPiece : MonoBehaviour
{
    [SerializeField] private bool _isUsingManualOffset = false;
    [Range(1, 4 )]
    [SerializeField] private float _offsetX = 0.0f;
    [Range(1, 4)]
    [SerializeField] private float _offsetY = 0.0f;

    private Material _material = null;
    private MaterialPropertyBlock _materialPropertyBlock = null;

    private void Start()
    {
        
    }


    private void OnValidate()
    {
        if (_materialPropertyBlock == null)
            _materialPropertyBlock = new MaterialPropertyBlock();

        Renderer renderer = GetComponent<Renderer>();

        _materialPropertyBlock.SetFloat("_Manual_Offset", _isUsingManualOffset ? 1.0f : 0.0f);
        _materialPropertyBlock.SetFloat("_xOffset", _offsetX);
        _materialPropertyBlock.SetFloat("_yOffset", _offsetY);

        renderer.SetPropertyBlock( _materialPropertyBlock );
    }
    
}
