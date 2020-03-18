using SQLite;
using System;
using UMDGeneral.Data;

namespace DataCaching.Data
{
    public class Truck : isaCacheItem<Truck>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public long ManifestId { get; set; }
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TruckCode { get; set; }
        [Indexed]
        public string ShipDate { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public bool IsClosed { get; set; }

        public Truck() { }
        public Truck(TruckData td) {
            Id = Id;
            ManifestId = td.ManifestId;
            DriverId = td.DriverId;
            FirstName = td.FirstName;
            LastName = td.LastName;
            TruckCode = td.TRK_CDE;
            ShipDate = td.SHIP_DTE.Date.ToString("yyyy-MM-dd");
            Description = td.Desc;
            Notes = td.NOTES;
            IsClosed = td.IsClosed;
        }
        public TruckData TruckData() {
            return new TruckData()
            {
                Command = UMDGeneral.Definitions.MsgTypes.eCommand.Trucks,
                Desc = Description,
                DriverId = DriverId,
                FirstName = FirstName,
                LastName = LastName,
                IsClosed = IsClosed,
                ManifestId = ManifestId,
                NOTES = Notes,
                SHIP_DTE = DateTime.Parse(ShipDate),
                TRK_CDE = TruckCode,
                Id = Id
            };
        }

        public int CompareTo(Truck other)
        {
            return this.Id.CompareTo(other.Id) + this.ManifestId.CompareTo(other.ManifestId) +
                TruckCode.CompareTo(other.TruckCode) + ShipDate.CompareTo(other.ShipDate);
        }
    }
}
