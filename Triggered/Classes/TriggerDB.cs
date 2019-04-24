using System;
namespace Triggered
{
    public class TriggerDB
    {
        public DateTime userDate { get; set; }
        public String id  { get; set; }
        public String comment { get; set; }
        public String reporter { get; set; }
        public TriggerDB(DateTime userDate,String comment,String reporter)
        {
            this.userDate = userDate;
            this.comment = comment;
            this.reporter=reporter;
        }
    }
}
