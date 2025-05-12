internal interface IReplaceManager : IService
{
    void AddPlace(int place);
    void InsertDoll();
    void Replace();
    void RotateAll(bool clockwise);
}