using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoViewer : Program
{
    [Header("Photo-Related UI")]
    [SerializeField] private Image currentPhoto;
    [SerializeField] private TextMeshProUGUI photoFileName;
    [SerializeField] private ImageFile[] photoViewerImages;

    private int photoViewerIndex = 0;

    public void Initialize(ImageFile[] imgFiles, int index)
    {
        photoViewerImages = imgFiles;
        photoViewerIndex = index;
        currentPhoto.sprite = photoViewerImages[photoViewerIndex].image;
        photoFileName.text = photoViewerImages[photoViewerIndex]._fileName;
    }

    public void CyclePhotosForward()
    {
        if (photoViewerIndex < photoViewerImages.Length - 1) // if player reaches end of image array, loop back from start
            photoViewerIndex++;
        else
            photoViewerIndex = 0;

        currentPhoto.sprite = photoViewerImages[photoViewerIndex].image;
        photoFileName.text = photoViewerImages[photoViewerIndex]._fileName;
    }

    public void CyclePhotosBackward()
    {
        if (photoViewerIndex > 0) // if player reaches start of image array, loop back from end
            photoViewerIndex--;
        else
            photoViewerIndex = photoViewerImages.Length - 1;

        currentPhoto.sprite = photoViewerImages[photoViewerIndex].image;
        photoFileName.text = photoViewerImages[photoViewerIndex]._fileName;
    }
}
