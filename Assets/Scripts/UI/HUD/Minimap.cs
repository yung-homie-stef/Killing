using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private FirstPersonController _player = null;
    [SerializeField] private Image _playerToken = null;
    [SerializeField] private Transform _lowerBoundsObject = null;
    [SerializeField] private Transform _upperBoundsObject = null;


    [SerializeField] private Texture2D _layerTexture = null;
    [SerializeField] private float _stackOffset = 0.0f;
    [SerializeField] private int stackCount = 0;
    [SerializeField] private float _stackColourOffset = 0.0f;
    [SerializeField] private bool _darkAtBottom = false;

    private Vector2 _levelMin = Vector2.zero;
    private Vector2 _levelMax = Vector2.zero;
    private Vector2 _levelSize = Vector2.zero;

    private Vector2 mapSize = Vector2.zero;
    private Vector2 _playerRelativePos = Vector2.zero;
    private Vector3 _playerRelativeRot = Vector3.zero;
    private Vector2 _playerMapPos = Vector2.zero;

    private RectTransform rt = null;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        if (rt != null)
            mapSize = rt.rect.size * rt.localScale;

        if (_player == null)
            print("Player is null");

        if (_lowerBoundsObject == null)
            print("Lower Bounds is null");

        if (_upperBoundsObject == null)
            print("Upper Bounds is null");

        if (_playerToken == null)
            print("Player token is null");
    
        _levelMin = new Vector2(_lowerBoundsObject.position.x, _lowerBoundsObject.position.z);
        _levelMax = new Vector2(_upperBoundsObject.position.x, _upperBoundsObject.position.z);
        _levelSize = _levelMax - _levelMin;
        print(mapSize);
        print(_levelSize);

        SpawnStack(stackCount);
    }

    private void FixedUpdate()
    {
        _playerRelativePos.x = _player.transform.position.x - _levelMin.x;
        _playerRelativePos.y = _player.transform.position.z - _levelMin.y;
        _playerRelativeRot.z = _player.transform.localEulerAngles.y;

        _playerMapPos = (_playerRelativePos / _levelSize) * mapSize;
        _playerMapPos -= mapSize / 2;
        rt.localPosition = -_playerMapPos;
        _playerToken.rectTransform.localEulerAngles = _playerRelativeRot * -1;

        //_playerToken.rectTransform.localPosition = _playerMapPos;

        //print(_playerMapPos);
        //print(_player.transform.rotation.y);
    }

    private void SpawnStack(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject whatever = Instantiate(gameObject,transform.parent, true);
            Minimap focus_M = whatever.GetComponent<Minimap>();
            if (focus_M != null)
            {
                focus_M.stackCount = 0;
                RectTransform mrt = whatever.GetComponent<RectTransform>();

                if (mrt != null)
                {
                    mrt.localScale *= 1 + (_stackOffset * (i+1));
                }

                RawImage rw = whatever.GetComponent<RawImage>();
                if (rw != null)
                {
                    rw.texture = _layerTexture;

                    if (_darkAtBottom)
                        rw.color *= 1 - (_stackColourOffset * ((count - i) + 1));
                    else
                        rw.color *= 1 - (_stackColourOffset * (i + 1));

                    rw.color = new Color(rw.color.r, rw.color.g, rw.color.b, 1.0f);
                }
            }
        }
    }


}
