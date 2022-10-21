namespace TcoCognexVision.Converters
{
    public class IndexedData<T>
    {
        public int Index { get; private set; }
        public T Data { get; private set; }

        public IndexedData(int Index, T Data)
        {
            this.Index = Index;
            this.Data = Data;
        }
    }
}
