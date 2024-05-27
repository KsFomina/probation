namespace Ex4
{
    internal class Party
    {
        public string Number {  get; set; }
        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
        public double Parameter3 { get; set; }
        public double Parameter4 { get; set; }
        public double Parameter5 { get; set; }
        public double Parameter6 { get; set; }
        public double Parameter7 { get; set; }

        public bool MatchesCriteria(Party other)
        {
            return this.Parameter1 == other.Parameter1 &&
                   Math.Abs(this.Parameter2 - other.Parameter2) <= 10 &&
                   Math.Abs(this.Parameter3 - other.Parameter3) <= 0.8 &&
                   Math.Abs(this.Parameter4 - other.Parameter4) <= 1.5 &&
                   Math.Abs(this.Parameter5 - other.Parameter5) <= 1 &&
                   Math.Abs(this.Parameter6 - other.Parameter6) <= 9 &&
                   Math.Abs(this.Parameter7 - other.Parameter7) <= 1;
        }
    }
}
