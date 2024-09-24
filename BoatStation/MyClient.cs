using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatStation
{
    public class MyClient
    {
        public int ClientID { get; }
        public string Tlf { get; }
        public string Pasport { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public MyUser ClientUser { get; }

        public string TblClient { get { return $"{ClientID:0000}"; } }

        public MyClient() { }
        public MyClient(int id, string fnm, string snm, string tn, string pasp, MyUser mu)
        {
            ClientID = id;
            FirstName = fnm;
            SecondName = snm;
            Tlf = tn;
            Pasport = pasp;
            ClientUser = new MyUser(mu.ID, mu.Rule, mu.Name, mu.Email, mu.Password);
        }
    }
}
