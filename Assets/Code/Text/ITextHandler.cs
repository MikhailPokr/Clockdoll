internal interface ITextHandler : IService
{
    TextData ReturnJsonData(string jsonData, int page);
    TextData DataSearch(string searchedKey);
    TextData ReturnJsonData(string jsonData);
}
