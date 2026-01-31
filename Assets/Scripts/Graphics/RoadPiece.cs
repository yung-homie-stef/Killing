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
    [Range(1, 4 )]
    [SerializeField] private float _offsetX = 0.0f;
    [Range(1, 4)]
    [SerializeField] private float _offsetY = 0.0f;
    [Range(1, 22)]
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

        renderer.SetPropertyBlock( _materialPropertyBlock );
    }
    
}
