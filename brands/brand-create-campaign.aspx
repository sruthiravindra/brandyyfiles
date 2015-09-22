<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="brand-create-campaign.aspx.cs" Inherits="brands_brand_create_campaign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <link href="<%=SessionState.WebsiteURLBrand+ "css/StyleSheet1.css"%>" rel="stylesheet" type="text/css" />
       <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                         Create Campaign                       
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li class="active">Create Campaign</li>                         
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Select One of the campaign objectives and create a corresponding campaign</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_1"><i class="fa fa-fw fa-question-circle"></i> Help: How can I create a campaign</a></li>                        
                    </ol>
                </section> 
         <asp:HiddenField runat="server" ID="current_step" Value="1" ClientIDMode="Static" />                
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-lg-12" style="">                                            
                            <div class="nav-tabs-custom">
                                
                                <ul class="nav nav-tabs">
                                    <li class="active" id="Tab1" runat="server">
                                     <asp:LinkButton runat="server" ID="lnk_Step1" ClientIDMode="Static" OnClientClick="SetPageUrl('1')" OnClick="lnk_Step1_Click">STEP I - Choose Platform</asp:LinkButton>                                           
                                    </li>
                                    <li id="Tab2" runat="server">
                                        <asp:LinkButton runat="server" ID="lnk_Step2" ClientIDMode="Static" OnClientClick="SetPageUrl('2')" CssClass="text-muted" OnClick="lnk_Step2_Click">STEP II - Choose Objective</asp:LinkButton>                                        
                                    </li>                                                                        
                                    <li id="Tab3" runat="server">
                                        <asp:LinkButton runat="server" ID="lnk_Step3" ClientIDMode="Static" OnClientClick="SetPageUrl('3')" CssClass="text-muted" OnClick="lnk_Step3_Click">STEP III - Basic Campaign Settings</asp:LinkButton>                                        
                                    </li> 
                                    <li id="Tab4" runat="server">
                                        <asp:LinkButton runat="server" ID="lnk_Step4" ClientIDMode="Static"  OnClientClick="SetPageUrl('4')" CssClass="text-muted" OnClick="lnk_Step4_Click">STEP IV - Reward Settings</asp:LinkButton>                                        
                                    </li> 
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                            <%--<div id="ucc1" runat="server"></div>   --%>
                                            <asp:MultiView ID="MainView" runat="server">
                                                <asp:View ID="View1" runat="server">
                                                    <div id="ucc1" runat="server"></div>                                                 
                                                </asp:View>
                                                <asp:View ID="View2" runat="server">
                                                    <div id="ucc2" runat="server"></div>                                                 
                                                </asp:View>
                                                <asp:View ID="View3" runat="server">
                                                    <div id="ucc3" runat="server"></div>                                                  
                                                </asp:View>
                                                <asp:View ID="View4" runat="server">
                                                    <div id="ucc4" runat="server"></div>                                                  
                                                </asp:View>
                                            </asp:MultiView>
                                     </div><!-- /.tab-pane -->                                    
                                </div><!-- /.tab-content -->
                            </div>
                            
                        </div>
                    </div>  
                                     
                </section><!-- /.content -->
            </ContentTemplate>     
       </asp:UpdatePanel>
     
<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
<script type="text/javascript">
    function ShowTbl(grouping) {        
    }    
</script>

<script type="text/javascript">
    function ShowOnLoadEvents() {        
        $("[id$=txtStartDate1]").datepicker({
        });
        $("[id$=txtEndDate1]").datepicker({
        });
        $("[id$=txtRewardWhenDate]").datepicker({});

        $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
            content: function (callback) {
                callback($(this).prop('title').replace('|', '<br />'));
            },
            position: { my: "left top+15 center", at: "right center" }
        });

        //SelectMultipleCountries();
    }
    $(window).load(function () {
        $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
            content: function (callback) {
                callback($(this).prop('title').replace('|', '<br />'));
            },
            position: { my: "left top+15 center", at: "right center" }
        });
    });
    </script>

     <script type="text/javascript">
         function setSession(grouping) {
             $("input[id=current_step]").val(2);
             PageMethods.SetSession(grouping);                          
             return false;
         }
         function setCamapignType(id, name) {
             $("input[id=current_step]").val(2);
             PageMethods.setCamapignType(id, name);             
             return false;

         }
         function setStep3() {
             //alert("here");
             $("input[id=current_step]").val(3);
             
            // $("#lnk_Step3")[0].click();
             return false;
         }
         function setStep4() {
             $("input[id=current_step]").val(4);;             
             //$("#lnk_Step4")[0].click();
             return false;
         }
         function setOnclickStep3() {
             //alert("here");
             $("input[id=current_step]").val(3);
             
             return false;
         }
         function SetPageUrl(id) {             
             if (parseInt(id) > parseInt($("input[id=current_step]").val())) return false;             
             $("input[id=current_step]").val(id);
             
         }
         
</script>
<style>
    .myTitleClass .ui-dialog-titlebar {
          background:#0073b7 !important;
          color:white;
          font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
          font-weight: normal;
          font-size: 20px;
    }
</style>
            
</asp:Content>
