
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerCollisionEnterSender : UdonCorePlayerSender
    {
        public override void OnPlayerCollisionEnter(VRCPlayerApi player)
        {
            OnPlayerHit_VRCPlayerApi = player;
            OnPlayerHit();
            OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
