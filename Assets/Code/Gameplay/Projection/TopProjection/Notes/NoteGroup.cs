using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Palette;

public class NoteGroup : MonoBehaviour, IInitializable
{
    [SerializeField] private int _notesCount;
    [SerializeField] private Transform _flyAwayPoint;
    [SerializeField] private float _animationDuration = 0.5f; // Установите разумное значение по умолчанию
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _flyAwayHeight;

    private NoteView[] _notes;
    private IDollPlacementController _placementController;
    private Palette _palette;
    private INoteMarkerData _markerData;

    private int _currentTopNoteIndex = 0;
    private bool _isAnimating = false;
    private Coroutine _animationCoroutine;

    public void Initialize()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        _placementController.CurrentPlaceChanged += OnDataChanged;
        _placementController.PlacementChanged += () => OnDataChanged(0);

        _palette = ServiceLocator.Resolve<Palette>();
        _markerData = ServiceLocator.Resolve<INoteMarkerData>();

        _notes = new NoteView[_notesCount];

        for (int i = 0; i < _notes.Length; i++)
        {
            NoteView model = Instantiate(_palette.NotePrefab, transform);
            model.Initialize(_palette.MarkerSprites, this);
            model.transform.position = transform.position;
            model.transform.SetAsFirstSibling(); 
            ClockNum doll = _placementController.GetCurrentDollIndex();
            model.UpdateMark(_markerData.GetDollMarkers(doll), _palette.DollsData.First(x => x.Index == doll).Symbol);
            _notes[i] = model;
        }

        _currentTopNoteIndex = _notes.Length-1; 
        _isAnimating = false;

        _markerData.MarkChanged += OnMarkChanged;
    }

    public void Click(int num)
    {
        _markerData.SetMark(num, _placementController.GetCurrentDollIndex());
    }

    public void OnMarkChanged()
    {
        ClockNum doll = _placementController.GetCurrentDollIndex();
        _notes[_currentTopNoteIndex].UpdateMark(_markerData.GetDollMarkers(doll), _palette.DollsData.First(x => x.Index == doll).Symbol);
    }

    private void OnDataChanged(ClockNum _)
    {
        if (_isAnimating) return; 

        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
        _animationCoroutine = StartCoroutine(AnimateNoteMovement());
    }

    private IEnumerator AnimateNoteMovement()
    {
        _isAnimating = true;

        NoteView topNote = _notes[_currentTopNoteIndex];
        Vector3 parentCenter = transform.position;

        int nextTopIndex = (_currentTopNoteIndex + 1) % _notes.Length;
        ClockNum doll = _placementController.GetCurrentDollIndex();
        _notes[nextTopIndex].UpdateMark(_markerData.GetDollMarkers(doll), _palette.DollsData.First(x => x.Index == doll).Symbol);

        float elapsedTime = 0f;
        while (elapsedTime < _animationDuration / 2)
        {
            float t = elapsedTime / (_animationDuration / 2);
            topNote.transform.position = Vector3.Lerp(
                parentCenter,
                _flyAwayPoint.position,
                t * t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        topNote.transform.SetAsFirstSibling();
        Quaternion newRotation = Quaternion.Euler(0, 0, Random.Range(-_maxRotationAngle, _maxRotationAngle));

        elapsedTime = 0f;
        while (elapsedTime < _animationDuration / 2)
        {
            float t = elapsedTime / (_animationDuration / 2);
            topNote.transform.position = Vector3.Lerp(
                _flyAwayPoint.position,
                parentCenter,
                t * t); 
            topNote.transform.rotation = Quaternion.Lerp(
                topNote.transform.rotation,
                newRotation,
                t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        topNote.transform.position = parentCenter;
        topNote.transform.rotation = newRotation;

        _currentTopNoteIndex = (_currentTopNoteIndex + 1) % _notes.Length;
        _isAnimating = false;
    }
}