using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SlideshowHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public Image targetImage; // The RawImage where the slideshow will show

    [Header("Slideshow Settings")]
    public List<Sprite> slideshowImages = new List<Sprite>(); // List of images

    private int currentIndex = 0; // Which image is currently shown

    void Start()
    {
        if (slideshowImages.Count > 0)
        {
            UpdateImage();
        }
    }

    // Update the RawImage based on currentIndex
    void UpdateImage()
    {
        if (targetImage != null && slideshowImages.Count > 0)
        {
            targetImage.sprite = slideshowImages[currentIndex];
        }
    }

    // Go to the next image
    public void NextImage()
    {
        if (slideshowImages.Count == 0) return;

        currentIndex++;
        if (currentIndex >= slideshowImages.Count)
            currentIndex = 0; // Loop back to first

        UpdateImage();
    }

    // Go to the previous image
    public void PreviousImage()
    {
        if (slideshowImages.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = slideshowImages.Count - 1; // Loop back to last

        UpdateImage();
    }

    // Function to populate the slideshow manually
    public void PopulateImages(List<Sprite> newImages)
    {
        slideshowImages = newImages;
        currentIndex = 0;
        UpdateImage();
    }
}
