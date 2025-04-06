using UnityEngine;

internal class DollAxis : RotatableAxis
{
    //исключительно для теста, потом удалить
    Color[] rainbowPalette = new[]
    {
        Color.red,    
        new Color(0.5f, 0.5f, 0),   
        Color.yellow,    
        Color.green,     
        new Color(0.56f,0.93f,0.56f), 
        Color.cyan,     
        Color.blue,       
        new Color(0, 0, 0.55f),   
        new Color(0.63f,0.13f,0.94f),   
        Color.magenta,    
        new Color(1, 0.75f, 0.8f),      
        new Color(0.5f, 1, 0)     
    };

    protected override GameObject Generate(int place)
    {
        Doll doll = Instantiate(_palette.DollPrefab, transform);
        doll.ChangeNumber(_seccionData.GetDollIndex(place), rainbowPalette[place - 1]);
        return doll.gameObject;
    }
}
