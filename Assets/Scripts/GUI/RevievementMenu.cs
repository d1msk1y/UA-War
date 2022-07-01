using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevievementMenu : MonoBehaviour
{
    public Button button;

    public delegate void RevievementMenuHandler();

    public void SetButton(RevievementMenuHandler function)
    {
        button.onClick.AddListener(() => { function(); });
    }
}
