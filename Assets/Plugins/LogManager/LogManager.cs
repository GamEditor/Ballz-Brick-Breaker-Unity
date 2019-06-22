using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public GameObject eventSystemGameObject;
    public bool keepInAllScene;
    public bool SavelogInFile;

    public static LogManager Instance;

    public static CanvasGroup logPanelCanvasGroup;
    public static Image logPanelImage;
    public static Text logText;
    public static Button logButton;

    public static Button clearButton;
    public static Image clearButtonImage;
    public static Text clearButtonText;

    public static Button LockButton;
    public static Image LockButtonImage;
    public static Text LockButtonText;

    public static Slider sliderZoom;
    public static Slider sliderFade;

    private static string _Log;
    private static string log 
    {
        set
        {
            _Log = value;
            if (logText.text.Length > 10000) _Log = "<color=white>Log:</color>\n";
            logText.text = _Log;
        }
        get 
        {
            return _Log;
        }
    }

    private static bool isLock = false;
    private static bool logIsShowing = false;
    private static int fontSize = 14;
    private static float alpha = 0.2f;
    private static Color tempColor;

    private static bool isInit = false;
    private static bool saveEnable;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
            return;
        }

        logPanelCanvasGroup = transform.Find("Log Panel").GetComponent<CanvasGroup>();
        logPanelImage = transform.Find("Log Panel").GetComponent<Image>();
        logText = transform.Find("Log Panel").transform.Find("Log Text").GetComponent<Text>();
        logButton = transform.Find("Log Button").GetComponent<Button>();
        clearButton = transform.Find("Clear Button").GetComponent<Button>();
        clearButtonImage = transform.Find("Clear Button").GetComponent<Image>();
        clearButtonText = transform.Find("Clear Button").transform.Find("Text").GetComponent<Text>();
        LockButton = transform.Find("Lock Button").GetComponent<Button>();
        LockButtonImage = transform.Find("Lock Button").GetComponent<Image>();
        LockButtonText = transform.Find("Lock Button").transform.Find("Text").GetComponent<Text>();
        sliderZoom = transform.Find("Log Panel").transform.Find("Slider zoom").GetComponent<Slider>();
        sliderFade = transform.Find("Log Panel").transform.Find("Slider fade").GetComponent<Slider>();
        
        Reset();

        GameObject eventSystem = null;
        if (GameObject.Find("EventSystem") == null) 
        {
            eventSystem = Instantiate(eventSystemGameObject);
            eventSystem.name = "EventSystem";
            if (keepInAllScene)
            {
                Object.DontDestroyOnLoad(eventSystem);
            }
        }
        if (keepInAllScene)
        {
            Object.DontDestroyOnLoad(this.gameObject);
        }
        saveEnable = SavelogInFile;

        GetComponent<Canvas>().enabled = true;

        tempColor = logPanelImage.color;
        log += "<color=white>Log=>(Scene:" + Application.loadedLevelName + ")</color>\n";
        if (!isInit)
        {
            isInit = true;
            
            Application.logMessageReceived += SetError;
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        transform.SetAsLastSibling();
        Canvas myCanvas = this.GetComponent<Canvas>();
        myCanvas.enabled = false;
        myCanvas.enabled = true;
    }

    public void ShowLog()
    {
        logButton.GetComponent<Image>().color = Color.white;
        if (logIsShowing)
        {
            logPanelCanvasGroup.alpha = 0;
            logPanelCanvasGroup.blocksRaycasts = false;
            logPanelCanvasGroup.interactable = false;
            
            LockButton.interactable = false;
            LockButtonImage.enabled = false;
            LockButtonText.enabled = false;
            LockButtonText.text = "Lock";
            LockButtonText.fontSize = 14;

            isLock = false;

            clearButton.interactable = false;
            clearButtonImage.enabled = false;
            clearButtonText.enabled = false;
        }
        else
        {
            logPanelCanvasGroup.alpha = 1;
            LockButton.interactable = true;
            LockButtonImage.enabled = true;
            LockButtonText.enabled = true;

            clearButton.interactable = true;
            clearButtonImage.enabled = true;
            clearButtonText.enabled = true;
        }
        logIsShowing = !logIsShowing;
    }

    public void Lock()
    {
        isLock = !isLock;
        logPanelCanvasGroup.blocksRaycasts = isLock;
        logPanelCanvasGroup.interactable = isLock;

        sliderZoom.interactable = isLock;
        sliderFade.interactable = isLock;

        if (isLock)
        {
            LockButtonText.text = "UnLock";
            LockButtonText.fontSize = 10;
        }
        else
        {
            LockButtonText.text = "Lock";
            LockButtonText.fontSize = 14;
        }
    }

    public void Zoom() 
    {
        fontSize = (int)(14f * (1f + sliderZoom.value));
        logText.fontSize = fontSize;
    }

    public void Fade() 
    {
        alpha = sliderFade.value;
        tempColor.a = alpha;
        logPanelImage.color = tempColor ;
    }

    public void Clear() 
    {
        log = "<color=white>Log:</color>\n";
    }

    private void Reset() 
    {
        logIsShowing = false;
        isLock = false;
    }

    public static void SetError(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Warning:
                log += "<color=yellow>- " + condition  + "</color>\n";
                if (saveEnable) LogToFile("Warning->" + condition);
                if (!logIsShowing)
                    logButton.GetComponent<Image>().color = Color.yellow;
                break;
            case LogType.Error:
            case LogType.Exception:
                log += "<color=red>- " + condition + "\n" + stackTrace + "</color>\n";
                if (saveEnable) LogToFile("Error->"+condition + "\n" + stackTrace);
                if (!logIsShowing)
                    logButton.GetComponent<Image>().color = Color.red;
                break;
            case LogType.Assert:
            case LogType.Log:
            default:
                log += "<color=green>- " + condition + "</color>\n";
                if (saveEnable) LogToFile("Log->"+condition);
                if (!logIsShowing)
                    logButton.GetComponent<Image>().color = Color.grey;
                break;
        }
    }

    public static void LogToFile(string m)
    {
        string filepath = Application.persistentDataPath + "/Log.txt";
        System.IO.StreamWriter writer = null;
        try
        {
            writer = System.IO.File.AppendText(filepath);
            writer.WriteLine(m);
            writer.Flush();
        }
        catch
        {}
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }
}