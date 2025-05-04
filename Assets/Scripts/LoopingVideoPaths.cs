using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoopingVideoPaths : MonoBehaviour

{
    private static LoopingVideoPaths _instance;
    public static LoopingVideoPaths GetInstance() => _instance;

    [SerializeField] public string m_Engpath = "Table";
    [SerializeField] string[] m_EngfilePaths;
    [Space]
    [SerializeField] public string m_Arbpath = "Table";
    [SerializeField] string[] m_ArbfilePaths;
    [SerializeField] string[] m_fileType = { ".mp4" };
    const string m_folderNameConfig = "VIDEO_FOLDER";
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //CheckFiles();
    }

    private void OnValidate()
    {
        FindFiles();
    }

    private void CheckFiles()
    {
        Debug.Log($"Video Paths FInding Started");
        m_Engpath = ConfigManager.Instance.GetStringValue(m_folderNameConfig);
        m_Arbpath = ConfigManager.Instance.GetStringValue(m_folderNameConfig);
        DontDestroyOnLoad(this);
        FindFiles();
    }

    public string[] GetEngPaths() => m_EngfilePaths;
    public string[] GetArbPaths() => m_ArbfilePaths;

    public string[] FindImageFiles(string p_folderPath)
    {
        Debug.Log($"Path to Find Images: " + p_folderPath);
        Debug.Log(Directory.Exists(Application.streamingAssetsPath));
        //Debug.Assert(Directory.Exists(p_folderPath));

        var di = new DirectoryInfo(p_folderPath);
        var list = new List<string>();

        foreach (var fileType in m_fileType)
        {
            Debug.Log($"Finding {fileType}");
            foreach (var file in di.GetFiles($"*{fileType}", SearchOption.AllDirectories))
            {
                list.Add(file.ToString());
                //Debug.Log($"{file}");
            }
        }

        return list.ToArray();
    }

    [ContextMenu("Find Files")]
    public void FindFiles()
    {
        m_EngfilePaths = FindImageFiles($"{Application.streamingAssetsPath}/{m_Engpath}/");
        m_ArbfilePaths = FindImageFiles($"{Application.streamingAssetsPath}/{m_Arbpath}/");
    }
}