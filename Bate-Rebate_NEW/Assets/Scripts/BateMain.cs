using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

public class BateMain : ASingleton<BateMain>
{
    private bool menuOk     = false;
    private bool selectionOk= false;
    private bool gameOk     = false;
    private static bool allowDebug = true;

    public int PlayerNumber { get; set; }

	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        /*uiCanvas = GameObject.Find("Canvas");*/
        //GoToMenu();
        //GoToSelection();
        //GoToGame(1);
	}
    public void GoToMenu()
    {
//         if (!menuOk)
//         {
//             menuOk = true;
//             menuScene = Resources.Load<GameObject>("preFabs/BateMenuScene");
//             Instantiate(menuScene);
//             menuClass = GameObject.Find("MenuClass");
//             menuClass.GetComponent<BateMenuManager>().StartScene(this);
//         }

        BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.MENU);
    }
    public void GoToSelection()
    {

        BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.SELECTION);

        if (menuOk)
        {
            menuOk = false;

           
            //menuClass.GetComponent<BateMenuManager>().GetReadyToDestroy();
            //menuScene.SetActive(false);
            //Debug.Log(menuClass);
        }
        if (!selectionOk)
        {
//             selectionOk = true;
//             selectionScene = Resources.Load<GameObject>("preFabs/BateSelectionScene");
//             Instantiate(selectionScene);
//             selectionClass = GameObject.Find("SelectionClass");
//             selectionClass.GetComponent<BateSelectionManager>().StartScene(this);
        }
    }
    public void GoToGame(int pNum)
    {
        BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.GAME);

        if (selectionOk)
        {
            selectionOk = false;
            //selectionClass.GetComponent<BateSelectionManager>().GetReadyToDestroy();
            //selectionScene.SetActive(false);
            //Destroy(selectionScene);
           
        }
        
        if (!gameOk)
        {
//             gameOk = true;
//             gameScene = Resources.Load<GameObject>("preFabs/BateGameScene");
//             Instantiate(gameScene);
//             gameClass = GameObject.Find("GameClass");
//             gameClass.GetComponent<BateGameManager>().StartScene(this, pNum);

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
