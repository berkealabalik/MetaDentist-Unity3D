using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class WriteResult : MonoBehaviour
{

    string myFilePath, fileName;
    List<string> resultsArr = new List<string>();
    
    private void StartWrite()
    {
        resultsArr.Add("Tarih : " + System.DateTime.Now + "\n");
        resultsArr.Add("Isim : " +  StartMenu.username + "\n");
        resultsArr.Add("Ba�ar� Y�zdesi : %" + ((100 * TouchDelete.DeletedDecayNumber) / 300 ).ToString() + "\n");
        resultsArr.Add("Mineye Girme Sayisi : " + TouchDelete.MineEntered.ToString() + "\n");
        resultsArr.Add("Toplam ��r�k Say�s� : 300 " +  "\n");
        resultsArr.Add("Temizlenen ��r�k Say�s� : " + TouchDelete.DeletedDecayNumber.ToString() + "\n");
        resultsArr.Add("Kalan ��r�k Say�s� : " + (300 - TouchDelete.DeletedDecayNumber).ToString() + "\n");
        resultsArr.Add("Freze Ucu De�i�imi Sayisi : " + ChangeCollider.howManyTimesChange.ToString() + "\n");
        resultsArr.Add("Toplam Ge�irilen S�re : " + Time.timeSinceLevelLoad.ToString() + "(Saniye)" + "\n");
        fileName = "result.txt";
        myFilePath = Application.dataPath + "/" + fileName;
    }
    public void createNewResultFile()
    {
        StartWrite();
        File.WriteAllLines(myFilePath, resultsArr);
    }

}

