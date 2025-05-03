using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;


//public class ImageLoader : MonoBehaviour
//{
//    public string ImageRoot = $"{Application.streamingAssetsPath}\\Puck\\8 Marker";
//    public string markerfolder = "8 Marker";
//    string[] m_fileType = { ".png", ".jpg", ".jpeg" };
//    [SerializeField] GameObject m_buttonParent;
//    [Space]

//    [SerializeField] List<Texture2D> m_buttonNormalImages;
//    [SerializeField] List<Texture2D> m_buttonHighlightedImages;
//    [SerializeField] List<Texture2D> m_buttonSelectedImages;

//    [SerializeField] int m_index = 0;

//    private void Awake()
//    {
//        DontDestroyOnLoad(this);
//        ImageRoot = $"{Application.streamingAssetsPath}\\Puck\\{markerfolder}";
//        DirectoryInfo directory = new DirectoryInfo(ImageRoot);
//        int folderDir = 0;
//        foreach (var folder in directory.GetDirectories())
//        {
//            Debug.Log($"Checking {folder}");

//            switch (folderDir)
//            {
//                case 0:
//                    m_buttonHighlightedImages = LoadSprites(folder.ToString());
//                    break;
//                case 1:
//                    m_buttonNormalImages = LoadSprites(folder.ToString());
//                    break;
//                case 2:
//                    m_buttonSelectedImages = LoadSprites(folder.ToString());
//                    break;
//                default:
//                    break;
//            }
//            folderDir++;
//        }
//        SetButtonsToNormal();
//    }

//    #region Get Images

//    public List<Texture2D> LoadSprites(string p_path)
//    {
//        List<Texture2D> imgList = new List<Texture2D>();
//        foreach (var file in FindImageFiles(p_path))
//        {
//            imgList.Add(GetSprite(file));
//        }
//        return imgList;
//        //GetSprites();
//    }

//    public string[] FindImageFiles(string p_folderPath)
//    {
//        var di = new DirectoryInfo(p_folderPath);
//        var list = new List<string>();
//        foreach (var fileType in m_fileType)
//        {
//            foreach (var file in di.GetFiles($"*{fileType}", SearchOption.AllDirectories))
//            {
//                list.Add(file.ToString());
//            }
//        }

//        return list.ToArray();
//    }

//    private Texture2D GetSprite(string p_path)
//    {
//        byte[] pngBytes = File.ReadAllBytes(p_path);
//        Texture2D tex = new Texture2D(2, 2);
//        tex.LoadImage(pngBytes);
//        //Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
//        var fileNameStartIndex = (p_path.LastIndexOf('0') < -1) ? p_path.LastIndexOf('0') : p_path.LastIndexOf('\\') + 1;
//        var fileName = p_path.Substring(fileNameStartIndex);
//        tex.name = fileName;
//        return tex;
//        //yield return new WaitForSeconds(0);
//    }

//    public Texture2D[] GetSprites()
//    {
//        return m_buttonNormalImages.ToArray<Texture2D>();
//    }

//    #endregion

//    #region Public Methods
//    [ContextMenu("Reverse")]
//    public void ReverseImages()
//    {
//        //m_buttonNormalImages = m_buttonNormalImages.Reverse();
//        //m_buttonSelectedImages = m_buttonSelectedImages.ToArray();.Reverse()
//        //m_buttonHighlightedImages = m_buttonHighlightedImages.Reverse().ToArray();
//    }

//    [ContextMenu("Set Images to Normal")]
//    public void SetButtonsToNormal()
//    {
//        SetButtonsTo(ButtonState.NORMAL);
//    }

//    public Texture2D GetButtonImage(int p_index, ButtonState p_state = ButtonState.NORMAL)
//    {
//        Texture2D[] buttonImages = GetSpriteType(p_state);
//        return buttonImages[p_index];
//    }

//    public void SetButtonsTo(ButtonState p_state = ButtonState.NORMAL)
//    {
//        Texture2D[] buttonImages = GetSpriteType(p_state);

//        for (int childIndex = 0; childIndex < m_buttonParent.transform.childCount; childIndex++)
//        {
//            RawImage childImage = m_buttonParent.transform.GetChild(childIndex).GetComponent<RawImage>();
//            childImage.texture = buttonImages[childIndex];
//        }
//    }

//    public void SetButtonTo(int p_index, ButtonState p_state = ButtonState.NORMAL)
//    {
//        Texture2D[] buttonImages = GetSpriteType(p_state);
//        RawImage childImage = m_buttonParent.transform.GetChild(p_index).GetComponent<RawImage>();
//        childImage.texture = buttonImages[p_index];
//    }

//    public Texture2D[] GetSpriteType(ButtonState p_state = ButtonState.NORMAL)
//    {
//        switch (p_state)
//        {
//            case ButtonState.SELECTED:
//                return m_buttonSelectedImages.ToArray();

//            case ButtonState.HIGHLIGHTED:
//                return m_buttonHighlightedImages.ToArray();

//            default:
//                return m_buttonNormalImages.ToArray();
//        }
//    }
//    #endregion
//}