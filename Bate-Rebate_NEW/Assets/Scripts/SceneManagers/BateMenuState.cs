using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.State;

namespace BateRebate
{
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

        private bool classicLoad = false;
        private BateController main;

        protected override void Awake()
        {
            m_stateID = EGameState.MENU;
        }

        public override void BuildState()
        {
            main = BateController.Instance;
            
            bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
            titleAssetUrl = main.AddPlatformAndQualityToUrl(titleAssetUrl);
            btJogarAssetUrl = main.AddPlatformAndQualityToUrl(btJogarAssetUrl);
            btVoltarAssetUrl = main.AddPlatformAndQualityToUrl(btVoltarAssetUrl);
            
            /*PQ NÂO FUNCIONA? Favor avisar o motivo*/
            //m_menuScene = Instantiate(AFAssetManager.Instance.Load<GameObject>("preFabs/PreFabMenuScene")) as GameObject;
            m_menuScene = Resources.Load<GameObject>("preFabs/PreFabMenuScene");
            Instantiate(m_menuScene);

            background = GameObject.Find("menuBg");
            backgroundTitle = GameObject.Find("menuTitle");
            btJogar = GameObject.Find("menuBtJogar");
            btVoltar = GameObject.Find("menuBtVoltar");
            
            background.transform.localScale = Vector3.one;
            backgroundTitle.transform.localScale = Vector3.one;
            btJogar.transform.localScale = Vector3.one;
            btVoltar.transform.localScale = Vector3.one;

            /*Aqui esta dando erro ao usar o carregamento da AFW*/
            if (!classicLoad)
            {
                background.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(bgAssetUrl)) as Sprite;
                background.GetComponent<Image>().SetNativeSize();
                background.GetComponent<Image>().preserveAspect = true;

                backgroundTitle.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(titleAssetUrl)) as Sprite;
                backgroundTitle.GetComponent<Image>().SetNativeSize();
                backgroundTitle.GetComponent<Image>().preserveAspect = true;

                btJogar.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(btJogarAssetUrl)) as Sprite;
                btJogar.GetComponent<Image>().SetNativeSize();
                btJogar.GetComponent<Image>().preserveAspect = true;

                btVoltar.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(btVoltarAssetUrl)) as Sprite;
                btVoltar.GetComponent<Image>().SetNativeSize();
                btVoltar.GetComponent<Image>().preserveAspect = true;
            }
            else
            {
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>(bgAssetUrl);
                backgroundTitle.GetComponent<Image>().sprite = Resources.Load<Sprite>(titleAssetUrl);
                btJogar.GetComponent<Image>().sprite = Resources.Load<Sprite>(btJogarAssetUrl);
                btVoltar.GetComponent<Image>().sprite = Resources.Load<Sprite>(btVoltarAssetUrl);
            }

            btJogar.GetComponent<Button>().onClick.AddListener(OnBtJogarClick);
            btVoltar.GetComponent<Button>().onClick.AddListener(OnBtVoltarClick);

            /*Add(background);
            Add(backgroundTitle);
            Add(btJogar);
            Add(btVoltar);*/
            base.BuildState();
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
}
