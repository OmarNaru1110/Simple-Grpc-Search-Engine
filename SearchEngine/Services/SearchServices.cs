using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ServerGrpc.Protos;
using TrieDataStructure;
using static ServerGrpc.Protos.SearchEngine;

namespace ServerGrpc.Services
{
    public class SearchServices:SearchEngineBase
    {
        public SearchServices() 
        {
        }
        public override async Task<Empty> Search(IAsyncStreamReader<SearchQuery> requestStream, ServerCallContext context)
        {
            var task = Task.Run(async () =>
            {
                await foreach (var item in requestStream.ReadAllAsync())
                    Memory.Dict.SearchWord(item.Query, item.IsEnterPressed);
                    
            });
            await task;
            return new Empty();
        }
    }
}
