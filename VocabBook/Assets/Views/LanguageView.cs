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
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace com.quentintran.view
{
    public class LanguageView : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        UIDocument document;

        [SerializeField]
        VisualTreeAsset languageEntry;

        ListView languageListView;

        List<LanguageModel> languages = new List<LanguageModel>();

        Button addButton;

        #endregion

        #region Methods

        private void Start()
        {
            languageListView = document.rootVisualElement.Q<ListView>("language-list-view");
            languageListView.selectionType = SelectionType.Single;
            languageListView.makeItem = MakeItem;
            languageListView.bindItem = BindItem;
            languageListView.itemsSource = languages;
            languageListView.onSelectionChange += (selected) =>
            {
                Debug.Log("TODO " + selected.First());
            };

            Refresh();

            BindDialogueBox(document.rootVisualElement);

            addButton = document.rootVisualElement.Q<Button>("add-btn");

            BindLanguagePopUp(document.rootVisualElement);

            addButton.clickable.clicked += OpenLanguagePopUp;
        }

        private VisualElement MakeItem()
        {
            var element = languageEntry.CloneTree();

            element.Q<Button>("remove-language-btn").clickable.clicked += () =>
            {
                LanguageModel lang = element.userData as LanguageModel;
                if (lang != null)
                {
                    OpenDialogueBox((b) =>
                    {
                        if (b)
                        {
                            VocabBookDatabase.instance.DeleteLanguage(lang);
                            Refresh();
                        }
                    },
                    "Are your sure you want to delete <b>" + lang.name + " </b>?",
                    "Yes", "Cancel");
                }
            };

            return element;
        }

        private void BindItem(VisualElement element, int index)
        {
            element.Q<Label>().text = languages[index].name;
            element.userData = languages[index];
        }

        private void Refresh()
        {
            languages.Clear();
            languages.AddRange(VocabBookDatabase.instance.languages);
            languageListView.Refresh();
        }

        #region DialogueBox

        VisualElement dialogueBox;

        System.Action<bool> onClosePopUp;

        Label dialogueContent;

        Button dialogueBoxOptionABtn;

        Button dialogueBoxOptionBBtn;

        private void BindDialogueBox(VisualElement root)
        {
            dialogueBox = root.Q<VisualElement>("dialogue-box");
            dialogueContent = root.Q<Label>("dialoguebox-content");
            dialogueBoxOptionABtn = root.Q<Button>("dialoguebox-option-1-btn");
            dialogueBoxOptionABtn.clickable.clicked += () => { onClosePopUp?.Invoke(true); HideDialogueBox(); };

            dialogueBoxOptionBBtn = root.Q<Button>("dialoguebox-option-2-btn");
            dialogueBoxOptionBBtn.clickable.clicked += () => { onClosePopUp?.Invoke(false); HideDialogueBox(); };

            HideDialogueBox();
        }

        public void HideDialogueBox()
        {
            dialogueBox.style.display = DisplayStyle.None;
        }

        public void OpenDialogueBox(System.Action<bool> onCloseCallback, string content, string optionA, string optionB)
        {
            dialogueBox.style.display = DisplayStyle.Flex;

            onClosePopUp = onCloseCallback;
            dialogueContent.text = content;
            dialogueBoxOptionABtn.text = optionA;
            dialogueBoxOptionBBtn.text = optionB;
        }

        #endregion

        #region Add Language Pop Up

        VisualElement addLanguagePopUp;
        Button addLanguageBtn;
        Button cancelLanguageBtn;
        TextField newLanguageNameField;

        private void BindLanguagePopUp(VisualElement root)
        {
            addLanguagePopUp = root.Q<VisualElement>("add-language-pup-up");
            addLanguageBtn = addLanguagePopUp.Q<Button>("add-language-btn");
            addLanguageBtn.clickable.clicked += () =>
            {
                VocabBookDatabase.instance.InsertLanguage(newLanguageNameField.value);
                Refresh();
                HideLangagePopUp();
            };

            cancelLanguageBtn = addLanguagePopUp.Q<Button>("cancel-add-language-btn");
            cancelLanguageBtn.clickable.clicked += HideLangagePopUp;


            newLanguageNameField = addLanguagePopUp.Q<TextField>("new-language-name-field");

            HideLangagePopUp();
        }

        public void HideLangagePopUp()
        {
            addLanguagePopUp.style.display = DisplayStyle.None;
        }

        public void OpenLanguagePopUp()
        {
            addLanguagePopUp.style.display = DisplayStyle.Flex;
            newLanguageNameField.value = string.Empty;
        }

        #endregion

        #endregion
    }
}