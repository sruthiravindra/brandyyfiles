using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IchooseIT.DAL
{
    /// <summary>
    /// Summary description for Common
    /// </summary>
    public class Common
    {
        public string Message { set; get; }
        public bool IsSuccess { set; get; }
    }

    public class Actions_Reward_To
    {
        public string[] column_headers = new string[10];
        public string[] campaign_settings = new string[20];
        public string[] campaign_objective_settings = new string[20];

        public Actions_Reward_To()
        {
            string lbl = "Points";

            column_headers[1] = lbl + " (Per User)";
            column_headers[2] = lbl + " (Per no. of friends/followers)";
            column_headers[3] = lbl + " (Per no. of likes/fav)";
            column_headers[4] = lbl + " (Per no. of shares/retweet)";

            //column_headers[5] = lbl + " (Per no. of followers)";
            //column_headers[6] = lbl + " (Per no. of favourites)";
            //column_headers[7] = lbl + " (Per no. of friends who retweets)";


            campaign_settings[1] = "1,2";
            campaign_settings[2] = "1,2";
            campaign_settings[3] = "1,2,3,4";
            campaign_settings[4] = "1,2";
            campaign_settings[5] = "1,2,3,4";
            campaign_settings[6] = "1,2";
            campaign_settings[7] = "1";
            campaign_settings[8] = "1,2";
            campaign_settings[9] = "1,2,3,4";
            campaign_settings[10] = "1,2,3,4";
            campaign_settings[11] = "1";
            campaign_settings[13] = "1";
            campaign_settings[14] = "1";
            campaign_settings[15] = "1";
            campaign_settings[16] = "1";
            campaign_settings[17] = "1,2";
            campaign_settings[18] = "1,2";
            campaign_settings[19] = "1,2,3";

            //campaign_objective_settings[1] = "1,2";
            //campaign_objective_settings[2] = "1,2";
            //campaign_objective_settings[3] = "1,2,3,4";
            //campaign_objective_settings[5] = "1,2";
            //campaign_objective_settings[6] = "1";
            //campaign_objective_settings[4] = "1,2,3,4";
            //campaign_objective_settings[7] = "1,2,3,4";
            //campaign_objective_settings[8] = "1";
            //campaign_objective_settings[9] = "1,2";
            //campaign_objective_settings[10] = "1,2";


            campaign_objective_settings[1] = "1,2";
            campaign_objective_settings[2] = "1,2";
            campaign_objective_settings[3] = "1,2,3,4";
            campaign_objective_settings[4] = "1,2";
            campaign_objective_settings[5] = "1,2,3,4";
            campaign_objective_settings[6] = "1,2";
            campaign_objective_settings[7] = "1";
            campaign_objective_settings[8] = "1,2";
            campaign_objective_settings[9] = "1,2,3,4";
            campaign_objective_settings[10] = "1,2,3,4";
            campaign_objective_settings[11] = "1";
            campaign_objective_settings[13] = "1";
            campaign_objective_settings[14] = "1";
            campaign_objective_settings[15] = "1";
            campaign_objective_settings[16] = "1";
            campaign_objective_settings[17] = "1,2";
            campaign_objective_settings[18] = "1,2";
            campaign_objective_settings[19] = "1,2,3";

        }
        
    }
    public class mypost
    {
        public string post { get; set; }
        public string post_id { get; set; }
        public string created_on { get; set; }
        public string img_url { get; set; }
        public string post_url { get; set; }
    }
}