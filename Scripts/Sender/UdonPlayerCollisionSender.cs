
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerCollisionSender : UdonCorePlayerJoinLeftSender
    {
        public override void OnPlayerCollisionEnter(VRCPlayerApi player)
        {
            Join_OnPlayerHit_VRCPlayerApi = player;
            Join_OnPlayerHit();
            Join_OnPlayerHit_VRCPlayerApi = null;
        }
        public override void OnPlayerCollisionExit(VRCPlayerApi player)
        {
            Left_OnPlayerHit_VRCPlayerApi = player;
            Left_OnPlayerHit();
            Left_OnPlayerHit_VRCPlayerApi = null;
        }
    }
}
