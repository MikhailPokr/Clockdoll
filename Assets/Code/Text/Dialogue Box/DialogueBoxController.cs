using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    private CoreTicker _coreticker;
    private Coroutine _currentCoroutine;
    private bool _isCurrentlyPrinting;
    private int _currentCharIndex;
    private string _displayText;
    public event Action OnPageTurned;
    public List<TextData> _jsonData;

    public DialogueBoxController(
        IDataLoader dataLoader,
        LocalizationHandler localizationHandler,
        ITextHandler textHandler,
        CoreTicker coreTicker,
        Palette palette,
        Canvas canvas)
    {
        _dataLoader = dataLoader;
        _localizationHandler = localizationHandler;
        _textHandler = textHandler;
        _coreticker = coreTicker;
        _palette = palette;
        _canvas = canvas;

        OnPageTurned += OnPageTurn;

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
            _coreticker.StopCoroutine(_currentCoroutine);
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
        OnPageTurned?.Invoke();
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
        _currentCoroutine = _coreticker.StartCoroutine(TextDelay(_delay));
    }

    IEnumerator TextDelay(float secondsToWait)
    {
        int charsToAdd = _currentCharIndex != ' ' ? 1 : 2;

        while (_currentCharIndex < _contentText.Length)
        {
            _isCurrentlyPrinting = true;

            TextTypingRepeat(charsToAdd);

            _dialogueBox.textSpeaker.text = _speakerText;
            _dialogueBox.textContent.text = _displayText;


            yield return new Delay(secondsToWait);
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
