using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[ExecuteInEditMode]
public class SidewalkPieces : MonoBehaviour
{
    [Header("Curb")]
    [SerializeField] private bool _usesCurbUVs = false;
    [ShowIf("_usesCurbUVs")]
    [Range(0, 3)]
    [SerializeField] private int _curbOffsetX = 0;
    [Header("Pavement")]
    [Range(0, 2)]
    [SerializeField] private int _pavementOffsetX = 0;
    [Range(0, 3)]
    [SerializeField] private int _pavementOffsetY = 0;
    [SerializeField] private bool _usesTextureArray = false;
    [ShowIf("_usesTextureArray")]
    [Range(0,2)]
    [SerializeField] private int _textureArrayIndex = 0;

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
        _materialPropertyBlock.SetFloat("_textureArrayIndex", _textureArrayIndex);

        renderer.SetPropertyBlock( _materialPropertyBlock );
    }
    
}
