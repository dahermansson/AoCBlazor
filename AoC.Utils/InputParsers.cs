namespace AoC.Utils
{
    public static class InputParsers
    {
        public static string[] GetInputLines(string input) => input.Split(Environment.NewLine);
        public static T[] GetInputLines<T>(string input) => GetInputLines(input).Select( s => (T) Convert.ChangeType(s, typeof(T))).ToArray();
        public static string[][] GetInputLinesMatrix(string filename) => 
            File.ReadAllLines(Path.Combine("inputs", filename)).Select(t => t.ToCharArray().Select(p => p.ToString()).ToArray()).ToArray();
        public static int[] GetIntArrayFromSingleLine(string filename) => File.ReadAllText(Path.Combine("inputs", filename)).Split(',').Select(int.Parse).ToArray();
        public static Dictionary<int, string[]> GetGroupsOfLines(string filename, int groupSize) => GetInputLines(filename).Select((s, i) => (index: i, value: s)).GroupBy(t => t.index/groupSize)
        .ToDictionary(x => x.First().index, y => y.Select(v => v.value).ToArray());
        public static StreamReader GetInputStreamReader(string filename) => new StreamReader(Path.Combine("inputs", filename));
    
    }
}