using SQLite;
using MobileDeliveryGeneral.Data;

namespace DataCaching.Data
{
    public class Stop : isaCacheItem<Stop>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public long ManifestId { get; set; }
        public int DisplaySeq { get; set; }
        public long DealerNo { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        [Indexed]
        public string TRK_CDE { get; set; }
        public int CustomerId { get; set; }
        public bool BillComplete { get; set; }

        //[ForeignKey(typeof(Truck))]     // Specify the foreign key
        //public int ManifestId { get; set; }
        //public DateTime Time { get; set; }
        //public decimal Price { get; set; }

        //[ManyToOne]      // Many to one relationship with Stock
        //public Stock Stock { get; set; }

        public Stop()
        { }
        public Stop(Stop stop)
        {
            Id = stop.Id;
            ManifestId = stop.ManifestId;
            DisplaySeq = stop.DisplaySeq;
            DealerNo = stop.DealerNo;
            DealerName = stop.DealerName;
            Address = stop.Address;
            PhoneNumber = stop.PhoneNumber;
            Description = stop.Description;
            Notes = stop.Notes;
            TRK_CDE = stop.TRK_CDE;
            CustomerId = stop.CustomerId;
            BillComplete = stop.BillComplete;
        }
        public Stop(StopData sd)
        {
            ManifestId = sd.ManifestId;
            DisplaySeq = sd.DisplaySeq;
            DealerNo = sd.DealerNo;
            DealerName = sd.DealerName;
            Address = sd.Address;
            PhoneNumber = sd.PhoneNumber;
            Description = sd.Description;
            Notes = sd.Notes;
            TRK_CDE = sd.TruckCode;
            CustomerId = sd.CustomerId;
            BillComplete = sd.BillComplete;
        }
        public StopData StopData()
        {
            return new StopData() { Address = this.Address, BillComplete = this.BillComplete,
                Command = MobileDeliveryGeneral.Definitions.MsgTypes.eCommand.Stops,
                CustomerId = this.CustomerId, DealerName = this.DealerName, DealerNo = this.DealerNo,
                Description = this.Description, DisplaySeq = this.DisplaySeq, ManifestId = this.ManifestId,
                Notes = this.Notes, PhoneNumber = this.PhoneNumber,
                TruckCode = this.TRK_CDE };
        }
        public int CompareTo(Stop other)
        {
            return TRK_CDE.CompareTo(other.TRK_CDE) +
                ManifestId.CompareTo(other.ManifestId) +
                DisplaySeq.CompareTo(other.DisplaySeq) +
                CustomerId.CompareTo(other.CustomerId);
            //throw new System.NotImplementedException();
        }
    }
}
