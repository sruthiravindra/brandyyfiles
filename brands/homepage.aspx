<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master"  CodeFile="homepage.aspx.cs" Inherits="brands_homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content" style="margin-top:20px;">   
                    <div class="row">
                        <section class="col-lg-6 connectedSortable"> 
                            <!-- Active Campaigns -->
                            <div class="box box-success" style="height: 314px;">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-fw fa-star"></i> Active Campaigns</h3>  
                                    <span class="pull-right"><i class="fa fa-info-circle" data-toggle="tooltip" data-placement="left" title="Listing of all Active campaigns. Click on the campaign name to view its summary"></i>&nbsp;&nbsp;</span>                                    
                                </div>
                                <div class="box-body chat" id="divActiveCamapigns">
                                    
                                </div>
                                <!-- /.chat -->
                                
                            </div><!-- /.box (latest updates) -->     
                    <!-- latest campaign activities-->
                            <div class="box box-danger" style="height: 314px;">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-fw fa-list-alt"></i> Notifications</h3>                                    
                                    <span class="pull-right"><i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Notifications - To help you perform priority actions."></i>&nbsp;&nbsp;</span>
                                </div>
                                <div class="box-body chat" id="divNotifications">
                                    
                                </div>
                                <!-- /.chat -->
                                
                            </div><!-- /.box (latest campaign activities) -->    
                            
                                  
                            </section>
                        <section class="col-lg-6 connectedSortable"> 
                             <!-- latest transactions-->
                            <div class="box box-warning" style="height: 314px;">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-clipboard"></i> Latest Transactions</h3>   
                                    <span class="pull-right"><i class="fa fa-info-circle" data-toggle="tooltip" data-placement="left" title="5 Latest brandyy point and cash transactions that were performed in the system.Click on 'Latest Transactions' to view all transactions"></i>&nbsp;&nbsp;</span>                                 
                                </div>
                                <div class="box-body chat" id="divTransactions">
                                    
                                </div>
                                <!-- /.chat -->
                                
                            </div><!-- /.box (latest transactions) -->      
                            <!-- latest updates -->
                            <div class="box box-info" style="height: 314px;">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-fw fa-edit"></i> Latest Updates</h3>                                    
                                    <span class="pull-right"><i class="fa fa-info-circle" data-toggle="tooltip" data-placement="left" title="5 Latest activity that was performed across the brand."></i>&nbsp;&nbsp;</span>                                 
                                </div>
                                <div class="box-body chat" id="chat-box">
                                    
                                </div>
                                <!-- /.chat -->
                                
                            </div><!-- /.box (latest updates) -->   
                        </section>
                    </div>                      
                                              <div id="dialog" style="display:none;margin-top:10px;" >
                                                   <img src='<%=SessionState.WebsiteURLBrand + "img/ad-close-icon.png" %>'  onclick="closePopup()" style="  margin-left: 915px;
  margin-top: 15px;
  overflow: auto;
  position: absolute;cursor:pointer"/>
<iframe frameborder="0" scrolling="no" width="950" height="390" src='<%=SessionState.WebsiteURLBrand + "Default4.aspx" %>' id="ifrmLoad">
</iframe>
</div>

<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
<script>
    $(document).ready(function () {
        var id = '<%= SessionState._BrandAdmin.steps %>';
        var count = '<%= SessionState._BrandAdmin.callCount %>';
        if (id != "No" && count <= 1 ) {
            $("#dialog").dialog("open");
        }
    })
    $( "#dialog_trigger" ).click(function() {
        $( "#dialog" ).dialog( "open" );
    });
    $("#dialog").dialog({
        autoOpen: false,
        position: 'center' ,
        title: 'Activate',
        draggable: false,
        width : 990,
        height : 433, 
        resizable : true,
        modal: true,
    });
    //$(".ui-dialog-title").hide();
    //$(".ui-widget-header").hide();
    $(".ui-dialog-titlebar").hide();
    </script>
 
  </div>
                </section>               
             </ContentTemplate>     
       </asp:UpdatePanel>
     <script src="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.css"%>" />
<script type="text/javascript">

    $(window).load(function () {
        $.ajax({
            url: 'ajax/dashboard-active-campaigns.aspx',
            type: 'GET'
        }).done(function (data) {
            $("#divActiveCamapigns").html(data);
        });

    });


    $(window).load(function () {
        $.ajax({
            url: 'ajax/dashboard-notifications.aspx',
            type: 'GET'
        }).done(function (data) {
            $("#divNotifications").html(data);
        });

    });
    $(window).load(function () {
        $.ajax({
            url: 'ajax/dashboard-brand-profile-trans.aspx',
            type: 'GET'
        }).done(function (data) {
            $("#chat-box").html(data);
        });
        
    });

    $(window).load(function () {
        $.ajax({
            url: 'ajax/dashboard-brand-purchase-trans.aspx',
            type: 'GET'
        }).done(function (data) {
            $("#divTransactions").html(data);
        });

    });



    function SetBrandProfileTrans(response) {
        alert(reponse);
    }

    function LoadCampaignView(pCampaignID) {        
        PageMethods.LoadCampaignView(pCampaignID, LoadCampaignViewLoginSuccess);
    }
    function LoadCampaignViewLoginSuccess(response) {        
        parent.window.location.href = response;
    }
</script>
     <script type="text/javascript">
         function closePopup() {
             $("#dialog").dialog("close");
             $("#dialog")._super();
             window.location.href = '<%=SessionState.WebsiteURLBrand+ "socialmedias.aspx"%>';
         }
</script>
 
</asp:Content>
