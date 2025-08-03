
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerCollisionExitSender : UdonCorePlayerSender
    {
        public override void OnPlayerCollisionExit(VRCPlayerApi player)
        {
            OnPlayerHit_VRCPlayerApi = player;
            OnPlayerHit();
            OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
