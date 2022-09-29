using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMainMenu : MonoBehaviour
{
    public Button selectMainMenu;

    private void OnEnable()
    {
        selectMainMenu.Select();
    }
}
