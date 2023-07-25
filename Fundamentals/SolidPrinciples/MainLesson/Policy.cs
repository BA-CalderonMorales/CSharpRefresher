namespace Fundamentals.SolidPrinciples.MainLesson
{
    public class Policy
    {
        public PolicyType Type { get; set; }

        #region Life
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSmoker { get; set; }
        public decimal Amount { get; set; }
        #endregion

        #region Land
        public string Address { get; set; }
        public decimal Size { get; set; }
        public decimal Valuation { get; set; }
        public decimal BondAmount { get; set; }
        #endregion

        #region Auto
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Miles { get; set; }
        public decimal Deductible { get; set; }
        #endregion

        #region Flood
        public decimal ElevationAboveSeaLevel { get; set; }
        #endregion

        #region Public methods
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Policy p = (Policy)obj;

            return
            Type == p.Type &&
            FullName == p.FullName &&
            DateOfBirth == p.DateOfBirth &&
            IsSmoker == p.IsSmoker &&
            Amount == p.Amount &&
            Address == p.Address &&
            Size == p.Size &&
            Valuation == p.Valuation &&
            BondAmount == p.BondAmount &&
            Make == p.Make &&
            Model == p.Model &&
            Year == p.Year &&
            Miles == p.Miles &&
            Deductible == p.Deductible;
        }
        #endregion
    }
}
