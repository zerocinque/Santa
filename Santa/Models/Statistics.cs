namespace Santa.Models
{
    public class Statistics
    {
        public int OrdersRequest { get; set; }
        public int OrdersReady { get; set; }
        public int OrderShipped { get; set; }

        public int WarehouseToys { get; set; }
        public int FinishedToys { get; set; }
    }
}