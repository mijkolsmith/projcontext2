using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    public GameObject panel = null;

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
