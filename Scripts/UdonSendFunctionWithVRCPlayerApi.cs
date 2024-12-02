
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Sonic853.Udon.SendFunction
{
    public class UdonSendFunctionWithVRCPlayerApi : UdonSendFunctionValue
    {
        /// <summary>
        /// 需要调整参数的值
        /// </summary>
        [Header("需要调整参数的值")]
        public VRCPlayerApi value;
        /// <summary>
        /// 需要调整参数的值
        /// </summary>
        public VRCPlayerApi[] values
        {
            get
            {
                return new VRCPlayerApi[] { value };
            }
            set
            {
                Debug.LogWarning("UdonSendFunctionWithVRCPlayerApi 只能保存一个 value");
                if (value == null || value.Length == 0)
                    this.value = default;
                else
                    this.value = value[0];
            }
        }
        public override void SendFunction()
        {
            if (isOnce && _isSended)
                return;
            if (udonBehaviour == null)
                return;
            if (string.IsNullOrEmpty(functionName))
                return;
            if (!string.IsNullOrEmpty(valueName))
                udonBehaviour.SetProgramVariable(valueName, value);
            udonBehaviour.SendCustomEvent(functionName);
            _isSended = true;
        }
    }
}
