namespace ClientGrpc
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var service = new SearchService();
            await service.Search();
        }
    }
}
