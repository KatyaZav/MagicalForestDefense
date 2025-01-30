using UnityEngine;
using System;

namespace Assets.Servises.Configs
{
    [CreateAssetMenu(fileName = "TranslateData", menuName = "Configs/System/TranslateData", order = 1)]
    public class TextTranslateConfig : ScriptableObject
    {
        private const string ErrorNullText = "Language {0} not filled in {1}";
        private const string ErrorNotFoundLanguage = "Not found language \"{0}\" for {1} config";

        [field: SerializeField] public string RussianText { get; private set; }
        [field: SerializeField] public string EnglishText { get; private set; }

        public string GetText(string language)
        {
            CheakNull();

            switch (language)
            {
                case "ru":
                    return RussianText;
                case "en":
                    return EnglishText;
                default:
                    throw new ArgumentException(string.Format(ErrorNotFoundLanguage, language, name));
            }
        }

        private void CheakNull()
        {
            if (RussianText == null)
                throw new ArgumentException(string.Format(ErrorNullText, "Russian", name));

            if (EnglishText == null)
                throw new ArgumentException(string.Format(ErrorNullText, "English", name));
        }
    }
}
