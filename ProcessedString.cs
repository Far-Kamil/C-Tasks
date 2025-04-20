using _8_task.Controllers;

namespace _8_task
{
    public class Constans
    {
        public const string englishLower = "abcdefghijklmnopqrstuvwxyz";
        public const string englishVowels = "aeiouy";
    }
    public interface IProcessedString
    {
        Task ProcessString(ProcessedStringData processedStringData, string sortMethod);
    }
    public class ProcessedString: IProcessedString, IDisposable
    {
        private readonly IRandom randomService;
        private readonly List<String> blackList;
        public ProcessedString(IRandom randomService, List<string> blackList)
        {
            this.randomService = randomService;
            this.blackList = blackList;
        }

        public async Task ProcessString(ProcessedStringData procesedStringData, string sortMethod)
        {
            var notAllowedChars = GetNonMatchingCharacters(procesedStringData.OriginalString, Constans.englishLower);
            if (notAllowedChars.Count > 0)
                throw new ArgumentException($"Characters are not allowed: {string.Join(", ", notAllowedChars)}");
            if (isInBlackList(procesedStringData.OriginalString))
                throw new ArgumentException($"String {procesedStringData.OriginalString} is in blacklist");
            procesedStringData.ProcessedString = InvertAndJoin(procesedStringData.OriginalString);
            procesedStringData.CharacterOccurrences = CountCharacterOccurrences(procesedStringData.ProcessedString);
            procesedStringData.LongestSubstring = GetLongestSubstring(procesedStringData.ProcessedString, Constans.englishVowels);
            procesedStringData.SortedString = SortProcessedString(procesedStringData.ProcessedString, sortMethod);
            procesedStringData.TruncatedString = await TruncateString(procesedStringData.ProcessedString);
        }
        private bool isInBlackList(string str)
        {
            foreach (string blackListed in blackList)
            {
                if (blackListed == str)
                    return true;
            }
            return false;
        }
        private static string SortProcessedString(string str, string sortMethod)
        {
            if (sortMethod == "treeSort")
                str = str.TreeSorted();
            else if (sortMethod == "quickSort")
                str = str.QuickSorted();
            else
                throw new ArgumentException($"{sortMethod} sorting method is not available");
            return str;
        }

        private HashSet<char> GetNonMatchingCharacters(string str, string allowedChars)
        {
            var notAllowedChars = new HashSet<char>();
            foreach (var ch in str)
            {
                if (!allowedChars.Contains(ch))
                    notAllowedChars.Add(ch);
            }
            return notAllowedChars;
        }

        private static string InvertAndJoin(string str)
        {
            var isEven = str.Length % 2 == 0;
            if (isEven)
            {
                var firstHalf = str[..(str.Length / 2)];
                var secondHalf = str.Substring(str.Length / 2, str.Length / 2);
                return new string(firstHalf.Reverse().ToArray()) + new string(secondHalf.Reverse().ToArray());
            }
            else
                return new string(str.Reverse().ToArray()) + str;
        }

        private static string GetLongestSubstring(string str, string boundaries)
        {
            var left = -1;
            var right = -1;
            for (var i = 0; i < str.Length; i++)
            {
                if (boundaries.Contains(str[i]))
                {
                    left = i;
                    break;
                }
            }
            for (var i = str.Length - 1; i >= 0; i--)
            {
                if (boundaries.Contains(str[i]))
                {
                    right = i;
                    break;
                }
            }
            if (left == -1 || right == -1)
                return "";
            return str.Substring(left, right - left + 1);
        }

        private static Dictionary<char, int> CountCharacterOccurrences(string str)
        {
            var characterOccurences = new Dictionary<char, int>();
            foreach (var ch in str)
            {
                if (characterOccurences.ContainsKey(ch))
                    characterOccurences[ch]++;
                else
                    characterOccurences.Add(ch, 1);
            }
            return characterOccurences;
        }

        private async Task<string> TruncateString(string str)
        {
            int randInt;
            try
            {
                randInt = await randomService.Next(str.Length - 1);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Connection failed");
                randInt = new Random().Next(str.Length - 1);
            }
            return str.Remove(randInt, 1);
        }
        public void Dispose()
        {
            randomService.Dispose();
        }
    }
}
