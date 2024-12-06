
using UnityEngine;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] GameObject _resolutionDropDown;
    [SerializeField] TMP_Text _resolutionText;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void ClickResolutionDropDown()
    {
        _resolutionDropDown.gameObject.SetActive(!_resolutionDropDown.activeSelf);
    }
    public void ChangeTo800X600()
    {
        Screen.SetResolution(800, 600, true);
        _resolutionDropDown.gameObject.SetActive(false);
        _resolutionText.text = "800X600";
        GameManager.Instance.SetBoundaries();
    }

    public void ChangeTo1366X768()
    {
        Screen.SetResolution(1366, 768, true);
        _resolutionDropDown.gameObject.SetActive(false);
        _resolutionText.text = "1366X768";
        GameManager.Instance.SetBoundaries();
    }

    public void ChangeTo1920X1080()
    {
        Screen.SetResolution(1920, 1080, true);
        _resolutionDropDown.gameObject.SetActive(false);
        _resolutionText.text = "1920X1080";
        GameManager.Instance.SetBoundaries();
    }

    public void CHangeTo1600X1200()
    {
        Screen.SetResolution(1600, 1200, true);
        _resolutionDropDown.gameObject.SetActive(false);
        _resolutionText.text = "1600X1200";
        GameManager.Instance.SetBoundaries();
    }
}
