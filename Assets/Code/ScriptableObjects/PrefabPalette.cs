using UnityEngine;
using UnityEngine.Rendering.Universal;


[CreateAssetMenu(fileName = "PrefabPalette", menuName = "Game/PrefabPalette")]
internal class PrefabPalette : ScriptableObject, IService
{
    public DollModel DollPrefab;
    public Light2D Light;
    //public Doll[] DollsPrefab;
    //public GameObject[] Numbers;
}

