namespace RunFoxyRun
{
    public sealed class GlobalState
    {
        static GlobalState()
        {
            State = new GlobalState();
        }

        private GlobalState()
        {
        }

        public static GlobalState State { get; }

        public int Scores { get; set; }
    }
}
