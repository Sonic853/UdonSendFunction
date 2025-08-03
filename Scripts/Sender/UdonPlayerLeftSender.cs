
using System;
using Sonic853.Udon.ArrayPlus;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerLeftSender : UdonCorePlayerSender
    {
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            OnPlayerHit_VRCPlayerApi = player;
            OnPlayerHit();
            OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
