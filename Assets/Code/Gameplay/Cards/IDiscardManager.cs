namespace Assets.Code.Logic
{
    internal interface IDiscardManager : IService
    {
        void AddDiscard(bool forPedro, int count);
        bool Discard(BaseCard cardl);
        bool NeedDiscard(bool pedro);
    }
}