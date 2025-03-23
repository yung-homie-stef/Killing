using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FileFolder : Program
{
    [SerializeField] ImageFile[] imgFiles;
    [SerializeField] PhotoViewer viewerPrefab;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        imgFiles = GetComponentsInChildren<ImageFile>();
    }

    public void OpenPhotoViewer(ImageFile file)
    {
        int fileIndex = System.Array.IndexOf(imgFiles, file);
        Debug.Log("The index of this photo is: " + fileIndex);

        PhotoViewer viewer = Instantiate(viewerPrefab, FindObjectOfType<Desktop>().transform);
        viewer.gameObject.SetActive(true);
        viewer.Initialize(imgFiles, fileIndex);
    }
}
