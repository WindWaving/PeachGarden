using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;

public class JsonOp
{
    private string filePath;
    private SaverMessage messager;
    public Bag bag;

    public JsonOp()
    {
        filePath = Application.streamingAssetsPath + "/saverdata.json";
    }

    //保存json数据到文件
    public void saveJson(int level)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        messager = new SaverMessage();
        messager.level = level;
        //messager.bag = bag;
        //messager.setting = setting;
        string json = JsonUtility.ToJson(messager, true);
        File.WriteAllText(filePath, json);
    }

    //读取文件内容
    public SaverMessage readJson()
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        string json = File.ReadAllText(filePath);
        messager = JsonUtility.FromJson<SaverMessage>(json);
        return messager;
    }

    //清空文件
    public void clearFile()
    {
        if (!File.Exists(filePath))
        {
            return;
        }
        File.WriteAllText(filePath, "");
    }
}

//需要保存的数据
public class SaverMessage
{
    public int level;
    public Bag bag;
    public Settings setting;
}
