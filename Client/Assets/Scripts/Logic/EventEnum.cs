using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pg
{
    public enum EventEnum
    {
        NetConnected = 1,
        NetError = 2,
        //----------------------------------------------------------------------------------------------
        //下面的id是 c#传到lua  上面反过来
        //----------------------------------------------------------------------------------------------
        SceneClick = 10001,
   
    }
}