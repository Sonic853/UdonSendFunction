
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonCoreSender : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的 Udon Sender
        /// </summary>
        [Header("需要调用的 Udon Sender")]
        public UdonBehaviour[] udonSender;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        public bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] public bool isInteracted = false;
    }
}
