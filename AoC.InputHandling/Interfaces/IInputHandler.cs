namespace AoC.InputHandling.Interfaces;

public interface IInputHandler
{
    public Task<string> GetInput(int year, int day);
}