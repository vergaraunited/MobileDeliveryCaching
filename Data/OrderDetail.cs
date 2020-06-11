using SQLite;
using MobileDeliveryGeneral.Data;

namespace DataCaching.Data
{
    public class OrderDetail : BaseData<OrderDetail>, isaCacheItem<OrderDetail>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public long ManifestId { get; set; }
        [Indexed]
        public int DSP_SEQ { get; set; }
        public int CustomerId { get; set; }
        public long DLR_NO { get; set; }
        [Indexed]
        public long ORD_NO { get; set; }
        public string CLR { get; set; }
        public short MDL_CNT { get; set; }
        public string MDL_NO { get; set; }
        public string MODEL { get; set; }
        public byte PAT_POS { get; set; }
        public string Status { get; set; }

        public OrderDetail() { }

        public OrderDetail(OrderDetailsData dt)
        {
            //this.ManifestId = dt.ManifestId;
            //this.DSP_SEQ = dt.DSP_SEQ;
            //this.CustomerId = dt.CustomerId;
            this.DLR_NO = dt.DLR_NO;
            this.ORD_NO = dt.ORD_NO;
            this.CLR = dt.CLR;
            this.MDL_CNT = dt.MDL_CNT;
            this.MDL_NO = dt.MDL_NO;
            //this.Status = dt.Status;
        }

        public OrderDetailsData OrderData()
        {
            return new OrderDetailsData()
            {
                //ManifestId = this.ManifestId,
                //DSP_SEQ = this.DSP_SEQ,
                Command = MobileDeliveryGeneral.Definitions.MsgTypes.eCommand.OrdersLoad,
                //CustomerId = this.CustomerId,
                DLR_NO = this.DLR_NO,
                ORD_NO = this.ORD_NO,
                CLR = this.CLR,
                MDL_CNT = this.MDL_CNT,
                MDL_NO = this.MDL_NO,
                //BIN_NO = this.BIN_NO
                //Status = this.Status
            };
        }
        public override int CompareTo(OrderDetail other)
        {
            return ManifestId.CompareTo(other.ManifestId) + DSP_SEQ.CompareTo(other.DSP_SEQ) +
                ORD_NO.CompareTo(other.ORD_NO) + MDL_CNT.CompareTo(other.MDL_CNT) +
                MDL_NO.CompareTo(other.MDL_NO);// + WIN_CNT.CompareTo(other.WIN_CNT) +
                //BIN_NO.CompareTo(other.BIN_NO);
        }
    }
}
