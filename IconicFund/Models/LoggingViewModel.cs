using IconicFund.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace IconicFund.Web.Models
{
    public class LoggingViewModel
    {

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string RowID { get; set; }

        public LoggingCategory? LoggingCategory { get; set; }

        public LoggingAction? LoggingAction { get; set; }



        public IPagedList<LoggingViewModelItems> LoggingList { get; set; }

    }

    public class LoggingViewModelItems
    {

        public long ID { get; set; }

        public LoggingCategory LoggingCategory { get; set; }

        public LoggingAction LoggingAction { get; set; }


        public string RowID { get; set; }


        public DateTime ActionDate { get; set; } = DateTime.Now;

        public string UserName { get; set; }



    }

}
