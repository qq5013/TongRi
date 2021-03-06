using System;
using System.Collections.Generic;
using System.Text;

namespace Sorting.Dispatching
{
    [Serializable]
    public class RefreshData
    {
        private int totalRoute;
        private int totalCustomer;
        private int totalQuantity;

        private int completeRoute;
        private int completeCustomer;
        private int completeQuantity;

        private int average;
        private string batchNo;

        public int Average
        {
            get { return average; }
            set { average = value; }
        }

        public int TotalRoute
        {
            get { return totalRoute; }
            set { totalRoute = value; }
        }

        public int TotalCustomer
        {
            get { return totalCustomer; }
            set { totalCustomer = value; }
        }

        public int TotalQuantity
        {
            get { return totalQuantity; }
            set { totalQuantity = value; }
        }

        public int CompleteRoute
        {
            get { return completeRoute; }
            set { completeRoute = value; }
        }
        public int CompleteCustomer
        {
            get { return completeCustomer; }
            set { completeCustomer = value; }
        }

        public int CompleteQuantity
        {
            get { return completeQuantity; }
            set { completeQuantity = value; }
        }

        public string BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }
    }
}
