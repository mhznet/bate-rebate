using UnityEngine;
using System.Collections;

public class BateMain : MonoBehaviour 
{
    private bool menuOk     = false;
    private bool selectionOk= false;
    private bool gameOk     = false;
    private bool allowDebug = true;
    
    public GameObject uiCanvas;

    public GameObject menuScene;
    public GameObject menuClass;

    public GameObject selectionScene;
    public GameObject selectionClass;

    public GameObject gameScene;
    public GameObject gameClass;

	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        uiCanvas = GameObject.Find("Canvas");
        GoToMenu();
        //GoToSelection();
        //GoToGame(1);
	}
    public void GoToMenu()
    {
        if (!menuOk)
        {
            menuOk = true;
            menuScene = Resources.Load<GameObject>("preFabs/BateMenuScene");
            Instantiate(menuScene);
            menuClass = GameObject.Find("MenuClass");
            menuClass.GetComponent<BateMenuManager>().StartScene(this);
        }
    }
    public void GoToSelection()
    {
        if (menuOk)
        {
            menuOk = false;
            //menuClass.GetComponent<BateMenuManager>().GetReadyToDestroy();
            menuScene.SetActive(false);
            //Debug.Log(menuClass);
        }
        if (!selectionOk)
        {
            selectionOk = true;
            selectionScene = Resources.Load<GameObject>("preFabs/BateSelectionScene");
            Instantiate(selectionScene);
            selectionClass = GameObject.Find("SelectionClass");
            selectionClass.GetComponent<BateSelectionManager>().StartScene(this);
        }
    }
    public void GoToGame(int pNum)
    {
        if (selectionOk)
        {
            selectionOk = false;
            //selectionClass.GetComponent<BateSelectionManager>().GetReadyToDestroy();
            selectionScene.SetActive(false);
            //Destroy(selectionScene);
        }
        
        if (!gameOk)
        {
            gameOk = true;
            gameScene = Resources.Load<GameObject>("preFabs/BateGameScene");
            Instantiate(gameScene);
            gameClass = GameObject.Find("GameClass");
            gameClass.GetComponent<BateGameManager>().StartScene(this, pNum);

            //this.gameObject.AddComponent<BateSelectionManager>();
            //this.gameObject.GetComponent<BateSelectionManager>().StartScene(this);
            //Application.LoadLevel("bateSelection");
            //GameObject.Find("BateSelectionManager>").StartScene(this) ;
        }
        //Resources.UnloadUnusedAssets();
    }
    public string AddPlatformAndQualityToUrl(string url, string platform = "IOS", string quality = "High")
    {
        char delim = '/';
        url = platform + delim + quality + delim + url;
        Trace(url);
        return url;
    }
    public void Trace(string text)
    {
        if (allowDebug) Debug.Log(text);
    }
    public void QuitGame()
    {
        //TODO
    }
}
