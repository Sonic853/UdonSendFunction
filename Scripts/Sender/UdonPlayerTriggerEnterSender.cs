
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerTriggerEnterSender : UdonCorePlayerSender
    {
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            OnPlayerHit_VRCPlayerApi = player;
            OnPlayerHit();
            OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
