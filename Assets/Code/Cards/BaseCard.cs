using UnityEngine;

internal abstract class BaseCard
{
    protected string _title;
    protected string _description;
    protected Color _color;

    public abstract void Play();
    public abstract bool Check();

    public virtual (string name, string description, Color color) GetData() => (_title, _description, _color);
}