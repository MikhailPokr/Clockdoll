using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
internal class TestButtonDialogue : MonoBehaviour, IInitializable
{
    private DialogueSystem DialogueSystem;

    public void Initialize()
    {
        DialogueSystem = ServiceLocator.Resolve<DialogueSystem>();
    }

    public void Click(string key)
    {
        DialogueSystem.CreateDialogueBoxByKey(key);
    }
}
