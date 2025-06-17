namespace šachy_upg
{
    public class Result
    {
        public hrac Winner { get; }
        public EndReason Reason { get; }
        public Result(hrac winner, EndReason reason)
        {
            Winner = winner;
            Reason = reason;
        }

        public static Result Win(hrac winner)
        {
            return new Result(winner, EndReason.Checkmate);
        }

        public static Result draw(EndReason reason)
        {
            return new Result(hrac.None, reason);
        }
    }
}
