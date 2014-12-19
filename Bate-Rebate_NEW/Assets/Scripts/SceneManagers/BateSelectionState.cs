using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

public class BateSelectionState : AState
{
    private string bgAssetUrl = "Scenes/Selection/bg";
    private string p1AssetUrl = "Scenes/Selection/p1";
    private string p2AssetUrl = "Scenes/Selection/p2";

    private BateMain main;
    private GameObject background;
    private GameObject btP1;
    private GameObject btP2;

    protected override void Awake()
    {
        m_stateID = 0;
    }

    public override void BuildState()
    {
        main = BateMain.Instance;

//         main = masterclass;
//         main.Trace("Selection Manager: StartScene!");
// 
//         background = GameObject.Find("BG");
//         bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
//         background.GetComponent<Image>().sprite = Resources.Load<Sprite>(bgAssetUrl);
//         background.transform.SetParent(main.uiCanvas.transform);
// 
//         btP1 = GameObject.Find("onePBtn");
//         p1AssetUrl = main.AddPlatformAndQualityToUrl(p1AssetUrl);
//         btP1.GetComponent<Image>().sprite = Resources.Load<Sprite>(p1AssetUrl);
//         btP1.GetComponent<Button>().onClick.AddListener(OnClickP1);
//         btP1.transform.SetParent(main.uiCanvas.transform);
//         
//         btP2 = GameObject.Find("twoPBtn");
//         p2AssetUrl = main.AddPlatformAndQualityToUrl(p2AssetUrl);
//         btP2.GetComponent<Image>().sprite = Resources.Load<Sprite>(p2AssetUrl);
//         btP2.GetComponent<Button>().onClick.AddListener(OnClickP2);
//         btP2.transform.SetParent(main.uiCanvas.transform);

        base.BuildState();
    }


    public void GetReadyToDestroy()
    {
        background.SetActive(false);
        btP1.SetActive(false);
        btP2.SetActive(false);
        /*Destroy(background);
        Destroy(btP1);
        Destroy(btP2);*/
        /*background.transform.SetParent(main.selectionScene.transform);
        btP2.transform.SetParent(main.selectionScene.transform);
        btP1.transform.SetParent(main.selectionScene.transform);*/
    }
    private void OnClickP1()
    {
        main.GoToGame(1);
    }
    private void OnClickP2()
    {
        main.GoToGame(2);
    }
}
