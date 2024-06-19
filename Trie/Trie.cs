using System.Text;

namespace TrieDataStructure
{
    public class Trie
    {
        const int MAX_CHAR = 26;
        List<Trie?> Child;
        bool IsLeaf;
        public Trie()
        {
            Child = Enumerable.Repeat((Trie?)null, MAX_CHAR).ToList();
            IsLeaf = false;
        }
        public Trie(IEnumerable<string> words)
        {
            Child = Enumerable.Repeat((Trie?)null, MAX_CHAR).ToList();
            IsLeaf = false;
            foreach (var item in words)
            {
                Insert(item);
            }
        }

        public void Insert(string str, int idx = 0)
        {
            if (idx == str.Length)
                IsLeaf = true;
            else
            {
                int cur = str[idx] - 'a';
                if (Child[cur] == null)
                    Child[cur] = new Trie();
                Child[cur]?.Insert(str, idx + 1);
            }
        }
        public void SearchWord(string str, bool isEnterPressed)
        {
            if (isEnterPressed == true)
                Insert(str);
            StringBuilder sb = new StringBuilder();
            SearchWord(str, ref sb);
            Console.WriteLine("--------------------");
        }
        private void SearchWord(string str, ref StringBuilder formedStr, int idx = 0)
        {
            if (idx < str.Length)
            {
                if (Child[str[idx] - 'a'] != null)
                {
                    formedStr.Append(str[idx]);
                    Child[str[idx] - 'a']?.SearchWord(str, ref formedStr, idx + 1);
                }
            }
            else
            {
                if (IsLeaf == true)
                    Console.WriteLine(formedStr);
                for (int i = 0; i < MAX_CHAR; i++)
                {
                    if (Child[i] != null)
                    {
                        formedStr.Append((char)(i + 'a'));
                        Child[i]?.SearchWord(str, ref formedStr, idx);
                        formedStr.Length--;
                    }
                }
            }
        }
    }
}
