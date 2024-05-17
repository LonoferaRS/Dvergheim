using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeResolution: MonoBehaviour
{
    public TMP_Dropdown Dropdown_;
    public void Change()
    {
        if (Dropdown_.value == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (Dropdown_.value == 1)
        {
            Screen.SetResolution(1366, 768, true);
        }
        else if (Dropdown_.value == 2)
        {
            Screen.SetResolution(1680, 1050, true);
        }
        else if (Dropdown_.value == 3)
        {
            Screen.SetResolution(2560, 1080, true);
        }
        else if (Dropdown_.value == 4)
        {
            Screen.SetResolution(2560, 1440, true);
        }
        else if (Dropdown_.value == 5)
        {
            Screen.SetResolution(3840, 2160, true);
        }
        else if (Dropdown_.value == 6)
        {
            Screen.SetResolution(1024, 768, true);
        }

    }
}
