namespace backend.Services
{
    public interface ICalcService
    {
        int RandValueFirst { get; }
        int RandValueSecond { get; }
        string GetAdd();
        string GetSub();
        string GetMult();
        string GetDiv();
    }
}
