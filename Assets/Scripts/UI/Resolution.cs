using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    private List<UnityEngine.Resolution> filteredResolutions;

    void Start()
    {
        filteredResolutions = new List<UnityEngine.Resolution>();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                filteredResolutions.Add(Screen.resolutions[i]);
            }
        }

        foreach (UnityEngine.Resolution resolution in filteredResolutions)
        {
            string resolutionOption = resolution.width + "x" + resolution.height + " " + resolution.refreshRate + " Hz";
            options.Add(resolutionOption);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GetCurrentResolutionIndex();
        resolutionDropdown.RefreshShownValue();
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                return i;
            }
        }
        return 0; // Возвращаем индекс первого элемента, если разрешение не найдено
    }

    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
