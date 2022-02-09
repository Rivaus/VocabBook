/*
MIT License

Copyright (c) 2022 Quentin Tran

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections.Generic;

namespace com.quentintran.models
{
    [System.Serializable]
    public class WordModel : AbstractModel
    {
        /// <summary>
        /// Word users want to learn.
        /// </summary>
        public string word;

        /// <summary>
        /// Transalation of <see cref="word"/> into the native langage.
        /// </summary>
        public string translation;

        /// <summary>
        /// Guide to pronounce <see cref="word"/>.
        /// </summary>
        public string pronounciation;

        /// <summary>
        /// Additional information about <see cref="word"/>.
        /// </summary>
        public string additionalInformation;

        /// <summary>
        /// Lanuage of <see cref="word"/>.
        /// </summary>
        public LanguageModel language;

        /// <summary>
        /// List of tags associated to <see cref="word"/>.
        /// </summary>
        public List<TagModel> tags;


        public override string ToString()
        {
            return word + "[" + pronounciation + "] (" + language?.name + ") : " + translation + " (FR). " + additionalInformation;
        }
    }
}

