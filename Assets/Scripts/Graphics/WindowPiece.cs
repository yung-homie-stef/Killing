using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WindowPiece : MonoBehaviour
{
    [SerializeField] private Texture2D _mainTexture = null;
    [SerializeField] private Texture2D _specularMap = null;
    private MaterialPropertyBlock _materialPropertyBlock = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {
        if (_materialPropertyBlock == null)
            _materialPropertyBlock = new MaterialPropertyBlock();

        Renderer renderer = GetComponent<Renderer>();

        _materialPropertyBlock.SetTexture("_Main_Texture", _mainTexture);
        _materialPropertyBlock.SetTexture("_Specular_Map", _specularMap);

        renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
