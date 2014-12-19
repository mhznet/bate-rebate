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

        private BateController main;

        protected override void Awake()
        {
            m_stateID = EGameState.MENU;
        }

        public override void BuildState()
        {
            main = BateController.Instance;

            m_menuScene = Instantiate(AFAssetManager.Instance.Load<GameObject>("preFabs/Canvas")) as GameObject;

            bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
            titleAssetUrl = main.AddPlatformAndQualityToUrl(titleAssetUrl);
            btJogarAssetUrl = main.AddPlatformAndQualityToUrl(btJogarAssetUrl);
            btVoltarAssetUrl = main.AddPlatformAndQualityToUrl(btVoltarAssetUrl);

            background = GameObject.Find("menuBg");
            backgroundTitle = GameObject.Find("menuTitle");
            btJogar = GameObject.Find("menuBtJogar");
            btVoltar = GameObject.Find("menuBtVoltar");

            background.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(bgAssetUrl)) as Sprite;
            Add(background);
            backgroundTitle.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(titleAssetUrl)) as Sprite;
            Add(backgroundTitle);
            btJogar.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(btJogarAssetUrl)) as Sprite;
            btJogar.GetComponent<Button>().onClick.AddListener(OnBtJogarClick);
            Add(btJogar);
            btVoltar.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(btVoltarAssetUrl)) as Sprite;
            btVoltar.GetComponent<Button>().onClick.AddListener(OnBtVoltarClick);
            Add(btVoltar);

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
}
