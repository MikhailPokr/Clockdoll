using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Game/DialogueLine")]
internal class DialogueLine : ScriptableObject
{
    public AudioClip AudioClip;
    [Space]
    [Multiline] public string _eng;
    [Multiline] public string _ru;
}
