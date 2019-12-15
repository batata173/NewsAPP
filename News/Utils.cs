using System;
using System.Collections.Generic;
using System.Linq;

namespace SantanderN
{
    public class News
    {

        public string by { get; set; }
        public int descendants { get; set; }
        public int id { get; set; }
        //public string kids { get; set; }
        public int score { get; set; }
        public string time { get; set; }
        public string title { get; set; }
        public string type { get; set; }

        

        public string url { get; set; }

        public static bool MaxChanged(ref int onMax)
        {
            onMax = GetCurrentMax();
            int offMax = GetLocalMax();

            if(onMax!= offMax)
            return true;

            return false;
        }

        private static int GetLocalMax()
        {
            return Convert.ToInt32(new DB().GetData(System.Configuration.ConfigurationManager.AppSettings["ItemMax"]));
        }

        public static int GetCurrentMax()
        {
            return Convert.ToInt32(new API().Invoke(System.Configuration.ConfigurationManager.AppSettings["URLBase"],
                                                    System.Configuration.ConfigurationManager.AppSettings["URLMaxItemID"]));
        }

        public static void UpdateMaxItem(int maxItem)
        {
            new DB().SetDataMaxItem(System.Configuration.ConfigurationManager.AppSettings["ItemMax"], 
                                    maxItem);
        }

        public static void GetStories(int maxItem)
        {
            List<string> newStories = new API().InvokeGetStories(System.Configuration.ConfigurationManager.AppSettings["URLBase"],
                                                    System.Configuration.ConfigurationManager.AppSettings["URLNewStories"]);

            new API().GetDetailFromStories(newStories, 
                                            System.Configuration.ConfigurationManager.AppSettings["URLBase"],
                                            System.Configuration.ConfigurationManager.AppSettings["URLDetail"]);


        }

        public static List<News> GetTopStories()
        {
            List<string> bestStories = new API().InvokeGetStories(System.Configuration.ConfigurationManager.AppSettings["URLBase"],
                                                                System.Configuration.ConfigurationManager.AppSettings["URLTopStories"]);

            

            return GetDetailFromTopStories(bestStories);


        }

        private static List<News> GetDetailFromTopStories(List<string> bestStories)
        {
            List<SantanderN.News> news = new List<News>();
            foreach (var bs in bestStories)
            {
                if (new DB().StoryExists(bs))
                    news.Add(StoryFromFile(bs));
                else
                    news.Add(StoryFromOn(bs));
            }

            return Filter(news);
        }

        private static List<News> Filter(List<News> news)
        {
            return news.OrderByDescending(x => x.score).Take(20).ToList();


        }

        private static News StoryFromOn(string bs)
        {
            News n = new API().GetStory(System.Configuration.ConfigurationManager.AppSettings["URLBase"],
                                System.Configuration.ConfigurationManager.AppSettings["URLDetail"], 
                                bs);

            new DB().SaveNews(n);

            return n;
        }

        private static News StoryFromFile(string bs)
        {
            return new DB().StoryFromFile(bs);
        }
    }

}
