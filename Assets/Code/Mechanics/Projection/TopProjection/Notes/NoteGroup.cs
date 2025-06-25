using UnityEngine;
using System.Linq;
using DG.Tweening;
using static Palette;

public class NoteGroup : MonoBehaviour, IInitializable
{
    [SerializeField] private int _notesCount;
    [SerializeField] private Transform _flyAwayPoint;
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private float _maxRotationAngle;

    private NoteView[] _notes;
    private IDollPlacementController _placementController;
    private Palette _palette;
    private INoteMarkerData _markerData;
    private int _currentTopNoteIndex;
    private Sequence _animationSequence;

    public void Initialize()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        _placementController.CurrentPlaceChanged += OnDataChanged;
        _placementController.PlacementChanged += () => OnDataChanged(0);

        _palette = ServiceLocator.Resolve<Palette>();
        _markerData = ServiceLocator.Resolve<INoteMarkerData>();

        InitializeNotes();
        _markerData.MarkChanged += OnMarkChanged;
    }

    private void InitializeNotes()
    {
        _notes = new NoteView[_notesCount];
        _currentTopNoteIndex = _notes.Length - 1;

        for (int i = 0; i < _notes.Length; i++)
        {
            _notes[i] = Instantiate(_palette.NotePrefab, transform);
            _notes[i].Initialize(_palette.MarkerSprites, this);
            _notes[i].transform.SetAsFirstSibling();
            UpdateNoteData(_notes[i]);
        }
    }

    private void UpdateNoteData(NoteView note)
    {
        ClockNum doll = _placementController.GetCurrentDollIndex();
        Doll dollData = _palette.DollsData.First(x => x.Index == doll);
        note.UpdateMark(_markerData.GetDollMarkers(doll), dollData.Symbol);
    }

    public void Click(int num) => _markerData.SetMark(num, _placementController.GetCurrentDollIndex());

    public void OnMarkChanged() => UpdateNoteData(_notes[_currentTopNoteIndex]);

    private void OnDataChanged(ClockNum _)
    {
        if (_animationSequence != null)
        {
            if (_animationSequence.IsActive())
            {
                _animationSequence.Kill(true);
            }
            ResetNotePosition();
        }
        AnimateNoteMovement();
    }

    private void ResetNotePosition()
    {
        _notes[_currentTopNoteIndex].transform.position = transform.position;
    }

    private void AnimateNoteMovement()
    {
        // Создаем новую последовательность
        _animationSequence = DOTween.Sequence();

        NoteView topNote = _notes[_currentTopNoteIndex];
        int nextTopIndex = (_currentTopNoteIndex + 1) % _notes.Length;
        UpdateNoteData(_notes[nextTopIndex]);

        // Настройка анимации
        _animationSequence.Append(
            topNote.transform.DOMove(_flyAwayPoint.position, _animationDuration / 2)
                .SetEase(Ease.OutQuad)
        );

        _animationSequence.AppendCallback(() => topNote.transform.SetAsFirstSibling());

        Vector3 randomRotation = new Vector3(0, 0, Random.Range(-_maxRotationAngle, _maxRotationAngle));
        _animationSequence.Append(
            topNote.transform.DOMove(transform.position, _animationDuration / 2)
                .SetEase(Ease.InQuad)
        );
        _animationSequence.Join(
            topNote.transform.DORotate(randomRotation, _animationDuration / 2)
        );

        _animationSequence.OnComplete(() => {
            _currentTopNoteIndex = nextTopIndex;
            _animationSequence = null; 
        });

        _animationSequence.SetLink(gameObject);
    }

    private void OnDestroy()
    {
        _placementController.CurrentPlaceChanged -= OnDataChanged;
        _placementController.PlacementChanged -= () => OnDataChanged(0);
        _markerData.MarkChanged -= OnMarkChanged;

        if (_animationSequence != null)
            _animationSequence.Kill();
    }
}