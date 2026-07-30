using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class WindowPiece : MonoBehaviour
{
    [SerializeField] private Texture2D _windowTexture = null;
    [SerializeField] private float _glassOpacity = 1.0f;
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

        if (_windowTexture!=null) 
        {
          _materialPropertyBlock.SetTexture("_Main_Texture", _windowTexture);
        }

        _materialPropertyBlock.SetFloat("_Glass_Opacity", _glassOpacity);

        renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
