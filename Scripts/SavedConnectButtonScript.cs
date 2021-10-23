using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SavedConnectButtonScript : MonoBehaviour
{
    public Text SubjectName;
    public string Code;
    public string token;

    private SavedButtonScript _saved;
    private InfoManager _info;
    private void Awake()
    {
        _saved = FindObjectOfType<SavedButtonScript>();
        _info = FindObjectOfType<InfoManager>();
    }

    public void SaveCode(string Name, string _code, string token)
    {
        SubjectName.text = Name;
        Code = _code;
        this.token = token;
    }

    public void Function()
    {
        int idx = _info.SubjectNamesList.FindIndex(a => a.Contains(SubjectName.text));
        if (_info.SubjectNamesList.FindIndex(a => a.Contains(SubjectName.text)) >= 0)
        {
            if (_info.isTokenlist[idx].Contains("true"))
            {
                OpenUrl(Code, _info.CodeNToken[idx]);
            }
            else
            {
                Application.OpenURL("zoommtg://zoom.us/join?confno=" + Code);
            }
        }
        
    }

    void OpenUrl(string code, [CanBeNull, LocalizationRequired(false)] string token)
    {
        Application.OpenURL("zoommtg://zoom.us/join?confno=" + Code + "&pwd=" + token);
    }

}
