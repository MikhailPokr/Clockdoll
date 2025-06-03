using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class DialogueSystem : IDialogueSystem
{
    private IDataLoader _dataLoader;
    private Palette _palette;
    private DialogueBox _dialogueBox;
    public Canvas _canvas;
    private CoreTicker _ticker;
    private int _page;
    private string _textboxJson;
    private string _displayText;
    private string _localizationKey;

    public List<DialoguePageData> _jsonData;
    private string _contentText {
        get {
            return DataSearch($"{_textboxJson}_{_localizationKey}_{_page}").content;
        }
    }
    private string _speakerText {
        get {
            return DataSearch($"{_textboxJson}_{_localizationKey}_{_page}").speaker;
        }
    }
    private string[] _variations {
        get {
            return DataSearch($"{_textboxJson}_{_localizationKey}_{_page}").variations;
        }
    }
    private int _dialogueLinesCount
    {
        get
        {
            return _jsonData.Count(data => data.key.Contains(_textboxJson));
        }
    }
    private float delay {
        get {
            return 0.035f;
        }
    }


    private Coroutine _currentCoroutine;
    private bool _isCurrentlyPrinting;
    private int _currentCharIndex = 0;

    public event Action OnPageTurned;

    public DialogueSystem(IDataLoader dataLoader, CoreTicker coreTicker, Palette palette)
    {
        _dataLoader = dataLoader;
        _ticker = coreTicker;
        _palette = palette;

        OnPageTurned += OnPageTurn;

        _jsonData = _dataLoader.LoadJsonList<DialoguePageData>("json");
    }


    private void CreateDialogueBoxMultipageByKey(string key)
    {
        CreateTextBox();

        ResetVariables();

        FillText(false);

        _textboxJson = key;

        _dialogueBox.button.onClick.AddListener(PageClick);

        _page = 0;
        TypewritePrintText();
    }

    public void ChangeLocalizationKey(string key)
    {
        _localizationKey = key;
    }

    public DialoguePageData ReturnJsonData(string jsonData, int page)
    {
        _textboxJson = jsonData;
        _page = page;
        return DataSearch($"{_textboxJson}_{_localizationKey}_{_page}");
    }

    public void CreateDialogueBoxByKey(string keyWithPage, int page)
    {
        CreateTextBox();

        ResetVariables();

        FillText(false);

        _textboxJson = keyWithPage;
        _page = page;

        _dialogueBox.button.onClick.AddListener(DestroyTextBox);

        TypewritePrintText();
    }

    private void PageClick() 
    {
        if (_isCurrentlyPrinting) { 
            FillText(true);
            _ticker.StopCoroutine(_currentCoroutine); 
            _isCurrentlyPrinting = false;
            return;
        }

        if (_page + 1 >= _dialogueLinesCount) { 
            DestroyTextBox(); 
            return;
        }

        ResetVariables();
        _page++;
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
        _currentCoroutine = _ticker.StartCoroutine(TextDelay(delay));
    }

    IEnumerator TextDelay(float secondsToWait)
    {
        int charsToAdd = _currentCharIndex != ' ' ? 1 : 2;

        while (_currentCharIndex < _contentText.Length) {
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
        if (charsToAdd == 0) {
            FillText(true);
            _currentCharIndex = _contentText.Length;
            return;
        }

        for (int i = 0; i < charsToAdd; i++) {
            _displayText += _contentText[_currentCharIndex];
            _currentCharIndex++;
        }
    }

    private void FillText(bool toBlankOrFull)
    {
        if (toBlankOrFull) { 
            _displayText = _contentText;
            _dialogueBox.textContent.text = _contentText;
        } else { 
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


    private DialoguePageData DataSearch(string searchedKey)
    {
        DialoguePageData searchedData;
        foreach (DialoguePageData data in _jsonData)
        {
            if (data.key == searchedKey)
            {
                searchedData = data;
                return searchedData;
            }
        }
        return null;
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