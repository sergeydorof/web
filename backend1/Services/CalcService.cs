namespace backend.Services
{
    public class CalcService : ICalcService
    {
        public int RandValueFirst { get; }
        public int RandValueSecond { get; }

        public CalcService()
        {
            var random = new Random();
            RandValueFirst = random.Next(0, 11);
            RandValueSecond = random.Next(0, 11);
        }

        public string GetAdd() => $"{RandValueFirst} + {RandValueSecond} = {RandValueFirst + RandValueSecond}";
        public string GetSub() => $"{RandValueFirst} - {RandValueSecond} = {RandValueFirst - RandValueSecond}";
        public string GetMult() => $"{RandValueFirst} * {RandValueSecond} = {RandValueFirst * RandValueSecond}";

        public string GetDiv()
        {
            if (RandValueSecond == 0)
            {
                return "Error: Division by zero";
            }
            return $"{RandValueFirst} / {RandValueSecond} = {RandValueFirst / RandValueSecond}";
        }
    }
}
