using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

using System.Linq;

public  class ColorsClass
{
    public  string name;
    public  Color color;
}


public class GameManager : MonoBehaviour, IPointerClickHandler
{

   

 
    public Button SaveData;
    public Button Restart;


    private Text buttontext;
    
    public GameObject image;
    public InputField nameinput;

    
    Color Red = new Color32(255, 42, 43, 255);
    Color Orange = new Color32(255, 150, 52, 255);
    Color Yellow = new Color32(255, 228, 23, 255);
    Color Teal = new Color32(0, 205, 176, 255);
    Color Blue = new Color32(0, 129, 255, 255);
    Color Aqua = new Color32(0, 221, 255, 255);
    Color Purple = new Color32(195, 92, 255, 255);
    Color Green = new Color32(53, 165, 2, 255);



    private List<string> Colorthatchoosen = new List<string>();


    private List<ColorsClass> colors = new List<ColorsClass>();
       
      
   


    private List<ColorsClass> ShuffleColorList = new List<ColorsClass>();
    


    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject color = eventData.pointerCurrentRaycast.gameObject;

        color.SetActive(false);


        Colorthatchoosen.Add(color.name);

      //  Debug.Log(color);



    }

    // Use this for initialization
    void Start()
    {
        ColorsClass a = new ColorsClass();
        a.name = "RED";
        a.color = Red;

        ColorsClass b = new ColorsClass();
        b.name = "Orange";
        b.color = Orange;

        ColorsClass c = new ColorsClass();
        c.name = "Yellow";
        c.color = Yellow;

        ColorsClass d = new ColorsClass();
        d.name = "Teal";
        d.color = Teal;
        ColorsClass e = new ColorsClass();
        e.name = "Blue";
        e.color = Blue;
        ColorsClass f = new ColorsClass();
        f.name = "Aqua";
        f.color = Aqua;
        ColorsClass g = new ColorsClass();
        g.name = "Purple";
        g.color = Purple;
        ColorsClass h = new ColorsClass();
        h.name = "Green";
        h.color = Green;


        colors.Add(a);
        colors.Add(b);
        colors.Add(c);
        colors.Add(d);
        colors.Add(e);
        colors.Add(f);
        colors.Add(g);
        colors.Add(h);



        SaveData.gameObject.SetActive(false);
        Restart.gameObject.SetActive(false);
        nameinput.gameObject.SetActive(false);
        buttontext = SaveData.GetComponentInChildren<Text>();
        Time.timeScale = 1;


       

       


       

    
        
       
        ShuffleColorList = colors.OrderBy(x => Random.value).ToList();

        


        for (int i = 0; i < 8; i++)
        {
            GameObject ColorBox = Instantiate(image, transform.position, transform.rotation, transform);
            ColorBox.GetComponent<Image>().color = ShuffleColorList[i].color;
            ColorBox.name = ShuffleColorList[i].name;
            
        
        }




    }
    public void Update()
    {
        if (Colorthatchoosen.Count==8)
        {
            SaveData.gameObject.SetActive(true);
            nameinput.gameObject.SetActive(true);

            
        }

     

    }
    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);

    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);

            Restart.gameObject.SetActive(true);
            
        }

    }

        public void Save()
    {
        if (nameinput.text != "")
        {
            SaveToCSV(nameinput.text, Colorthatchoosen);

            SaveData.interactable = false;
            buttontext.text = "SAVING PLEASE WAIT...";

            StartCoroutine(LoseTime());
        }
        else
        {
            buttontext.text = "PLEASE TYPE THE NAME THEN PRESS SAVE";
        }


    }
    private List<string[]> rowData = new List<string[]>();

    void SaveToCSV(string name, List<string> nameofcolorList)
    {
        
        //Creating first row manually
        string[] rowDataTemp = new string[9];

        rowDataTemp[0] = name;

        for (int i = 1; i < rowDataTemp.Length; i++)
        {
            rowDataTemp[i] = nameofcolorList[i - 1];


        }


        rowData.Add(rowDataTemp);

       
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

       // string filePath =Application.dataPath  + "DATA.csv";

        //android
         string filePath = Application.persistentDataPath + "Saved_data.csv";
        if (File.Exists(filePath))
        {
            StreamWriter writer = File.AppendText(filePath);
            writer.WriteLine(sb);
            writer.Close();
            
           

        }
        else
        {
            StreamWriter outStream = System.IO.File.CreateText(filePath);


            outStream.WriteLine(sb);
            outStream.Close();
        }

    }




}

