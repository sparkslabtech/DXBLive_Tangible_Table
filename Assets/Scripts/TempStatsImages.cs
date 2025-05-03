using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempStatsImages : MonoBehaviour
{

    public List<Sprite> tempStatImages;
    public RawImage statImage;
    public SideTangibleHandler sideTangible;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowStatRender(int p_index)
    {
        statImage.texture = ConvertSprites(tempStatImages[p_index]);
    }

    static Texture2D ConvertSprites(Sprite sprite)
    {
        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);

        tex.SetPixels(pixels);
        return tex;
    }
}
