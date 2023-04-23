using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

public class SetVersionInfoLabel : MonoBehaviour
{
    public bool DisplayVersion = true;
    public bool DisplayBuildNumber;
    public bool DisplayBuildTime = true;
    public bool DisplayBuildMachineName;
    public bool DisplayGitCommitShortHash = true;

    private UIDocument _document;
    private Label _versionInfoLabel;

    private void Start()
    {
        _document = GetComponent<UIDocument>();
        _versionInfoLabel = _document.rootVisualElement.Q<VisualElement>("VersionInfo").Q<Label>();
        _versionInfoLabel.text  = ConcatInfo();
    }

    private string ConcatInfo()
    {
        string info = "";
        
        if (DisplayVersion) AddArgument(ref info, VersionInfo.Asset.version);
        if (DisplayBuildNumber) AddArgument(ref info, VersionInfo.Asset.buildNumber);
        if (DisplayBuildTime) AddArgument(ref info, VersionInfo.Asset.buildTime);
        if (DisplayBuildMachineName) AddArgument(ref info, VersionInfo.Asset.buildMachineName);
        if (DisplayGitCommitShortHash) AddArgument(ref info, VersionInfo.Asset.gitCommitShortHash);

        return info;
    }

    private void AddArgument(ref string info, string argument)
    {
        info += info.IsNullOrWhitespace() ? argument : " | " + argument;
    }
}
