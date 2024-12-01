
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class LocalInteractSender : UdonCoreSender
    {
        /// <summary>
        /// 放入玩家，isLocalOnly 必须为 false
        /// </summary>
        [Header("放入玩家")]
        [Tooltip("（UdonSendFunctionsWithVRCPlayerApi Only）")]
        public bool sendPlayer = false;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        public bool onlyPlayerName = false;
        VRCPlayerApi localPlayer;
        private void Start()
        {
            if (Networking.LocalPlayer != null)
                localPlayer = Networking.LocalPlayer;
        }
        public void Interact_()
        {
            if (isOnce
            && isInteracted)
                return;
            var _playerName = localPlayer.displayName;
            foreach (UdonBehaviour udon in udonSender)
            {
                if (udon == null)
                    continue;
                if (sendPlayer)
                {
                    var udonSenderValues = (object[])udon.GetProgramVariable("values");
                    if (udonSenderValues == null)
                        continue;
                    if (onlyPlayerName)
                    {
                        var _udonSenderValues = (string[])udonSenderValues;
                        for (int j = 0; j < udonSenderValues.Length; j++)
                        {
                            _udonSenderValues.SetValue(_playerName, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    else
                    {
                        var _udonSenderValues = (VRCPlayerApi[])udonSenderValues;
                        for (int j = 0; j < udonSenderValues.Length; j++)
                        {
                            _udonSenderValues.SetValue(localPlayer, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    udon.SetProgramVariable("values", udonSenderValues);
                }
                udon.SendCustomEvent("SendFunctions");
            }
            isInteracted = true;
        }
        public override void Interact()
        {
            if (localPlayer == null)
                localPlayer = Networking.LocalPlayer;
            Interact_();
        }
    }
}
