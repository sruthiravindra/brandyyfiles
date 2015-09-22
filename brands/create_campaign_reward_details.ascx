<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_reward_details.ascx.cs" Inherits="brands_create_campaign_reward_details" %>

<div class="row" style="min-height:520px">
    <div class="col-md-12"">
        <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="box-body">                                      
                              
                               <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                            <strong>Max brandyy points per activity</strong> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Define what should be the max brandyy point that a user can by performing an activity"></i>
                                                        </label>                                                                                                  
                                           </div>
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:TextBox ID="TextBox1" runat="server" Columns="10"/> BP                                                             
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Specify max BP" ValidationGroup="save" ControlToValidate="txtMaxBrandyyPoints" InitialValue="" ForeColor="Red" />                                                        
                                            </div>                                                                                                
                                        </div>                                        
                                    </div>                                    
                                <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                            <strong>Set Brandyy Points breakup for a activity</strong> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Define what should be the max brandyy point that a user can by performing an activity"></i>
                                                        </label>                                                                                                  
                                           </div>
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">
                                                <table class="table">
                                                        <tr style="background-color:#fafafa">                                
                                                            <asp:Repeater runat="server" ID="repTab_header">
                                                                <ItemTemplate>                                                                    
                                                                    <th id="Th1" runat="server"  visible='<%#Eval("visiblestate") %>'> <span class="text-light-blue"> <%# Eval("header") %></span>                                                                    
                                                                    </th>
                                                                </ItemTemplate>                                                                        
                                                            </asp:Repeater>  
                                                        </tr>    
                                                        <tr>                                                      
                                                            <asp:Repeater runat="server" ID="repTab_content">
                                                                <ItemTemplate>                                                                    
                                                                    <td id="Td1" runat="server"  visible='<%#DataBinder.Eval(Container.DataItem, "col_visiblestate")%>' >
                                                                        <asp:TextBox ID="txtRewards" runat="server" Columns="10"  visible='<%#DataBinder.Eval(Container.DataItem, "visiblestate")%>' Text='<%#DataBinder.Eval(Container.DataItem, "data")%>' />                                                                                                                                        
                                                                    </td>
                                                                </ItemTemplate>                                                                        
                                                           </asp:Repeater>   
                                                        </tr>                          
                                                    </table>
                                            </div>
                                        </div>
                                    </div>
                               
                                <div>&nbsp;</div>                                
                                            <div class="form-actions text-right pal">                                           
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                           
                                           <%--<asp:Button runat="server" Text="Update Campaign" ValidationGroup="save" ID="btn_Update" OnClick="btn_Update_Click" CssClass="btn btn-primary" />                                                                                                                                                                              --%>
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>

                                       </div>
                                       </div> 
                                     </div>                                      
                                        
                                </ContentTemplate>
                              </asp:UpdatePanel>
    </div>
</div>


     <script src="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.css"%>" />
    
    <script type="text/javascript">
        function ShowOnLoadEvents() {           

            $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
                content: function (callback) {
                    callback($(this).prop('title').replace('|', '<br />'));
                },
                position: { my: "left top+15 center", at: "right center" }
            });            
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
         <script src='<%=SessionState.WebsiteURLAdmin+ "custom-js/Only-Numbers.js"%>' type="text/javascript"></script>
     <!-- validation notification end -->

     