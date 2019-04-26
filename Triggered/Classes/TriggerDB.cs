using System;

namespace Triggered
{
    public class TriggerDB
    {
        public TriggerDB(DateTime userDate, string comment, string reporter)
        {
            this.userDate=userDate;
            this.comment=comment;
            this.reporter=reporter;
        }

        public DateTime userDate { get; set; }
        public string id { get; set; }
        public string comment { get; set; }
        public string reporter { get; set; }
    }
}