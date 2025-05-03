using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteListWrapper
{

    public string name;
    public List<Sprite> sprites;
}

public class TempSlideshowImages : MonoBehaviour
{
    public SlideshowHandler slideshowHandler;
    public List<SpriteListWrapper> sprites;

    public void AssignSlideShowSprites(int p_index)
    {
        slideshowHandler.PopulateImages(sprites[p_index].sprites);
    }
}