using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[ExecuteInEditMode]
public class SidewalkPieces : MonoBehaviour
{
    [SerializeField] private bool _usesCurbUVs = false;
    [ShowIf("_usesCurbUVs")]
    [Range(0, 3)]
    [SerializeField] private float _curbOffsetX = 0.0f;
    [Range(0, 2)]
    [SerializeField] private float _pavementOffsetX = 0.0f;
    [Range(0, 3)]
    [SerializeField] private float _pavementOffsetY = 0.0f;

    private MaterialPropertyBlock _materialPropertyBlock = null;

    private void Start()
    {
         
    }


    private void OnValidate()
    {
        if (_materialPropertyBlock == null)
            _materialPropertyBlock = new MaterialPropertyBlock();

        Renderer renderer = GetComponent<Renderer>();

        _materialPropertyBlock.SetFloat("_xOffset", _pavementOffsetX);
        _materialPropertyBlock.SetFloat("_yOffset", _pavementOffsetY);
        _materialPropertyBlock.SetFloat("_xCurbOffset", _curbOffsetX);

        renderer.SetPropertyBlock( _materialPropertyBlock );
    }
    
}
