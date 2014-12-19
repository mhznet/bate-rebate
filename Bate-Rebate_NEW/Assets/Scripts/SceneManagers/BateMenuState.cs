using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.State;

public class BateMenuState : AState 
{
    private string bgAssetUrl = "Scenes/Menu/tela-inicio";
    private string titleAssetUrl = "Scenes/Menu/tela-inicioOver";
    private string btJogarAssetUrl = "Scenes/Menu/telainicioJogar";
    private string btVoltarAssetUrl = "Scenes/Menu/telainicioVoltar";

    private GameObject m_menuScene;
    private GameObject background;
    private GameObject backgroundTitle;
    private GameObject btJogar;
    private GameObject btVoltar;
    private GameObject m_canvas;

    private BateMain main;

    protected override void Awake()
    {
        m_stateID = EGameState.MENU;
    }

    public override void BuildState()
    {
        main = BateMain.Instance;

        m_menuScene = Instantiate(AFAssetManager.Instance.Load<GameObject>("preFabs/Canvas")) as GameObject;

        //Completement the URL
        bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
        titleAssetUrl = main.AddPlatformAndQualityToUrl(titleAssetUrl);
        btJogarAssetUrl = main.AddPlatformAndQualityToUrl(btJogarAssetUrl);
        btVoltarAssetUrl = main.AddPlatformAndQualityToUrl(btVoltarAssetUrl);

        //Get The Scene Obj
        background = GameObject.Find("menuBg");
        backgroundTitle = GameObject.Find("menuTitle");
        btJogar = GameObject.Find("menuBtJogar");
        btVoltar = GameObject.Find("menuBtVoltar");

        background.GetComponent<Image>().sprite = Resources.Load<Sprite>(bgAssetUrl);
        backgroundTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>(titleAssetUrl);
        btJogar.GetComponent<Image>().sprite = Resources.Load<Sprite>(btJogarAssetUrl);
        btJogar.GetComponent<Button>().onClick.AddListener(OnBtJogarClick);
        btVoltar.GetComponent<Image>().sprite = Resources.Load<Sprite>(btVoltarAssetUrl);
        btVoltar.GetComponent<Button>().onClick.AddListener(OnBtVoltarClick);

        base.BuildState();
    }

    public override void AFUpdate(double deltaTime)
    {
        base.AFUpdate(deltaTime);
    }

    private void OnBtJogarClick()
    {
        main.GoToSelection();
    }
    private void OnBtVoltarClick()
    {
        main.QuitGame();
    }

}
