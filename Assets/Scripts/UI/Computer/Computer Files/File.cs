using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class File : MonoBehaviour, IPointerClickHandler
{
    private int tap = 0;
    //private FileFolder _fileFolder = null;
    enum FileType {jpg, png, txt, exe, folder};

    [Header("File Parameters")]
    public string _fileName;
    [SerializeField] private FileType _fileType;
    [SerializeField] private Image _fileIcon;
    //[SerializeField] private bool _isInFolder = true;

    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI _fileNameText;
    [SerializeField] private TextMeshProUGUI _fileTypeText;
    [SerializeField] private Image _fileIconImage;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _fileNameText.text = _fileName;
        AssignFileName(_fileType);
    }

    private void AssignFileName(FileType fileEnum)
    {
        switch (fileEnum)
        {
            case FileType.jpg:
                _fileTypeText.text = "JPG File";
                break;
            case FileType.png:
                _fileTypeText.text = "PNG File";
                break;
            case FileType.txt:
                _fileTypeText.text = "Text Document";
                break;
            case FileType.exe:
                _fileTypeText.text = "Application";
                break;
            case FileType.folder:
                _fileTypeText.text = "File folder";
                break;
        }
    }

    protected virtual void OnFileClick() { }

    public void OnPointerClick(PointerEventData eventData)
    {
        tap = eventData.clickCount;

        if (tap == 1)
        {

        }
        else if (tap == 2)
        {
            OnFileClick();
        }
    }
}
