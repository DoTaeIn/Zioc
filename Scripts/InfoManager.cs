using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    private SavedButtonScript _script;
    public List<string> SubjectNamesList = new List<string>();
    public Dictionary<string, string> SaveInfo = new Dictionary<string, string>();

    private void Awake()
    {
        _script = FindObjectOfType<SavedButtonScript>();
    }

    private void Update()
    {
        
    }
}
