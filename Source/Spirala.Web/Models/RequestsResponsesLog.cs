using System;
using System.ComponentModel.DataAnnotations;

namespace Aut3.Models
{
    public class RequestsResponsesLog
    {
        public RequestsResponsesLog(Guid logId, string whoChanged, string whichModel,string method, Guid idOfChangedItem, string whichValue, string previousValue, string nextValue, DateTime dateTimeOfChange)
        {
            LogId = logId;
            WhoChanged = whoChanged;
            WhichModel = whichModel;
            IdOfChangedItem = idOfChangedItem;
            Method = method;
            WhichValue = whichValue;
            PreviousValue = previousValue;
            NextValue = nextValue;
            DateTimeOfChange = dateTimeOfChange;
        }

        [Key]
        public Guid LogId { get; set; }
        public string WhoChanged { get; set; }
        public string WhichModel { get; set; }
        public string Method { get; set; }
        
        public Guid IdOfChangedItem { get; set; }
        public string WhichValue { get; set; }
        public string PreviousValue { get; set; }
        public string NextValue { get; set;  }
        public DateTime DateTimeOfChange { get; set; }
        
    }
    
    
}