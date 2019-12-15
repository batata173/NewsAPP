using System.Net.NetworkInformation;

namespace PingS
{
    public class UP
    {
        public bool SiteUp()
        {
            Ping pingSender = new Ping();

            PingReply reply = pingSender.Send(System.Configuration.ConfigurationManager.AppSettings["URL"]);

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }

            return false;
        }
    }
}
