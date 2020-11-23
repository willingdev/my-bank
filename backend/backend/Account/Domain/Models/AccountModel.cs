using System.Runtime.Serialization;

namespace MyBank.Account.Domain.Models
{
    [DataContract]
    public class AccountModel
    {
        [DataMember]
        public string Id { set; get; }
        [DataMember]
        public string CustomerId { set; get; }

        [DataMember]
        public double TotalMoney { set; get; }
    }

}
