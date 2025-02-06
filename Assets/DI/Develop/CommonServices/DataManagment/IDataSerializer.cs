namespace DI.Game.Develop.CommonServices.DataManagment
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);
        TData Deserialize<TData>(string serializedData);
    }
}
