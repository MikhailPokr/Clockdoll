using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

internal class DialogueBoxController : IDialogueBoxController
{
    private IDataLoader _dataLoader;
    private LocalizationHandler _localizationHandler;
    private ITextHandler _textHandler;
    private DialogueBox _dialogueBox;
    public Canvas _canvas;
    private Palette _palette;
    private int _index;
    private string _textUniqueKey;
    private float _delay;
    private bool _isCurrentlyPrinting;
    private int _currentCharIndex;
    private string _displayText;
    public List<TextData> _jsonData;

    public DialogueBoxController(
        IDataLoader dataLoader,
        LocalizationHandler localizationHandler,
        ITextHandler textHandler,
        Palette palette,
        Canvas canvas)
    {
        _dataLoader = dataLoader;
        _localizationHandler = localizationHandler;
        _textHandler = textHandler;
        _palette = palette;
        _canvas = canvas;

        _jsonData = _dataLoader.LoadJsonList<TextData>("json");

        _delay = 0.035f;
    }

    private string _contentText {
        get {
            return _textHandler.DataSearch($"{_textUniqueKey}_{_localizationHandler.localizationKey}_{_index}").content;
        }
    }
    private string _speakerText {
        get {
            return _textHandler.DataSearch($"{_textUniqueKey}_{_localizationHandler.localizationKey}_{_index}").speaker;
        }
    }
    private int _dialoguePagesCount {
        get {
            return _jsonData.Count(data => data.key.Contains(_textUniqueKey));
        }
    }

    private void CreateDialogueBoxMultipageByKey(string key)
    {
        CreateTextBox();

        ResetVariables();

        FillText(false);

        _textUniqueKey = key;

        _dialogueBox.button.onClick.AddListener(PageClick);

        _index = 0;
        TypewritePrintText();
    }

    public void CreateDialogueBoxByKey(string keyWithPage, int page)
    {
        CreateTextBox();

        ResetVariables();

        FillText(false);

        _textUniqueKey = keyWithPage;
        _index = page;

        _dialogueBox.button.onClick.AddListener(DestroyTextBox);

        TypewritePrintText();
    }

    private void PageClick()
    {
        if (_isCurrentlyPrinting)
        {
            FillText(true);
            _isCurrentlyPrinting = false;
            return;
        }

        if (_index + 1 >= _dialoguePagesCount)
        {
            DestroyTextBox();
            return;
        }

        ResetVariables();
        _index++;
        OnPageTurn();
    }

    private void OnPageTurn()
    {
        TypewritePrintText();
    }

    private void ResetVariables()
    {
        _currentCharIndex = 0;
        FillText(false);
    }

    private void TypewritePrintText()
    {
        TextDelay(_delay).Forget();
    }

    private async UniTask TextDelay(float secondsToWait)
    {
        int charsToAdd = _currentCharIndex != ' ' ? 1 : 2;

        while (_currentCharIndex < _contentText.Length)
        {
            _isCurrentlyPrinting = true;

            TextTypingRepeat(charsToAdd);

            _dialogueBox.textSpeaker.text = _speakerText;
            _dialogueBox.textContent.text = _displayText;

            await UniTask.Delay(TimeSpan.FromSeconds(secondsToWait));
        }
        _isCurrentlyPrinting = false;
    }

    private void TextTypingRepeat(int charsToAdd)
    {
        if (charsToAdd == 0)
        {
            FillText(true);
            _currentCharIndex = _contentText.Length;
            return;
        }

        for (int i = 0; i < charsToAdd; i++)
        {
            _displayText += _contentText[_currentCharIndex];
            _currentCharIndex++;
        }
    }

    private void FillText(bool toBlankOrFull)
    {
        if (toBlankOrFull)
        {
            _displayText = _contentText;
            _dialogueBox.textContent.text = _contentText;
        }
        else
        {
            _displayText = "";
            _dialogueBox.textContent.text = ""; 
        }
    }

    private void CreateTextBox()
    {
        _dialogueBox = GameObject.Instantiate(_palette.DialogueBoxPrefab, _canvas.transform);
    }

    private void DestroyTextBox()
    {
        GameObject.Destroy(_dialogueBox.gameObject);
    }
    
    private class Delay : CustomYieldInstruction
    {
        private float timer;
        private float targetTime;

        public Delay(float delay) {
            timer = 0f;
            targetTime = delay;
        }

        public override bool keepWaiting {
            get {
                timer += Time.deltaTime;

                return timer < targetTime;
            }
        }
    }
}
