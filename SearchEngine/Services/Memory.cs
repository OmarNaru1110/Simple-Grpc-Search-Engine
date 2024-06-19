using TrieDataStructure;

namespace ServerGrpc.Services
{
    public static class Memory
    {
        public static Trie Dict { get; set; } = new Trie(/*new List<string> {"naru","naruto","narutu"}*/);
    }
}
