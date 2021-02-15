using System.Collections.Generic;
namespace IconicFund.Web.Models
{
    public class DashBoardStatusChartViewModel
    {
        public List<ChartsDataViewModel> TransactionDepartmentsBar { get; set; }
        public List<ChartsDataViewModel> TransactionCompletionsPie { get; set; }
        public List<ChartsDataViewModel> TransactionsPeriorityBar { get; set; }
        public List<ChartsDataViewModel> TransactionsIsSecretBar { get; set; }
        public List<ChartsDataViewModel> IncomingTransactionsSourceBar { get; set; }

      
    }
    public class ChartsDataViewModel
    {
        public string label { get; set; }
        public List<string> value { get; set; }
        public string colorValue { get; set; }
        public List<string> fields { get; set; }
        public ChartsDataViewModel()
        {
            fields = new List<string>();
            value = new List<string>();
        }
    }

}
