using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class DataManager : MonoBehaviour {

    public String fileName;

    public static DataManager instance;

    string[] tmpString;
    char[] delimeterCharacteres = {'|', ','};

    public int items;
    public int lives;

	// Use this for initialization
	void Awake () {
        instance = this;
        LoadData();
	}
	

    public void LoadData() {

        if (File.Exists(fileName)){
            //StreamReader sr = new StreamReader(Application.dataPath + fileName);
            StreamReader sr = new StreamReader(fileName);

            string stringLine;

            while ((stringLine = sr.ReadLine()) != null) {
                tmpString = stringLine.Split(delimeterCharacteres);
                items = int.Parse(tmpString[0]);
                lives = int.Parse(tmpString[1]);
            }
            sr.Close();

        }
        else {
            items = 0;
            lives = 3;
        }

    }

    public void SaveData() {

        StreamWriter sw = new StreamWriter(fileName);
        string tmpData = items + "|" + lives;
        sw.WriteLine(tmpData);
        sw.Close();

    }
}
