
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerTriggerSender : UdonCorePlayerJoinLeftSender
    {
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            Join_OnPlayerHit_VRCPlayerApi = player;
            Join_OnPlayerHit();
            Join_OnPlayerHit_VRCPlayerApi = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            Left_OnPlayerHit_VRCPlayerApi = player;
            Left_OnPlayerHit();
            Left_OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
