using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine.UI;
using SimpleJSON;
using System.Timers;


public class Conversations : MonoBehaviour
{
    public UIManagerScript uiScript;
    Rect _Reset, _CustomerChat, _Opt1, _Opt2, _Opt3;
    bool updatecount = false;
    bool initialLoad;
    bool gameOver;
    string _FileLocation, _FileName;
    // public GameObject _Player;
    UserData myData;
    string _PlayerName;
    string _data;
    string[] game_message1_, game_message2_, game_message3_; //decalare the string
    string[] _CustomerMessage;
    int _ClickIndex= 0;
    int _CustClickIndex = 0;
    const int OPTXPOS = 1200;
    const int OPTYPOS = 400;
    int optSize = 550;
    int offset = 208;
    Vector3 VPosition;
    int counting;
    int counter = 0;
    float walk_up_timer = 75.0f;
    const float CHATTIMER = 75.0f;

    float chat_delay_timer = CHATTIMER;
    float new_time = 0.0f;
    float delta_time = 0.0f;
    float last_time = 0.0f;
    float current_time = 0.0f;

    bool goodOpt, okayOpt, badOpt;

    public bool getGoodOpt() { return goodOpt; }
    public void setGoodOpt(bool gO) { goodOpt = gO; }
    public bool getOkayOpt() { return okayOpt; }
    public void setOkayOpt(bool oO) { okayOpt = oO; }
    public bool getBadOpt() { return badOpt; }
    public void setBadOpt(bool bO) { badOpt = bO; }

    public int getCounter() { return counter; }
    public bool getUpdateCount() { return updatecount; }

    public Image visualRageBar;

    void Start()
    {
        _CustomerChat = new Rect(Screen.width / 2 - offset, 0, 400, 100);
        _Opt1 = new Rect(Screen.width - optSize, OPTYPOS, optSize, 50);
        _Opt2 = new Rect(Screen.width - optSize, OPTYPOS + 80, optSize, 50);
        _Opt3 = new Rect(Screen.width - optSize, OPTYPOS + 160, optSize, 50);
        _Reset = new Rect(Screen.width / 2 - offset, Screen.height - 40, 60, 30);

        initialLoad = true;
        gameOver = false;


        _FileLocation = Application.dataPath;
        _FileName = "SaveData.xml";
        //
        
        _PlayerName = "Ball";

        myData = new UserData();
        game_message1_ = new string[14]; // setting the string size to 14
        game_message2_ = new string[14];
        game_message3_ = new string[14]; 
        _CustomerMessage = new string[14];


        uiScript.UpdateScores();
    }
    void WalkUp()
    {
        new_time += 0.33f;
        delta_time = (new_time - last_time);
        current_time += delta_time;

        walk_up_timer -= delta_time;
        if (walk_up_timer <= 0.0f)
        {
            walk_up_timer = 0.0f;
        }
        last_time = new_time;
    }

    void ChatDelay()
    {
        if (_ClickIndex < 7)
        {
            new_time += 0.33f;
            delta_time = (new_time - last_time);
            current_time += delta_time;

            chat_delay_timer -= delta_time;

            last_time = new_time;
        }

    }
    void CustomerChat()
    {
        GUI.Box(_CustomerChat, _CustomerMessage[_CustClickIndex]);


        for (int i = 0; i < 14; i++)
        {

            _CustomerMessage[i] = myData._iUser.customerMessage_[i]; // initial message
        }
        ChatDelay();
    }
    void UserChat()
    {
 
        //***************************************************
        //The Buttons for the customer service game
        //***************************************************
        for (int i = 0; i < 14; i++)
        {
            game_message1_[i] = myData._iUser.message1_[i];
            game_message2_[i] = myData._iUser.message2_[i];
            game_message3_[i] = myData._iUser.message3_[i];
        }


        if (GUI.Button(_Opt1, game_message1_[_ClickIndex]))
        {
            setGoodOpt(true);
            //int engagecounter = counter + 1;
            //string optionID = engagecounter + "";
            JSONNode vals = JSON.Parse("{\"optionID\" : 1 }");

            Debug.Log(vals);

            StartCoroutine(EngAGe.E.assess("newOptionSelected", vals, uiScript.ActionAssessed));

            if (_ClickIndex == 0)
            {
                updatecount = true;
                _ClickIndex = 1;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 1)
            {
                updatecount = true;
                _ClickIndex = 3;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 2)
            {
                updatecount = true;
                _ClickIndex = 3;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex == 3)
            {
                updatecount = true;
                _ClickIndex = 7;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 4)
            {
                updatecount = true;
                _ClickIndex = 9;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 5)
            {
                updatecount = true;
                _ClickIndex = 8;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex == 6)
            {
                updatecount = true;
                _ClickIndex = 11;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex >= 7)
            {
                if (counter >= 2)
                {
                    Debug.Log("------------GAME OVER, GOOD JOB!!!-----------------");
                    gameOver = true;
                }
                if (counter < 2 && counter >= 0)
                {
                    Debug.Log("------------GAME OVER, OKAY JOB!!!-----------------");
                    gameOver = true;
                }

                _CustClickIndex = _ClickIndex;
            }
            if (counter < -1)
            {
                Debug.Log("------------GAME OVER, TERRIBLE JOB!!!-----------------");
                gameOver = true;
            }

            if (_ClickIndex < 7)
                counter += 1;

            chat_delay_timer = CHATTIMER;

        }

        if (GUI.Button(_Opt2, game_message2_[_ClickIndex]))
        {
            setOkayOpt(true);
            //JSONNode vals = JSON.Parse("{\"score\" : \"" + counter + "\" }");
            JSONNode vals = JSON.Parse("{\"optionID\" : 2 }");

            StartCoroutine(EngAGe.E.assess("newOptionSelected", vals, uiScript.ActionAssessed));

            if (_ClickIndex == 0)
            {
                updatecount = true;
                _ClickIndex = 1;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 1)
            {
                updatecount = true;
                _ClickIndex = 3;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 2)
            {
                updatecount = true;
                _ClickIndex = 6;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex == 3)
            {
                updatecount = true;
                _ClickIndex = 7;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 4)
            {
                updatecount = true;
                _ClickIndex = 10;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 5)
            {
                updatecount = true;
                _ClickIndex = 8;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex == 6)
            {
                updatecount = true;
                _ClickIndex = 12;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex >= 7)
            {
                if (counter >= 2)
                {
                    Debug.Log("------------GAME OVER, GOOD JOB!!!-----------------");
                    gameOver = true;
                }
                if (counter < 2 && counter >= 0)
                {
                    Debug.Log("------------GAME OVER, OKAY JOB!!!-----------------");
                    gameOver = true;
                }

                _CustClickIndex = _ClickIndex;
            }
            if (counter < -1)
            {
                Debug.Log("------------GAME OVER, TERRIBLE JOB!!!-----------------");
                gameOver = true;
            }

            chat_delay_timer = CHATTIMER;
        }
        if (GUI.Button(_Opt3, game_message3_[_ClickIndex]))
        {
            setBadOpt(true);
            //JSONNode vals = JSON.Parse("{\"score\" : \"" + counter + "\" }");
            JSONNode vals = JSON.Parse("{\"optionID\" : 3 }");

            StartCoroutine(EngAGe.E.assess("newOptionSelected", vals, uiScript.ActionAssessed));

            if (_ClickIndex == 0)
            {
                updatecount = true;
                _ClickIndex = 2;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 1)
            {
                updatecount = true;
                _ClickIndex = 4;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 2)
            {
                Debug.Log("------------GAME OVER, TERRIBLE JOB!!!-----------------");
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex == 3)
            {
                updatecount = true;
                _ClickIndex = 8;
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 4)
            {
                Debug.Log("------------GAME OVER, TERRIBLE JOB!!!-----------------");
                _CustClickIndex = _ClickIndex;
            }

            else if (_ClickIndex == 5)
            {
                updatecount = true;
                _ClickIndex = 9;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex == 6)
            {
                updatecount = true;
                _ClickIndex = 13;
                _CustClickIndex = _ClickIndex;
            }
            else if (_ClickIndex >= 7)
            {
                if (counter >= 2)
                {
                    Debug.Log("------------GAME OVER, GOOD JOB!!!-----------------");
                    gameOver = true;
                }
                if (counter < 2 && counter >= 0)
                {
                    Debug.Log("------------GAME OVER, OKAY JOB!!!-----------------");
                    gameOver = true;
                }

                _CustClickIndex = _ClickIndex;
            }
            if (counter < -1)
            {
                Debug.Log("------------GAME OVER, TERRIBLE JOB!!!-----------------");
                gameOver = true;
            }

            if (_ClickIndex < 7)
                counter -= 1;

            chat_delay_timer = CHATTIMER;

        }
    }
    void LoadFile()
    {
        // Load our UserData into myData 
        LoadXML();

        if (_data.ToString() != "")
        {
            // notice how I use a reference to type (UserData) here, you need this 
            // so that the returned object is converted into the correct type 
            myData = (UserData)DeserializeObject(_data);
            // set the players position to the data we loaded 
            VPosition = new Vector3(myData._iUser.x, myData._iUser.y, myData._iUser.z);
                 
            counting = myData._iUser.count;
            Debug.Log(counting.ToString());
            Debug.Log(myData._iUser.name);
            //player.score = counting;
        }
    }

    void Update()
    {
        if (counter >= 3)
            visualRageBar.color = new Color32(0, 255, 0, 255);

        if (counter == 2)
            visualRageBar.color = new Color32(255 / 2, 255, 0, 255);

        if (counter == 1)
            visualRageBar.color = new Color32(255, 255, 0, 255);

        if (counter == 0)
            visualRageBar.color = new Color32(255, 255 / 2, 0, 255);

        if (counter < 0)
            visualRageBar.color = new Color32(255, 0, 0, 255);
        if (Input.GetKey("escape"))
            Application.LoadLevel("MainMenu");
    }

    void OnGUI()
    {
        updatecount = false;

        if (initialLoad == true)
        {
          
            LoadFile();
            initialLoad = false;
        }
        WalkUp();
        if (walk_up_timer <= 0.1f)
        {
            CustomerChat();

            if(chat_delay_timer <= 0.1f)
            UserChat();
        }

        if (_ClickIndex >= 7 || gameOver)
        {
            if (GUI.Button(_Reset, "Reset"))
            {
                _ClickIndex = 0;
                _CustClickIndex = _ClickIndex;
                counter = 0;
                walk_up_timer = 50.0f;
                chat_delay_timer = 100.0f;
                gameOver = false;
            }
        }

      
    }

    /* The following metods came from the referenced URL */
    string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    // Here we serialize our UserData object of myData 
    string SerializeObject(object pObject)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(typeof(UserData));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xmlTextWriter.Formatting = Formatting.Indented;
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    // Here we deserialize it back into its original form 
    object DeserializeObject(string pXmlizedString)
    {
        XmlSerializer xs = new XmlSerializer(typeof(UserData));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    // Finally our save and load methods for the file itself 
    void CreateXML()
    {
        StreamWriter writer;
        FileInfo t = new FileInfo(_FileLocation + "\\" + _FileName);
        if (!t.Exists)
        {
            writer = t.CreateText();
        }
        else
        {
            t.Delete();
            writer = t.CreateText();
        }
        writer.Write(_data);
        writer.Close();
        Debug.Log("File written.");
    }

    void LoadXML()
    {
        StreamReader r = File.OpenText(_FileLocation + "\\" + _FileName);
        string _info = r.ReadToEnd();
        r.Close();
        _data = _info;
        Debug.Log("File Read");
    }
}

// UserData is our custom class that holds our defined objects we want to store in XML format 
public class UserData
{
    // We have to define a default instance of the structure 
    public DemoData _iUser;
    // Default constructor doesn't really do anything at the moment 
    public UserData() { _iUser.message1_ = new string[14]; _iUser.message2_ = new string[14]; _iUser.message3_ = new string[14]; _iUser.customerMessage_ = new string[14]; }

    // Anything we want to store in the XML file, we define it here 
    public struct DemoData
    {
        public float x;
        public float y;
        public float z;
        public string name;
        public int count;

        public string[] message1_;
        public string[] message2_;
        public string[] message3_;

        public string[] customerMessage_;

        public string customerMessageOne;
        public string customerMessageTwo;
        public string customerMessageThree;
        public string customerMessageFour;
        public string customerMessageFive;
        public string customerMessageSix;
        public string customerMessageSeven;
    }
}