using Grpc.Net.Client;
using Microsoft.VisualBasic.FileIO;
using ServerGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServerGrpc.Protos.SearchEngine;

namespace ClientGrpc
{
    internal class SearchService
    {
        private SearchEngineClient _client;

        private SearchEngineClient Client
        {
            get
            {
                if(_client == null)
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7044");
                    _client = new SearchEngineClient(channel);
                }
                return _client;
            }
        }
        public async Task Search()
        {
            var stream = Client.Search();

            StringBuilder query = new StringBuilder();
            char keyPressed;
            do
            {
                keyPressed = Console.ReadKey().KeyChar;
                if (keyPressed == '\r' && query.Length > 0)
                    break;
                else if (keyPressed == '\b' && query.Length > 0)
                {
                    query.Length--;
                    Console.Write(" \b");
                    await stream.RequestStream.WriteAsync(new SearchQuery { IsEnterPressed = false, Query = query.ToString() });
                }
                else if (!char.IsControl(keyPressed))
                {
                    query.Append(keyPressed);
                    //send query to the service
                    await stream.RequestStream.WriteAsync(new SearchQuery { IsEnterPressed = false, Query = query.ToString() });
                }
            } while (true);
            await stream.RequestStream.WriteAsync(new SearchQuery { IsEnterPressed = true, Query = query.ToString() });
        }
    }
}
