
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerTriggerExitSender : UdonCorePlayerSender
    {
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            OnPlayerHit_VRCPlayerApi = player;
            OnPlayerHit();
            OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
