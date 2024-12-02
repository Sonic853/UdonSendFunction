
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonStartSender : UdonCoreSender
    {
        void Start()
        {
            if (isOnce && isInteracted)
                return;
            foreach (UdonBehaviour udon in udonSender)
            {
                if (udon == null)
                    continue;
                udon.SendCustomEvent("SendFunctions");
            }
        }
    }
}
