using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TranslitRu
{
    /// <summary>
    /// Base interface for all converters
    /// </summary>
    public interface ITranslitConverter
    {
        /// <summary>
        /// Convert one cyrilic character into latin string
        /// </summary>
        /// <param name="inputCharacter">cyrillic or other character</param>
        /// <returns></returns>
        string ConvertCharacter(char inputCharacter);

        /// <summary>
        /// Convert one string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        string ConvertString(string inputString);
    }

    public abstract class BaseTrasLitConverter : ITranslitConverter
    {
        #region Implementation of ITranslitConverter

        /// <summary>
        /// Convert one cyrilic character into latin string
        /// </summary>
        /// <param name="inputCharacter">cyrillic or other character</param>
        /// <returns></returns>
        public abstract string ConvertCharacter(char inputCharacter);

        /// <summary>
        /// Convert one string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public string ConvertString(string inputString)
        {
            StringBuilder result = new StringBuilder();
            foreach (var character in inputString)
            {
                result.Append(ConvertCharacter(character));
            }
            return result.ToString();
        }

        #endregion
    }
}
