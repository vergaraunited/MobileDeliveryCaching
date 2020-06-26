using SQLite;
using MobileDeliveryGeneral.Data;

namespace DataCaching.Data
{
    public class Order : BaseData<Order>, isaCacheItem<Order>
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
        public int MDL_CNT { get; set; }
        public string MDL_NO { get; set; }
        public int WIN_CNT { get; set; }
        public decimal WIDTH { get; set; }
        public decimal HEIGHT{ get; set; }
        public Order() { }

        public Order(OrderData dt)
        {
            this.ManifestId = dt.ManifestId;
            //this.DSP_SEQ = dt.DSP_SEQ;
            //this.CustomerId = dt.CustomerId;
            this.DLR_NO = dt.DLR_NO;
            this.ORD_NO = dt.ORD_NO;
            //this.CLR = dt.CLR;
            //this.MDL_CNT = dt.MDL_CNT;
            //this.MDL_NO = dt.MDL_NO;
            //this.HEIGHT = dt.HEIGHT;
            //this.WIDTH = dt.WIDTH;
            //this.WIN_CNT = dt.WIN_CNT;
            this.status = dt.status;
        }

        public OrderData OrderData()
        {
            return new OrderData()
            {
                ManifestId = this.ManifestId,
                //DSP_SEQ = this.DSP_SEQ,
                Command = MobileDeliveryGeneral.Definitions.MsgTypes.eCommand.OrdersLoad,
            //    CustomerId = this.CustomerId,
                DLR_NO = (int)DLR_NO,
                ORD_NO = (int)this.ORD_NO,
                //CLR = this.CLR,
                //MDL_CNT = this.MDL_CNT,
                //MDL_NO = this.MDL_NO,
                //WIN_CNT = this.WIN_CNT,
                //WIDTH = this.WIDTH,
                //HEIGHT = this.HEIGHT,
                status = this.status
            };
        }
        public OrderMasterData OrderMasterData()
        {
            return new OrderMasterData()
            {
                ManId = this.ManifestId,
                //DSP_SEQ = this.DSP_SEQ,
                Command = MobileDeliveryGeneral.Definitions.MsgTypes.eCommand.OrdersLoad,
                //    CustomerId = this.CustomerId,
                DLR_NO = (int)DLR_NO,
                ORD_NO = (int)this.ORD_NO,
                //CLR = this.CLR,
                //MDL_CNT = this.MDL_CNT,
                //MDL_NO = this.MDL_NO,
                //WIN_CNT = this.WIN_CNT,
                //WIDTH = this.WIDTH,
                //HEIGHT = this.HEIGHT,
                status = this.status
            };
        }
        public override int CompareTo(Order other)
        {
            return ManifestId.CompareTo(other.ManifestId) + DSP_SEQ.CompareTo(other.DSP_SEQ) + 
                ORD_NO.CompareTo(other.ORD_NO) + MDL_CNT.CompareTo(other.MDL_CNT) + 
                MDL_NO.CompareTo(other.MDL_NO) + HEIGHT.CompareTo(other.HEIGHT) + WIDTH.CompareTo(other.WIDTH);
        }
    }
}
