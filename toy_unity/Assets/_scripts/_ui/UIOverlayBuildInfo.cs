using TMPro;
using UnityEngine;

public class UIOverlayBuildInfo : MonoBehaviour
{
    void Start()
    {
        TMP_Text tmpText = GetComponent<TMP_Text>();
        tmpText.text = $"{VersionInfo.Asset.version} | {VersionInfo.Asset.buildTime} | {VersionInfo.Asset.buildMachineName}";
    }
}