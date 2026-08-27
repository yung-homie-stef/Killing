using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pavement : MonoBehaviour
{
    [Header("Shading")]
    [SerializeField] private bool _isEndPiece = false;
    [ShowIf("_isEndPiece")][SerializeField] private bool _isShadedAtBottom = false;
    [Header("Decal")]
    [SerializeField] private bool _isUsingTextureArray = false;
    [ShowIf("_isUsingTextureArray")][Range(0, 9)][SerializeField] private int _textureArrayIndex = 0;
    [ShowIf("_isUsingTextureArray")][Range(-0.5f, 0.5f)][SerializeField] private float _decalOffsetX = 0.0f;
    [ShowIf("_isUsingTextureArray")][Range(-0.5f, 0.5f)][SerializeField] private float _decalOffsetY = 0.0f;
    [ShowIf("_isUsingTextureArray")][Range(0.0f, 360.0f)][SerializeField] private float _decalRotation = 0.0f;

    private MaterialPropertyBlock _materialPropertyBlock = null;

    private void OnValidate()
    {
        if (_materialPropertyBlock == null)
            _materialPropertyBlock = new MaterialPropertyBlock();

        Renderer renderer = GetComponent<Renderer>();

        _materialPropertyBlock.SetFloat("_isEndPiece", _isEndPiece ? 1.0f : 0.0f);
        _materialPropertyBlock.SetFloat("_isShadedAtBottom", _isShadedAtBottom ? 1.0f : 0.0f);
        _materialPropertyBlock.SetFloat("_textureArrayIndex", _textureArrayIndex);
        _materialPropertyBlock.SetFloat("_decalOffsetX", _decalOffsetX);
        _materialPropertyBlock.SetFloat("_decalOffsetY", _decalOffsetY);
        _materialPropertyBlock.SetFloat("_decalRotation", _decalRotation * (Mathf.PI / 180.0f));


        renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
