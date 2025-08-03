
using System;
using Sonic853.Udon.ArrayPlus;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonCorePlayerJoinLeftSender : UdonSharpBehaviour
    {
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        public bool Join_isOnce = false;
        /// <summary>
        /// 需要调用的 Udon Sender
        /// </summary>
        [Header("需要调用的 Udon Sender")]
        public UdonBehaviour[] Join_udonSender;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool Join_isInteracted = false;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        public bool Join_isLocalOnly = true;
        /// <summary>
        /// 放入玩家，isLocalOnly 必须为 false
        /// </summary>
        [Header("放入玩家")]
        [Tooltip("isLocalOnly 必须为 false（UdonSendFunctionsWithVRCPlayerApi Only）")]
        public bool Join_sendPlayer = false;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        public bool Join_onlyPlayerName = false;
        /// <summary>
        /// 只有一位玩家触发，sendPlayer 必须为 true
        /// </summary>
        [Header("只有一位玩家触发")]
        [Tooltip("sendPlayer 必须为 true")]
        public bool Join_onlyOnePlayerInteract = false;
        /// <summary>
        /// 已触发的玩家
        /// </summary>
        [UdonSynced][NonSerialized] public string[] Join_isInteractedPlayerName = new string[0];
        public VRCPlayerApi Join_OnPlayerHit_VRCPlayerApi;
        public virtual void Join_OnPlayerHit()
        {
            var player = Join_OnPlayerHit_VRCPlayerApi;
            var _playerName = player.displayName;
            if (Join_isOnce
            && Join_isInteracted)
            {
                if (Join_onlyOnePlayerInteract || Join_isLocalOnly)
                    return;
                if (Join_sendPlayer)
                {
                    if (player != null
                    && Array.IndexOf(Join_isInteractedPlayerName, _playerName) != -1)
                        return;
                }
                else
                    return;
            }
            for (int i = 0; i < Join_udonSender.Length; i++)
            {
                if (Join_udonSender[i] == null)
                    continue;
                if (Join_sendPlayer
                && !string.IsNullOrEmpty(_playerName))
                {
                    var udonSenderValues = (object[])Join_udonSender[i].GetProgramVariable("values");
                    if (udonSenderValues == null)
                        continue;
                    if (Join_onlyPlayerName)
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
                            _udonSenderValues.SetValue(player, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    Join_udonSender[i].SetProgramVariable("values", udonSenderValues);
                }
                Join_udonSender[i].SendCustomEvent("SendFunctions");
            }
            Join_isInteracted = true;
            if (Join_sendPlayer
            && !string.IsNullOrEmpty(_playerName)
            && Array.IndexOf(Join_isInteractedPlayerName, _playerName) == -1)
            {
                Join_isInteractedPlayerName = UdonArrayPlus.Add(Join_isInteractedPlayerName, _playerName);
            }
        }
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        public bool Left_isOnce = false;
        /// <summary>
        /// 需要调用的 Udon Sender
        /// </summary>
        [Header("需要调用的 Udon Sender")]
        public UdonBehaviour[] Left_udonSender;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool Left_isInteracted = false;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        public bool Left_isLocalOnly = true;
        /// <summary>
        /// 放入玩家，isLocalOnly 必须为 false
        /// </summary>
        [Header("放入玩家")]
        [Tooltip("isLocalOnly 必须为 false（UdonSendFunctionsWithVRCPlayerApi Only）")]
        public bool Left_sendPlayer = false;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        public bool Left_onlyPlayerName = false;
        /// <summary>
        /// 只有一位玩家触发，sendPlayer 必须为 true
        /// </summary>
        [Header("只有一位玩家触发")]
        [Tooltip("sendPlayer 必须为 true")]
        public bool Left_onlyOnePlayerInteract = false;
        /// <summary>
        /// 已触发的玩家
        /// </summary>
        [UdonSynced][NonSerialized] public string[] Left_isInteractedPlayerName = new string[0];
        public VRCPlayerApi Left_OnPlayerHit_VRCPlayerApi;
        public virtual void Left_OnPlayerHit()
        {
            var player = Left_OnPlayerHit_VRCPlayerApi;
            var _playerName = player.displayName;
            if (Left_isOnce
            && Left_isInteracted)
            {
                if (Left_onlyOnePlayerInteract || Left_isLocalOnly)
                    return;
                if (Left_sendPlayer)
                {
                    if (player != null
                    && Array.IndexOf(Left_isInteractedPlayerName, _playerName) != -1)
                        return;
                }
                else
                    return;
            }
            for (int i = 0; i < Left_udonSender.Length; i++)
            {
                if (Left_udonSender[i] == null)
                    continue;
                if (Left_sendPlayer
                && !string.IsNullOrEmpty(_playerName))
                {
                    var udonSenderValues = (object[])Left_udonSender[i].GetProgramVariable("values");
                    if (udonSenderValues == null)
                        continue;
                    if (Left_onlyPlayerName)
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
                            _udonSenderValues.SetValue(player, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    Left_udonSender[i].SetProgramVariable("values", udonSenderValues);
                }
                Left_udonSender[i].SendCustomEvent("SendFunctions");
            }
            Left_isInteracted = true;
            if (Left_sendPlayer
            && !string.IsNullOrEmpty(_playerName)
            && Array.IndexOf(Left_isInteractedPlayerName, _playerName) == -1)
            {
                Left_isInteractedPlayerName = UdonArrayPlus.Add(Left_isInteractedPlayerName, _playerName);
            }
        }
    }
}
