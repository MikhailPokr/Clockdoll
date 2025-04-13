using UnityEngine;
using System.Collections;

public class NoteGroup : MonoBehaviour, IInitializable
{
    [SerializeField] private int _notesCount = 0;
    [SerializeField] private Transform _flyAwayPoint;
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private float _maxRotationAngle = 15f;
    [SerializeField] private float _flyAwayHeight = 1f;

    private MarkerData.Mode _mode;

    private NoteModel[] _notes;
    private TableData _tableData;
    private Palette _palette;
    private MarkerData _markerData;

    private int _currentTopNoteIndex = 0;
    private bool _isAnimating = false;

    public void Initialize()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
        _tableData.CurrentPlaceChanged += OnDataChanged;
        _tableData.PlacementChanged += () => OnDataChanged(0);
        _palette = ServiceLocator.Resolve<Palette>();
        _markerData = ServiceLocator.Resolve<MarkerData>();

        _notes = new NoteModel[_notesCount];
        for (int i = 0; i < _notes.Length; i++)
        {
            NoteModel model = Instantiate(_palette.NotePrefab, transform);
            model.Initialize(_palette.MarkerSprites, this);
            model.transform.position = transform.position;
            model.transform.SetSiblingIndex(i);
            model.UpdateMark(_markerData.GetDollMarkers(_tableData.GetCurrentDollIndex()));
            _notes[i] = model;
        }

        _currentTopNoteIndex = 0;
        _isAnimating = false;

        for (int i = 0; i < 3; i++)
        {
            int nextIndex = (_currentTopNoteIndex + 1) % _notes.Length;
            _notes[nextIndex].UpdateMark(_markerData.GetDollMarkers(_tableData.GetCurrentDollIndex()));
            _currentTopNoteIndex = nextIndex;

            _notes[_currentTopNoteIndex].transform.SetAsLastSibling();
        }

        _markerData.MarkChanged += () => _notes[_currentTopNoteIndex].UpdateMark(_markerData.GetDollMarkers(_tableData.GetCurrentDollIndex()));
    }


    public void Click(int num)
    {
        _markerData.SetMark(num, _tableData.GetCurrentDollIndex());
    }

    private void OnDataChanged(int _)
    {
        int nextTopIndex = (_currentTopNoteIndex + 1) % _notes.Length;
        _notes[nextTopIndex].UpdateMark(_markerData.GetDollMarkers(_tableData.GetCurrentDollIndex()));

        if (!_isAnimating)
        {
            StartCoroutine(AnimateNoteMovement());
        }
    }

    private IEnumerator AnimateNoteMovement()
    {
        _isAnimating = true;

        NoteModel topNote = _notes[_currentTopNoteIndex];
        Vector3 parentCenter = transform.position; 

        Vector3 startOffset = topNote.transform.position - parentCenter;
        Quaternion startRotation = topNote.transform.rotation;

        float elapsedTime = 0f;
        while (elapsedTime < _animationDuration / 2)
        {
            float t = elapsedTime / (_animationDuration / 2);

            Vector3 currentPos = Vector3.Lerp(
                Vector3.Lerp(parentCenter + startOffset, parentCenter + Vector3.up * _flyAwayHeight, t),
                Vector3.Lerp(parentCenter + Vector3.up * _flyAwayHeight, _flyAwayPoint.position, t),
                t);

            topNote.transform.position = currentPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        topNote.transform.SetAsFirstSibling();

        Quaternion newRotation = Quaternion.Euler(0, 0, Random.Range(-_maxRotationAngle, _maxRotationAngle));

        elapsedTime = 0f;
        while (elapsedTime < _animationDuration / 2)
        {
            float t = elapsedTime / (_animationDuration / 2);
            topNote.transform.position = Vector3.Lerp(_flyAwayPoint.position, parentCenter, t);
            topNote.transform.rotation = Quaternion.Lerp(startRotation, newRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        topNote.transform.position = parentCenter;
        topNote.transform.rotation = newRotation;
        _currentTopNoteIndex = (_currentTopNoteIndex + 1) % _notes.Length;
        _isAnimating = false;
    }
}