using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedConnectButtonScript : MonoBehaviour
{
    public Text SubjectName;
    public string Code;

    public void SaveCode(string Name, string _code)
    {
        SubjectName.text = Name;
        Code = _code;
    }
    
    public void Function()
    {
        Application.OpenURL("https://us04web.zoom.us/j/" + Code);
    }
}
