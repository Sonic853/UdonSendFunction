
using System;
using Sonic853.Udon.ArrayPlus;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonTriggerSender : UdonSharpBehaviour
    {
        #region 进入
        /// <summary>
        /// 启用进入
        /// </summary>
        public bool enableEnter = true;
        /// <summary>
        /// 进入后需要调用的 Udon Sender
        /// </summary>
        [Header("进入后需要调用的 Udon Sender")]
        public UdonBehaviour[] udonSenderEnter;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        public bool isLocalOnlyEnter = true;
        /// <summary>
        /// 只允许进入一次
        /// </summary>
        public bool isOnceEnter = false;
        /// <summary>
        /// 放入玩家
        /// </summary>
        [Header("放入玩家")]
        [Tooltip("（UdonSendFunctionsWithVRCPlayerApi Only）")]
        public bool sendPlayerEnter = false;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        public bool onlyPlayerNameEnter = false;
        /// <summary>
        /// 只有一位玩家触发，sendPlayerEnter 必须为 true
        /// </summary>
        [Header("只有一位玩家触发")]
        [Tooltip("sendPlayerEnter 必须为 true")]
        public bool onlyOnePlayerEnter = false;
        /// <summary>
        /// 已触发的玩家
        /// </summary>
        [UdonSynced][NonSerialized] public string[] isEnteredPlayerName = new string[0];
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool isEntered = false;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        #endregion
        #region 退出
        /// <summary>
        /// 启用退出
        /// </summary>
        public bool enableExit = true;
        /// <summary>
        /// 退出后需要调用的 Udon Sender
        /// </summary>
        [Header("退出后需要调用的 Udon Sender")]
        public UdonBehaviour[] udonSenderExit;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        public bool isLocalOnlyExit = true;
        /// <summary>
        /// 只允许退出一次
        /// </summary>
        public bool isOnceExit = false;
        /// <summary>
        /// 放入玩家
        /// </summary>
        [Header("放入玩家")]
        [Tooltip("（UdonSendFunctionsWithVRCPlayerApi Only）")]
        public bool sendPlayerExit = false;
        /// <summary>
        /// 只传入玩家名称
        /// </summary>
        [Header("只传入玩家名称")]
        [Tooltip("（UdonSendFunctionsWithString Only）")]
        public bool onlyPlayerNameExit = false;
        /// <summary>
        /// 只有一位玩家触发，sendPlayerEnter 必须为 true
        /// </summary>
        [Header("只有一位玩家触发")]
        [Tooltip("sendPlayerEnter 必须为 true")]
        public bool onlyOnePlayerExit = false;
        /// <summary>
        /// 已触发的玩家
        /// </summary>
        [UdonSynced][NonSerialized] public string[] isExitedPlayerName = new string[0];
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool isExited = false;
        [NonSerialized] private VRCPlayerApi OnPlayerTriggerExit_VRCPlayerApi_ = null;
        #endregion
        public void OnPlayerTriggerEnter_()
        {
            var _playerName = OnPlayerTriggerEnter_VRCPlayerApi_.displayName;
            if (isOnceEnter
            && isEntered)
            {
                if (onlyOnePlayerEnter || isLocalOnlyEnter)
                    return;
                if (sendPlayerEnter)
                {
                    if (OnPlayerTriggerEnter_VRCPlayerApi_ != null
                    && Array.IndexOf(isEnteredPlayerName, _playerName) != -1)
                        return;
                }
                else
                    return;
            }
            foreach (UdonBehaviour udon in udonSenderEnter)
            {
                if (udon == null)
                    continue;
                if (sendPlayerEnter
                && !string.IsNullOrEmpty(_playerName))
                {
                    var udonSenderValues = (object[])udon.GetProgramVariable("values");
                    if (udonSenderValues == null)
                        continue;
                    if (onlyPlayerNameEnter)
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
                            _udonSenderValues.SetValue(OnPlayerTriggerEnter_VRCPlayerApi_, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    udon.SetProgramVariable("values", udonSenderValues);
                }
                udon.SendCustomEvent("SendFunctions");
            }
            isEntered = true;
            if (sendPlayerEnter
            && !string.IsNullOrEmpty(_playerName)
            && Array.IndexOf(isEnteredPlayerName, _playerName) == -1)
            {
                isEnteredPlayerName = UdonArrayPlus.Add(isEnteredPlayerName, _playerName);
            }
        }
        public void OnPlayerTriggerExit_()
        {
            var _playerName = OnPlayerTriggerExit_VRCPlayerApi_.displayName;
            if (isOnceExit
            && isExited)
            {
                if (onlyOnePlayerExit || isLocalOnlyExit)
                    return;
                if (sendPlayerExit)
                {
                    if (OnPlayerTriggerExit_VRCPlayerApi_ != null
                    && Array.IndexOf(isExitedPlayerName, _playerName) != -1)
                        return;
                }
                else
                    return;
            }
            foreach (UdonBehaviour udon in udonSenderExit)
            {
                if (udon == null)
                    continue;
                if (sendPlayerExit
                && !string.IsNullOrEmpty(_playerName))
                {
                    var udonSenderValues = (object[])udon.GetProgramVariable("values");
                    if (udonSenderValues == null)
                        continue;
                    if (onlyPlayerNameExit)
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
                            _udonSenderValues.SetValue(OnPlayerTriggerExit_VRCPlayerApi_, j);
                        }
                        udonSenderValues = _udonSenderValues;
                    }
                    udon.SetProgramVariable("values", udonSenderValues);
                }
                udon.SendCustomEvent("SendFunctions");
            }
            isExited = true;
            if (sendPlayerExit
            && !string.IsNullOrEmpty(_playerName)
            && Array.IndexOf(isExitedPlayerName, _playerName) == -1)
            {
                isExitedPlayerName = UdonArrayPlus.Add(isExitedPlayerName, _playerName);
            }
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!enableEnter)
                return;
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (!enableExit)
                return;
            OnPlayerTriggerExit_VRCPlayerApi_ = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi_ = null;
        }
    }
}
