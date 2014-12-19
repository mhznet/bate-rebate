using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

namespace BateRebate
{
    public class BateController : ASingleton<BateController>
    {
        private bool menuOk     = false;
        private bool selectionOk= false;
        private bool gameOk     = false;
        public int PlayerNumber { get; set; }
        public void GoToMenu()
        {
            BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.MENU);
        }
        public void GoToSelection()
        {
            BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.SELECTION);
        }
        public void GoToGame()
        {
            BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.GAME);
        }
        public void QuitGame()
        {
            //TODO
        }
        public string AddPlatformAndQualityToUrl(string url, string platform = "IOS", string quality = "High")
        {
            char delim = '/';
            url = platform + delim + quality + delim + url;
            return url;
        }
    }
}
