
using System;
using Sonic853.Udon.ArrayPlus;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonPlayerJoinSender : UdonCoreSender
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
        /// <summary>
        /// 已触发的玩家
        /// </summary>
        [UdonSynced][NonSerialized] public string[] isInteractedPlayerName = new string[0];
        [NonSerialized] public VRCPlayerApi OnPlayerJoined_VRCPlayerApi = null;
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            OnPlayerJoined_VRCPlayerApi = player;
            OnPlayerJoined_();
            OnPlayerJoined_VRCPlayerApi = null;
        }
        public void OnPlayerJoined_()
        {
            var _playerName = OnPlayerJoined_VRCPlayerApi.displayName;
            if (isOnce
            && isInteracted)
            {
                if (onlyOnePlayerInteract || isLocalOnly)
                    return;
                if (sendPlayer)
                {
                    if (OnPlayerJoined_VRCPlayerApi != null
                    && Array.IndexOf(isInteractedPlayerName, _playerName) != -1)
                        return;
                }
                else
                    return;
            }
            for (int i = 0; i < udonSender.Length; i++)
            {
                if (udonSender[i] == null)
                    continue;
                if (sendPlayer
                && !string.IsNullOrEmpty(_playerName))
                {
                    var udonSenderValues = (object[])udonSender[i].GetProgramVariable("values");
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
                            _udonSenderValues.SetValue(OnPlayerJoined_VRCPlayerApi, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    udonSender[i].SetProgramVariable("values", udonSenderValues);
                }
                udonSender[i].SendCustomEvent("SendFunctions");
            }
            isInteracted = true;
            if (sendPlayer
            && !string.IsNullOrEmpty(_playerName)
            && Array.IndexOf(isInteractedPlayerName, _playerName) == -1)
            {
                isInteractedPlayerName = UdonArrayPlus.Add(isInteractedPlayerName, _playerName);
            }
        }
    }
}
