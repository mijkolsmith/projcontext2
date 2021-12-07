using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    public GameObject QuestPanel = null;

    public void TogglePanel()
    {
        QuestPanel.SetActive(!QuestPanel.activeSelf);
    }
}
