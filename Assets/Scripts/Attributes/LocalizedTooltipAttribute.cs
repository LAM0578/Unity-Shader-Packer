using System;
using UnityEngine;

namespace NCat.Tools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LocalizedTooltipAttribute : PropertyAttribute
    {
        public LocalizedTooltipAttribute(string zhHans, string en)
        {
            this.zhHans = zhHans;
            this.en = en;
        }
        
        private readonly string zhHans;
        private readonly string en;
        
        public string Get()
        {
            var isChinese = Application.systemLanguage == SystemLanguage.Chinese;
            return isChinese ? zhHans : en;
        }
    }
}
