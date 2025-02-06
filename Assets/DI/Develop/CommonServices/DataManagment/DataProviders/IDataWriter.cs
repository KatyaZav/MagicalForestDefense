namespace DI.Game.Develop.CommonServices.DataManagment.DataProviders
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);   
    }
}
