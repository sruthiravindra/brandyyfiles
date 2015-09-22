using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
public class PageProcessing
{
    public int ListPageNo { get; set; }
    public int MapPageNo { get; set; }
    public int ShowingOnPage { get { return 10; } }
    public int TotalRecordFound { get; set; }
    public int MapTotalRecordFound { get; set; }
    public int ShowingMapOnPage { get { return 30; } }
    public string ListOrMap { get; set; }
    public string QueryProcess { set; get; }
    public string OrderBy { set; get; }
    public string MapProcess { set; get; }
    public string MapLatLong { set; get; }
}
public class SelectKeyOnPage
{
    //public int CityKey { set; get; }
    //public int CategoryKey { set; get; }
    public List<string> PropFor { set; get; }
    public List<string> CityKey { set; get; }
    public List<string> CategoryKey { set; get; }
    public List<string> TypeKey { set; get; }
    public List<string> CommunityKey { set; get; }
    public List<string> SubCommunityKey { set; get; }
    public List<string> PropertyNameKey { set; get; }
    public List<string> BedroomKey { set; get; }
    public List<string> SalePriceKey { set; get; }
    public List<string> RentPriceKey { set; get; }
    public List<string> reg_uid { set; get; }
    public List<string> BathroomId { set; get; }
    public List<string> AreaId { set; get; }
    //public int BathroomKey { set; get; }

    //public int MinPrice { set; get; }
    //public int MaxPrice { set; get; }
    //public int MinArea { set; get; }
    //public int MaxArea { set; get; }
    //public int BathroomId { set; get; }
    //public int MaxBathroom { set; get; }

    public string Featured { set; get; }
    public int PriceAreaOrderBy { set; get; }
    public string AutoExtender { set; get; }

    public SelectKeyOnPage()
    {
        PropFor = new List<string>();
        CityKey = new List<string>();
        CategoryKey = new List<string>();
        TypeKey = new List<string>();
        CommunityKey = new List<string>();
        SubCommunityKey = new List<string>();
        PropertyNameKey = new List<string>();
        BedroomKey = new List<string>();
        SalePriceKey = new List<string>();
        RentPriceKey = new List<string>();
        reg_uid = new List<string>();
        BathroomId = new List<string>();
        AreaId = new List<string>();
    }
}

public class PageData
{
    public DataTable PropFor { set; get; }
    public DataTable DataCity { set; get; }
    public DataTable DataCategory { set; get; }
    public DataTable DataType { set; get; }
    public DataTable DataCommunity { set; get; }
    public DataTable DataSubCommunity { set; get; }
    public DataTable DataPropertyName { set; get; }
    public DataTable DataBedroom { set; get; }
    public DataTable AgentsBrokers { set; get; }
    public DataTable DataPrice { set; get; }
    public DataTable DataBathroom { set; get; }

}

public class PageSessionData
{
    public PageProcessing _PageProcessing = new PageProcessing();
    public SelectKeyOnPage _SelectKeyOnPage = new SelectKeyOnPage();
    public PageData _PageData = new PageData();


}

public class PaginationSession
{
    public Int64 start_row { get; set; }
    public Int64 end_row { get; set; }
}
