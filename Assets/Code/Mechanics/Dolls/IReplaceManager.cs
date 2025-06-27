internal interface IReplaceManager : IService
{
    void AddPlace(int place);
    void InsertDoll();
    void StartReplace();
    void RotateAll(bool clockwise);
    void Shuffle();
}