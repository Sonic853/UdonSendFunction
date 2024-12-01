
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonLab.Toolkit
{
    public class LocalPickupSender : UdonSharpBehaviour
    {
        #region OnPickup
        /// <summary>
        /// 启用拾取触发
        /// </summary>
        [Header("启用拾取触发")]
        public bool enableOnPickup = true;
        /// <summary>
        /// 当玩家拾取时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家拾取时调用的UdonBehaviour")]
        public UdonBehaviour[] udonSenderOnPickup;
        /// <summary>
        /// 拾取只允许触发一次
        /// </summary>
        [Header("拾取只允许触发一次")]
        public bool onPickupIsOnce = false;
        /// <summary>
        /// 拾取是否已触发
        /// </summary>
        [NonSerialized] public bool onPickupIsTriggered = false;
        public void OnPickup_()
        {
            if (onPickupIsOnce && onPickupIsTriggered)
                return;
            foreach (UdonBehaviour udon in udonSenderOnPickup)
            {
                udon.SendCustomEvent("SendFunctions");
            }
            onPickupIsTriggered = true;
        }
        public override void OnPickup()
        {
            if (!enableOnPickup) return;
            OnPickup_();
        }
        #endregion
        #region OnDrop
        /// <summary>
        /// 启用丢弃触发
        /// </summary>
        [Header("启用丢弃触发")]
        public bool enableOnDrop = true;
        /// <summary>
        /// 当玩家丢弃时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家丢弃时调用的UdonBehaviour")]
        public UdonBehaviour[] udonSenderOnDrop;
        /// <summary>
        /// 丢弃只允许触发一次
        /// </summary>
        [Header("丢弃只允许触发一次")]
        public bool onDropIsOnce = false;
        /// <summary>
        /// 丢弃是否已触发
        /// </summary>
        [NonSerialized] public bool onDropIsTriggered = false;
        public void OnDrop_()
        {
            if (onDropIsOnce && onDropIsTriggered)
                return;
            foreach (UdonBehaviour udon in udonSenderOnDrop)
            {
                udon.SendCustomEvent("SendFunctions");
            }
            onDropIsTriggered = true;
        }
        public override void OnDrop()
        {
            if (!enableOnDrop) return;
            OnDrop_();
        }
        #endregion
        #region OnPickupUseDown
        /// <summary>
        /// 启用按下使用触发
        /// </summary>
        [Header("启用按下使用触发")]
        public bool enableOnPickupUseDown = true;
        /// <summary>
        /// 当玩家按下使用时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家按下使用时调用的UdonBehaviour")]
        public UdonBehaviour[] udonSenderOnPickupUseDown;
        /// <summary>
        /// 按下使用只允许触发一次
        /// </summary>
        [Header("按下使用只允许触发一次")]
        public bool onPickupUseDownIsOnce = false;
        /// <summary>
        /// 按下使用是否已触发
        /// </summary>
        [NonSerialized] public bool onPickupUseDownIsTriggered = false;
        public void OnPickupUseDown_()
        {
            if (onPickupUseDownIsOnce && onPickupUseDownIsTriggered)
                return;
            foreach (UdonBehaviour udon in udonSenderOnPickupUseDown)
            {
                udon.SendCustomEvent("SendFunctions");
            }
            onPickupUseDownIsTriggered = true;
        }
        public override void OnPickupUseDown()
        {
            if (!enableOnPickupUseDown) return;
            OnPickupUseDown_();
        }
        #endregion
        #region OnPickupUseUp
        /// <summary>
        /// 启用松开使用触发
        /// </summary>
        [Header("启用松开使用触发")]
        public bool enableOnPickupUseUp = true;
        /// <summary>
        /// 当玩家松开使用时调用的UdonBehaviour
        /// </summary>
        [Header("当玩家松开使用时调用的UdonBehaviour")]
        public UdonBehaviour[] udonSenderOnPickupUseUp;
        /// <summary>
        /// 松开使用只允许触发一次
        /// </summary>
        [Header("松开使用只允许触发一次")]
        public bool onPickupUseUpIsOnce = false;
        /// <summary>
        /// 松开使用是否已触发
        /// </summary>
        [NonSerialized] public bool onPickupUseUpIsTriggered = false;
        public void OnPickupUseUp_()
        {
            if (onPickupUseUpIsOnce && onPickupUseUpIsTriggered)
                return;
            foreach (UdonBehaviour udon in udonSenderOnPickupUseUp)
            {
                udon.SendCustomEvent("SendFunctions");
            }
            onPickupUseUpIsTriggered = true;
        }
        public override void OnPickupUseUp()
        {
            if (!enableOnPickupUseUp) return;
            OnPickupUseUp_();
        }
        #endregion
    }
}

