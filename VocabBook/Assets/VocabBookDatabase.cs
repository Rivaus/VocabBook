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

using com.quentintran.models;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace com.quentintran
{
    [System.Serializable]
    public class VocabBookDatabase
    {
        public static VocabBookDatabase instance;

        public List<LanguageModel> languages = new List<LanguageModel>();

        public List<WordModel> words = new List<WordModel>();

        public List<TagModel> tags = new List<TagModel>();

        public VocabBookDatabase()
        {
            if (instance != null)
                UnityEngine.Debug.LogError("INTERNAR ERROR");
            else
            {
                instance = this;
            }
        }

        public void InsertTag(string tagName)
        {
            TagModel tag = new TagModel { tagName = tagName };
            tags.Add(tag);
            Save();
        }

        public void DeleteTag(TagModel tag)
        {
            tag.Delete();
            tags.Remove(tag);
            Save();
        }

        public void InsertWord(string word, string translation, string pronounciation, string note, LanguageModel language, List<TagModel> tags)
        {
            WordModel w = new WordModel
            {
                word = word,
                translation = translation,
                pronounciation = pronounciation,
                additionalInformation = note,
                language = language,
                tags = tags
            };
            words.Add(w);
            Save();
        }

        public void DeleteWord(WordModel word)
        {
            word.Delete();
            words.Remove(word);
            Save();
        }

        public void InsertLanguage(string name)
        {
            LanguageModel l = new LanguageModel { name = name };
            languages.Add(l);
            Save();
        }

        public void DeleteLanguage(LanguageModel language)
        {
            language.Delete();
            languages.Remove(language);
            Save();
        }

        private void Save()
        {
            File.WriteAllText(AppManager.filePath, JsonUtility.ToJson(this, true));
        }
    }
}

