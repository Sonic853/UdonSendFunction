
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class UdonEnableSender : UdonCoreSender
    {
        void OnEnable()
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
