using System.ComponentModel.Design;
using System.Security.Cryptography;

namespace AoC.Solvers.Y2017;

public class Day09(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;


    private StreamState ScoreGroups((StreamState streamState, char current) state) => state switch
    {
        { streamState.IgnoreNext: true } => state.streamState with { IgnoreNext = false },
        { current: '!' } => state.streamState with { IgnoreNext = true },
        { current: '<', streamState.InGarbage: false } => state.streamState with { InGarbage = true },
        { current: '>', } => state.streamState with { InGarbage = false },
        { current: '{', streamState.InGarbage: false } => state.streamState with { GroupScore = state.streamState.GroupScore + 1 },
        { current: '}', streamState.InGarbage: false } => state.streamState with
            {
                GroupScore = state.streamState.GroupScore - 1,
                Groups = [.. state.streamState.Groups, state.streamState.GroupScore]
            },
        _ => state.streamState with
            {
                GarbageCount = state.streamState.InGarbage
                ? state.streamState.GarbageCount + 1
                : state.streamState.GarbageCount
            }
    };

    public int Star1() => Input.Aggregate(new StreamState(), (streamState, current) => ScoreGroups((streamState, current))).Groups.Sum();

    public int Star2() => Input.Aggregate(new StreamState(), (streamState, current) => ScoreGroups((streamState, current))).GarbageCount;

    record StreamState(bool InGarbage = false, int GroupScore = 0, bool IgnoreNext = false)
    {
        public List<int> Groups { get; init; } = [];
        public int GarbageCount { get; init; } = 0;
    }
}