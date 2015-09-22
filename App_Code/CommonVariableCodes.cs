using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonVariableCodes
/// </summary>
public class CommonVariableCodes
{
    public int campaign_status_active;
    public int campaign_status_inactive;

    public int verification_status_active;
    public int verification_status_inactive;

    public int schedule_type_daily;
    public int schedule_type_periodic;

    public Int32 brandyy_brand_id;
    public Int64 brandyy_campaign_id;
    public Int64 brandyy_action_id_fb;
    public Int64 brandyy_action_id_tw;
    public Int64 brandyy_action_id_insta;

	public CommonVariableCodes()
	{
        campaign_status_active = 1;
        campaign_status_inactive = 0;

        verification_status_active = 1;
        verification_status_inactive = 0;

        schedule_type_daily = 1;
        schedule_type_periodic = 2;


        brandyy_brand_id = 3;
        brandyy_campaign_id = 2;
        brandyy_action_id_fb = 4;
        brandyy_action_id_tw = 5;
        brandyy_action_id_insta = 6;

	}
}
