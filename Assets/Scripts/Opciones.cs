using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Opciones : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown idiomaDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i <resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Se ha cambiado la resolucion");
    }
    private bool active = false;
    IEnumerator SetLocale(int localeId){
        active= true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        active = false;

    }

    public void CambiarIdioma(int localeId){
        if(active == true){
            return;
        }
        StartCoroutine(SetLocale(localeId));
    }
    
}
