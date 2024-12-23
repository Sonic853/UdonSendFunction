﻿
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Sonic853.Udon.SendFunction
{
    public class UdonInteractFunctionWithInt : UdonSharpBehaviour
    {
        /// <summary>
        /// 需要调用的UdonBehaviour
        /// </summary>
        [Header("需要调用的UdonBehaviour")]
        [SerializeField] public UdonBehaviour[] udonBehaviours;
        /// <summary>
        /// 互动后将调用以下的函数
        /// </summary>
        [Header("进入后将调用以下的函数")]
        [SerializeField] public string[] functionNames;
        /// <summary>
        /// 需要调整参数的变量名
        /// </summary>
        [Header("需要调整参数的变量名")]
        [SerializeField] public string[] valueNames;
        /// <summary>
        /// 需要调整参数的值
        /// </summary>
        [Header("需要调整参数的值")]
        [SerializeField] public int[] values;
        /// <summary>
        /// 只允许本地玩家触发
        /// </summary>
        [Header("只允许本地玩家触发")]
        [SerializeField] private bool isLocalOnly = true;
        /// <summary>
        /// 只允许触发一次
        /// </summary>
        [Header("只允许触发一次")]
        [SerializeField] private bool isOnce = false;
        /// <summary>
        /// 是否已触发
        /// </summary>
        [NonSerialized] private bool _isInteracted = false;
        public void Interact_()
        {
            if (isOnce && _isInteracted)
                return;
            for (int i = 0; i < udonBehaviours.Length; i++)
            {
                if (udonBehaviours[i] == null)
                    continue;
                if (i >= functionNames.Length)
                    break;
                if (string.IsNullOrEmpty(functionNames[i]))
                    continue;
                if (i < valueNames.Length && !string.IsNullOrEmpty(valueNames[i]))
                    udonBehaviours[i].SetProgramVariable(valueNames[i], values[i]);
                udonBehaviours[i].SendCustomEvent(functionNames[i]);
            }
            _isInteracted = true;
        }
        public override void Interact()
        {
            if (isLocalOnly)
            {
                Interact_();
            }
            else
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "Interact_");
            }
        }
    }
}
