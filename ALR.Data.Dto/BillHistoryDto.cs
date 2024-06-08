namespace ALR.Data.Dto
{
    public class BillHistoryDto
    {
        public int billType { get; set; }
        public DateTime paymentDate { get; set; }
        public float cost { get; set; }
        public string billDescription { get; set; }
        public int status { get; set; }
        public Guid userId { get; set; }
    }
}
