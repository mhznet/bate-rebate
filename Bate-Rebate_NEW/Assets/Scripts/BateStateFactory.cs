﻿using UnityEngine;
using System.Collections;

using UnityEngine;
using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.State;


public class BateStateFactory : IStateFactory {

    public IState CreateStateByID(AState.EGameState newstateID)
    {
        switch( newstateID )
        {
            case AState.EGameState.MENU :
                return AFObject.Create<BateMenuState>();
            case AState.EGameState.GAME :
                return AFObject.Create<BateGameState>();
            case AState.EGameState.SELECTION :
                return AFObject.Create<BateSelectionState>();
        }


        return null;
    }
}
