
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class LocalTriggerSender : UdonSharpBehaviour
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
                return;
            foreach (UdonBehaviour udon in udonSenderEnter)
            {
                if (udon == null)
                    continue;
                if (sendPlayerEnter)
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
        }
        public void OnPlayerTriggerExit_()
        {
            var _playerName = OnPlayerTriggerEnter_VRCPlayerApi_.displayName;
            if (isOnceExit
            && isExited)
                return;
            foreach (UdonBehaviour udon in udonSenderExit)
            {
                if (udon == null)
                    continue;
                if (sendPlayerExit)
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
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!enableEnter)
                return;
            if (player != Networking.LocalPlayer) return;
            OnPlayerTriggerEnter_VRCPlayerApi_ = player;
            OnPlayerTriggerEnter_();
            OnPlayerTriggerEnter_VRCPlayerApi_ = null;
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (!enableExit)
                return;
            if (player != Networking.LocalPlayer) return;
            OnPlayerTriggerExit_VRCPlayerApi_ = player;
            OnPlayerTriggerExit_();
            OnPlayerTriggerExit_VRCPlayerApi_ = null;
        }
    }
}
