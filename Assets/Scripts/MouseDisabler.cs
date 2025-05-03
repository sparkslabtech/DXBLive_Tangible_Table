using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDisabler : MonoBehaviour
{
    public bool showMouse = false;
    public KeyCode enableShortcutKey = KeyCode.M;
    void Start()
    {
        showMouse = ConfigManager.Instance.GetBool("SHOW_MOUSE");
        Cursor.visible = showMouse;
    }

    private void Update()
    {
        if (Input.GetKeyDown(enableShortcutKey))
        {
            showMouse = !showMouse;
            Cursor.visible = showMouse;
        }
    }
}
