using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Json
    [SerializeField]
    private User user = null;
    public User currentUser { get { return user; } }
    private string SAVE_PATH;
    private readonly string SAVE_FILENAME = "/SaveFile.txt";
    #endregion

    public Vector3 mousePos { get; private set; }

    #region Pool & Managers
    public UIManager uiManager { get; private set; }
    #endregion

    public void EarnEnergyPerSecond()
    {
        foreach (Units unit in user.unitList)
        {
            user.slimes += unit.sPs * unit.amount;
        }
        uiManager.UpdateEnergyPanel();
    }

    void Awake()
    {
        #region Data Save-Load
        SAVE_PATH = Application.persistentDataPath;

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
        InvokeRepeating("SaveToJson", 1f, 60f);
        InvokeRepeating("EarnEnergyPerSecond", 0f, 1f);
        #endregion

        uiManager = gameObject.GetComponent<UIManager>();
    }

    #region SingleTon MousePos
 
    public void setMousePos(Vector3 pos)
    {
        mousePos = pos;
    }
    #endregion

    #region  Json Funcs
    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);

        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }
    public void Save()
    {
        SaveToJson();
    }
    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }

    public void Quit()
    {
        SaveToJson();
        Application.Quit();
    }
    #endregion
}
