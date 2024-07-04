using CHEExportsDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALOrderDeliverySlipDetailsItems : DALBase
    {
        [DataMember]
        public OrderDeliverySlipDetailsItems iOrderDeliverySlipDetailsItems { get; private set; }

        public DALOrderDeliverySlipDetailsItems(OrderDeliverySlipDetailsItems aOrderDeliverySlipDetailsItems) : base(aOrderDeliverySlipDetailsItems)
        {
            iOrderDeliverySlipDetailsItems = aOrderDeliverySlipDetailsItems;
        }

        public void CreateNewOrderDeliverySlipDetailsItems()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void SaveOrderDeliverySlipDetailsItems(string token)
        {
            try
            {
                ValidateOrderDeliverySlipDetailsItemsSave();
                if (iOrderDeliverySlipDetailsItems != null && (iOrderDeliverySlipDetailsItems.errorMsg_lsit == null || iOrderDeliverySlipDetailsItems.errorMsg_lsit.Count == 0))
                {

                    Save(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void ValidateOrderDeliverySlipDetailsItemsSave()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderDeliverySlipDetailsItems(string token)
        {
            try
            {
                //ValidateUserSave();
                if (iOrderDeliverySlipDetailsItems != null && (iOrderDeliverySlipDetailsItems.errorMsg_lsit == null || iOrderDeliverySlipDetailsItems.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteOrderDeliverySlipDetailsItems(string token)
        {
            try
            {
                // ValidateOrderDeliverySlipDetailsItemsDelete();
                if (iOrderDeliverySlipDetailsItems != null && (iOrderDeliverySlipDetailsItems.errorMsg_lsit == null || iOrderDeliverySlipDetailsItems.errorMsg_lsit.Count == 0) && iOrderDeliverySlipDetailsItems.order_delivery_slip_detail_items_id > 0)
                {
                    Delete(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void OpenOrderDeliverySlipDetailsItems(string token)
        {
            try
            {
                if (iOrderDeliverySlipDetailsItems != null && (iOrderDeliverySlipDetailsItems.errorMsg_lsit == null || iOrderDeliverySlipDetailsItems.errorMsg_lsit.Count == 0) && iOrderDeliverySlipDetailsItems.order_delivery_slip_detail_items_id > 0)
                {
                    Open(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
