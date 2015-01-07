using UnityEngine;
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
                return null;
            case AState.EGameState.GAME :
                return null;
        }


        return null;
    }
}
