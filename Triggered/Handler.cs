using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;

namespace Triggered
{
    public class Handler
    {

        public static List<String> DateTime = new List<string>();



        public Handler(TriggerDB trigger)
        {
            
            try
            {
                UserDialogs.Instance.ShowLoading("Sending Data....");
                IMobileServiceTable<TriggerDB> trigger_event = App.client.GetTable<TriggerDB>();

                trigger_event.InsertAsync(trigger);
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Data Successfully Imported");


            }
            catch(Exception e)
            {
               
                UserDialogs.Instance.Alert(e.ToString());        
    }

        }



        public  Handler()
        {
           
        }


        public List<string> getDateTime()
        {
            return DateTime;
        }





        public async  Task LoadStuff()
        {
            UserDialogs.Instance.ShowLoading("Downloading Reports");
            try
            {
                var Report = App.client.GetTable<TriggerDB>();
                var result = await Report.Where(x => x.userDate != null && x.comment!=string.Empty ).ToListAsync();


                foreach (var i in result)
                {
                    String build = i.userDate.ToString() + " " + i.comment;
                    DateTime.Add(build);


                }
                
                UserDialogs.Instance.HideLoading();



               
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(e.ToString());
            }
        }
    }
}
