using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BateMenuManager : MonoBehaviour 
{
    private string bgAssetUrl = "Scenes/Menu/tela-inicio";
    private string titleAssetUrl = "Scenes/Menu/tela-inicioOver";
    private string btJogarAssetUrl = "Scenes/Menu/telainicioJogar";
    private string btVoltarAssetUrl = "Scenes/Menu/telainicioVoltar";

    private BateMain main;
    private GameObject background;
    private GameObject backgroundTitle;
    private GameObject btJogar;
    private GameObject btVoltar;

    public void StartScene(BateMain masterclass)
    {
        main = masterclass;
        main.Trace("Menu Manager: StartScene!");

        background = GameObject.Find("menuBg");
        bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
        backgroundTitle = GameObject.Find("menuTitle");
        titleAssetUrl = main.AddPlatformAndQualityToUrl(titleAssetUrl);
        btJogar = GameObject.Find("menuBtJogar");
        btJogarAssetUrl = main.AddPlatformAndQualityToUrl(btJogarAssetUrl);
        btVoltar = GameObject.Find("menuBtVoltar");
        btVoltarAssetUrl = main.AddPlatformAndQualityToUrl(btVoltarAssetUrl);

        background.transform.SetParent(main.uiCanvas.transform);
        backgroundTitle.transform.SetParent(main.uiCanvas.transform);
        btJogar.transform.SetParent(main.uiCanvas.transform);
        btVoltar.transform.SetParent(main.uiCanvas.transform);

        background.GetComponent<Image>().sprite = Resources.Load<Sprite>(bgAssetUrl);
        backgroundTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>(titleAssetUrl);
        btJogar.GetComponent<Image>().sprite = Resources.Load<Sprite>(btJogarAssetUrl);
        btJogar.GetComponent<Button>().onClick.AddListener(OnBtJogarClick);
        btVoltar.GetComponent<Image>().sprite = Resources.Load<Sprite>(btVoltarAssetUrl);
        btVoltar.GetComponent<Button>().onClick.AddListener(OnBtVoltarClick);
    }

    public void GetReadyToDestroy()
    {
        background.SetActive(false);
        btJogar.SetActive(false);
        btJogar.SetActive(false);
        btVoltar.SetActive(false);
        /*Destroy(background);
        Destroy(backgroundTitle);
        Destroy(btJogar);
        Destroy(btVoltar);*/
        /*background.transform.SetParent(main.menuScene.transform);
        backgroundTitle.transform.SetParent(main.menuScene.transform);
        btJogar.transform.SetParent(main.menuScene.transform);
        btVoltar.transform.SetParent(main.menuScene.transform);*/
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
