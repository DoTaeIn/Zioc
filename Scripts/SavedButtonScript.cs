using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedButtonScript : MonoBehaviour
{
    public InputField ClassName;
    public InputField ClassCode;
    public InputField ClassToken;
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button DeleteButton;
    [SerializeField] private GameObject ParentPanel;
    
    private InfoManager _infoManager;
    private SaveNLoad saveNLoad;

    private void Awake()
    {
        _infoManager = FindObjectOfType<InfoManager>();
    }

    private void Start()
    {
        DeleteButton.interactable = false;
    }

    private void Update()
    {

        if (ClassName.text == "")
        {
            SaveButton.interactable = false;
            
        }
        else if (ClassCode.text == "")
        {
            SaveButton.interactable = false;
        }
        else if (_infoManager.SaveInfo.ContainsKey(ClassName.text))
        {
            SaveButton.interactable = false;
            DeleteButton.interactable = true;
        }
        else if (_infoManager.SaveInfo.ContainsValue(ClassCode.text))
        {
            SaveButton.interactable = false;
            DeleteButton.interactable = true;
        }
        else
        {
            SaveButton.interactable = true;
            bool Saved = SaveButton.interactable;
            DeleteButton.interactable = !Saved;
        }
    }

    #region ButtonFunc

    public void SaveButtonFunc()
    {
        if (ClassName.text != "")
        {
            if (ClassCode.text != "")
            {
                ClassName.interactable = false;
                ClassCode.interactable = false;
                ClassToken.interactable = false;
                DeleteButton.interactable = true;
                _infoManager.SubjectNamesList.Add(ClassName.text);
                _infoManager.SaveInfo.Add(ClassName.text, ClassCode.text);
                Debug.Log(_infoManager.SaveInfo.Keys.ToString());
                
            }
        }
    }

    public void DeleteButtonFunc()
    {
        if (_infoManager.SaveInfo.ContainsKey(ClassName.text))
        {
            ClassName.interactable = true;
            ClassCode.interactable = true;
            ClassToken.interactable = true;
            DeleteButton.interactable = false;
            SaveButton.interactable = true;
            _infoManager.SubjectNamesList.Remove(ClassName.text);
            _infoManager.SaveInfo.Remove(ClassName.text);
        }   
    }
    

    #endregion

    public void MadeInputs(string name, string code)
    {
        ClassName.text = name;
        ClassCode.text = code;
    }
    
}
