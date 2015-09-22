<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="brand-create-campaign-2.aspx.cs" Inherits="brands_brand_create_campaign_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    <div class="col-lg-9">
                      <div class="box box-primary">        
                          <div class="box-header">
                                    <h3 class="box-title">SET CAMPAIGN ACTIONS TO BE PERFORMED BY THE USER</h3>
                                </div><!-- /.box-header -->                            
                          <div style="overflow: hidden;" class="box-body">
                              <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>                                                                          
                                        <div class="form-body pal">
                                            <div class="form-group">
                                               <div id="ucc1" runat="server"></div>                                               
                                           </div>                                                 
                                       </div>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                          </div>
                        </div>
                  </div>
                            </div>                    
                </section><!-- /.content -->
           
      <!--BEGIN CONTENT-->
        
     <!--END CONTENT-->      
     

     <script src="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.css"%>" />
    
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
     <!-- validation notification start -->
     <script type="text/javascript">
         function fnOnUpdateValidators() {
             for (var i = 0; i < Page_Validators.length; i++) {
                 var val = Page_Validators[i];
                 var ctrl = document.getElementById(val.controltovalidate);
                 if (ctrl != null && ctrl.style != null) {
                     if (!val.isvalid)
                         ctrl.style.background = '#FFAAAA';
                     else
                         ctrl.style.backgroundColor = '';
                 }
             }
         }
         function HideStatusNotification() {

             $('.my_status_notification').fadeOut(5000, function () {
                 $(this).html(""); //reset label after fadeout
             });

         }

     </script>
     <style type="text/css">
    .my_status_notification
    {
        box-shadow: 10px 10px 5px #888888;
    }
         </style>
     <!-- validation notification end -->

     
</asp:Content>