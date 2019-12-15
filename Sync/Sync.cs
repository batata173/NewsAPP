using System;
using System.Net.NetworkInformation;

namespace Sync
{
    class Sync
    {


        static void Main(string[] args)
        {
            
            //Verificar se API ON
            if(new PingS.UP().SiteUp())
            {
                int maxItem = 0;

                //Check Updates
                if (MaxItemChanged(ref maxItem))
                {
                    //MaxItemChanged GetNew Stories
                    GetStories(maxItem);

                    UpdateMaxItem(maxItem);
                }
            }

        }

        private static void UpdateMaxItem(int maxItem)
        {
            SantanderN.News.UpdateMaxItem(maxItem);
        }

        private static void GetStories(int maxItem)
        {
            //GetStories Until maxitem
            SantanderN.News.GetStories(maxItem);
        }

        private static Boolean MaxItemChanged(ref int maxItem)
        {
            return SantanderN.News.MaxChanged(ref maxItem);
        }
    }
}
