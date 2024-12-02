
using System;
using Sonic853.Udon.ArrayPlus;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Sonic853.Udon.SendFunction
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class UdonInteractSender : UdonCoreSender
    {
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        public bool isLocalOnly = true;
        /// <summary>
        /// 放入玩家，isLocalOnly 必须为 false
        /// </summary>
        [Header("放入玩家")]
        [Tooltip("isLocalOnly 必须为 false（UdonSendFunctionsWithVRCPlayerApi Only）")]
        public bool sendPlayer = false;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        public bool onlyPlayerName = false;
        /// <summary>
        /// 只有一位玩家触发，sendPlayer 必须为 true
        /// </summary>
        [Header("只有一位玩家触发")]
        [Tooltip("sendPlayer 必须为 true")]
        public bool onlyOnePlayerInteract = false;
        [UdonSynced] private string _playerName = "";
        /// <summary>
        /// 已触发的玩家
        /// </summary>
        [UdonSynced][NonSerialized] public string[] isInteractedPlayerName = new string[0];
        VRCPlayerApi interactedPlayer;
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
            {
                if (onlyOnePlayerInteract || isLocalOnly)
                    return;
                if (sendPlayer)
                {
                    if (!string.IsNullOrEmpty(_playerName)
                    && Array.IndexOf(isInteractedPlayerName, _playerName) != -1)
                        return;
                }
                else
                    return;
            }
            if (sendPlayer && !onlyPlayerName)
                interactedPlayer = UdonArrayPlus.PlayersFindName(UdonArrayPlus.Players(), _playerName);
            foreach (UdonBehaviour udon in udonSender)
            {
                if (udon == null)
                    continue;
                if (sendPlayer
                && !string.IsNullOrEmpty(_playerName))
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
                            _udonSenderValues.SetValue(interactedPlayer, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    udon.SetProgramVariable("values", udonSenderValues);
                }
                udon.SendCustomEvent("SendFunctions");
            }
            isInteracted = true;
            if (sendPlayer
            && !string.IsNullOrEmpty(_playerName)
            && Array.IndexOf(isInteractedPlayerName, _playerName) == -1)
            {
                isInteractedPlayerName = UdonArrayPlus.Add(isInteractedPlayerName, _playerName);
            }
            interactedPlayer = null;
            _playerName = "";
        }
        public override void Interact()
        {
            if (localPlayer == null)
                localPlayer = Networking.LocalPlayer;
            if (localPlayer != null)
                _playerName = localPlayer.displayName;
            if (isLocalOnly)
            {
                Interact_();
            }
            else if (sendPlayer)
            {
                if (isOnce
                && isInteracted)
                {
                    if (onlyOnePlayerInteract
                    || UdonArrayPlus.IndexOf(isInteractedPlayerName, _playerName) != -1)
                        return;
                }
                Networking.SetOwner(localPlayer, gameObject);
                _playerName = localPlayer.displayName;
                RequestSerialization();
                OnDeserialization();
            }
            else
            {
                if (isOnce
                && isInteracted)
                    return;
                SendCustomNetworkEvent(NetworkEventTarget.All, "Interact_");
            }
        }
        public override void OnDeserialization()
        {
            if (isLocalOnly)
                return;
            if (sendPlayer
            && !string.IsNullOrEmpty(_playerName))
                Interact_();
        }
    }
}
