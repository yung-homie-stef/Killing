using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[ExecuteInEditMode]
public class RoadPiece : MonoBehaviour
{
    [SerializeField] private bool _isUsingManualOffset = false;
    [SerializeField] private bool _isUsingTextureArray = false;
    [SerializeField] private Texture2D _roadLineTexture;
    [SerializeField] private Texture2D _roadAsphaltTexture;
    [Range(1, 4 )]
    [SerializeField] private int _offsetX = 0;
    [Range(1, 4)]
    [SerializeField] private int _offsetY = 0;
    [Range(0, 22)]
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

        _materialPropertyBlock.SetFloat("_Manual_Offset", _isUsingManualOffset ? 1.0f : 0.0f);
        _materialPropertyBlock.SetFloat("_Texture_Array", _isUsingTextureArray ? 1.0f : 0.0f);
        _materialPropertyBlock.SetFloat("_xOffset", _offsetX);
        _materialPropertyBlock.SetFloat("_yOffset", _offsetY);
        _materialPropertyBlock.SetFloat("_textureIndex", _textureArrayIndex);

        if (_roadLineTexture != null)
        _materialPropertyBlock.SetTexture("_roadPaintTexture", _roadLineTexture);
        if (_roadAsphaltTexture != null)
            _materialPropertyBlock.SetTexture("_roadAsphaltTexture", _roadAsphaltTexture);

        renderer.SetPropertyBlock( _materialPropertyBlock );
    }
    
}
