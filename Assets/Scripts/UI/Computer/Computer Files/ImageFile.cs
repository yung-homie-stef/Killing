using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFile : File
{
    private FileFolder _folder;
    public Sprite image;

    public override void Start()
    {
        base.Start();
        _folder = GetComponentInParent<FileFolder>();
    }

    protected override void OnFileClick()
    {
        base.OnFileClick();
        _folder.OpenPhotoViewer(this);
    }
}
