﻿using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Sound;

namespace BateRebate
{
    public class BateController : ASingleton<BateController>
    {
        public string somAmbiente = "Sounds/tema-bate-rebate";
        public string somBallHitWall = "Sounds/ball-hit-wall-bate-rebate";
        public string somBallHitPaddle = "Sounds/ball-hit-paddle-bate-rebate";
        public string somPlayerScores = "Sounds/score-cheer-bate-rebate";
        public string somIAScores = "Sounds/score-booing-bate-rebate";

        public int PlayerNumber { get; set; }
        public bool IsPaused { get; set; }
        public bool IsSounding { get; set; }
        public void MuteSound() 
        {
            IsSounding = false;
            AFSoundManager.Instance.Stop(somAmbiente);
        }
        public void PlaySound() 
        {
            IsSounding = true;
            AFSoundManager.Instance.Play(somAmbiente);
        }
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
