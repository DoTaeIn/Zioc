using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Menus
{
    public GameObject MainMenuPanel;
    public GameObject SettingPanel;
    public GameObject BeforeSavedPanel;
    public GameObject SavedPanel;
    public GameObject SavedSettingPanel;
}

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private GameObject ContentofInput;
    [SerializeField] private GameObject ContentofButton;
    [SerializeField] private GameObject InputPrefab;
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button SaveSettingButton;
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button DeleteButton;
    [SerializeField] private Text Time;

    public Transform[] allchild;
    public Menus menus = new Menus();
    private InfoManager _infoManager;
    private SaveNLoad _saveNLoad;
    public Text CountText;
    public int subjectamount = 0;

    private void Awake()
    {
        _saveNLoad = FindObjectOfType<SaveNLoad>();
        _infoManager = FindObjectOfType<InfoManager>();
    }

    private void Start()
    {
        if (Directory.Exists(_saveNLoad.SAVE_DATA_DIRECTORY))
        {
            menus.SavedPanel.SetActive(true);
            menus.MainMenuPanel.SetActive(false);
            _saveNLoad.LoadData();
        }
    }

    private void Update()
    {
        Time.text = DateTime.Now.ToString("t");
        if (menus.SettingPanel.activeSelf)
        {
            SettingButton.interactable = false;
        }
        else if(menus.BeforeSavedPanel.activeSelf)
        {
            SettingButton.interactable = false;
        }
        else if(menus.SavedSettingPanel.activeSelf)
        {
            SettingButton.interactable = false;
        }
        else
        {
            SettingButton.interactable = true;
        }

        if (_infoManager.SubjectNamesList.Count == 0)
        {
            SaveButton.interactable = false;
        }
        else
        {
            SaveButton.interactable = true;
        }
        
        CountText.text = subjectamount.ToString();
        if (subjectamount < 0)
        {
            subjectamount = 0;
        }

        if (subjectamount == _infoManager.SubjectNamesList.Count)
        {
            DeleteButton.interactable = false;
        }
        else
        {
            DeleteButton.interactable = true;
        }
        
        
    }

    #region ButtonFunc

    public void AddSubjectButton()
    {
        subjectamount++;
        GameObject.Instantiate(InputPrefab).transform.parent = ContentofInput.transform;
    }

    public void DeleteSujectButton()
    {
        if (subjectamount > 0)
        {
            subjectamount--;
            SavedButtonScript InputPrefab = FindObjectOfType<SavedButtonScript>();
            Destroy(InputPrefab.gameObject);
            
        }

    }
    
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SavePanelOpen()
    {
        menus.BeforeSavedPanel.SetActive(true);
    }
    
    public void SaveButtoninLast()
    {
        _saveNLoad.SaveInfo();
        foreach (Transform child in ContentofInput.transform)
        {
            if(child.name == ContentofInput.name)
                return;
            Destroy(child.gameObject);
        }
        menus.MainMenuPanel.SetActive(false);
        menus.BeforeSavedPanel.SetActive(false);
        menus.SavedPanel.SetActive(true);
        MakeButtons();
    }

    public void ResetThings()
    {
        subjectamount = 0;

        foreach (Transform child in ContentofButton.transform)
        {
            if(child.name == ContentofButton.name)
                return;
            Destroy(child.gameObject);
        }
        menus.SavedPanel.SetActive(false);
        menus.SavedSettingPanel.SetActive(false);
        menus.MainMenuPanel.SetActive(true);
        _infoManager.SubjectNamesList.Clear();
        _infoManager.SaveInfo.Clear();
        Directory.Delete(Application.dataPath + "/Data_File", true);
    }

    public void EditButtonFunc()
    {
        menus.MainMenuPanel.SetActive(true);
        for (int i = 0; i < _infoManager.SubjectNamesList.Count; i++)
        {
            Instantiate(InputPrefab).transform.parent = ContentofInput.transform;
            SavedButtonScript _script = FindObjectOfType<SavedButtonScript>();
            _script.MadeInputs(_infoManager.SubjectNamesList[i], _infoManager.SaveInfo[_infoManager.SubjectNamesList[i]], _infoManager.CodeNToken[i]);
        }
        
        foreach (Transform child in ContentofButton.transform)
        {
            if(child.name == ContentofButton.name)
                return;
            Destroy(child.gameObject);
        }
        menus.SavedPanel.SetActive(false);
        menus.SavedSettingPanel.SetActive(false);
        subjectamount = ContentofInput.transform.childCount;
        _infoManager.SubjectNamesList.Clear();
        _infoManager.SaveInfo.Clear();
        Directory.Delete(Application.dataPath + "/Data_File", true);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    #endregion

    void MakeButtons()
    {
        for (int i = 0; i < subjectamount; i++)
        {
            Instantiate(ButtonPrefab).transform.parent = ContentofButton.transform;
            SavedConnectButtonScript buttonScript = FindObjectOfType<SavedConnectButtonScript>();
            string Name = _infoManager.SubjectNamesList[i];
            string Code = _infoManager.SaveInfo[_infoManager.SubjectNamesList[i]];
            string token = _infoManager.CodeNToken[i];
            buttonScript.SaveCode(Name, Code, token);
        }
    }

    public void LoadedMakeButtons(List<string> Name, List<string> Code, List<string> token)
    {
        if (Directory.Exists(_saveNLoad.SAVE_DATA_DIRECTORY))
        {
            for (int i = 0; i < Name.Count; i++)
            {
                menus.MainMenuPanel.SetActive(false);
                menus.BeforeSavedPanel.SetActive(false);
                menus.SavedPanel.SetActive(true);
                Instantiate(ButtonPrefab).transform.parent = ContentofButton.transform;
                SavedConnectButtonScript buttonScript = FindObjectOfType<SavedConnectButtonScript>();
                buttonScript.SaveCode(Name[i], Code[i], token[i]);
            }
        }

    }
}
