<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="brand-create-campaign-3ii.aspx.cs" Inherits="brands_brand_create_campaign_3ii" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">     
     <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    <div class="col-lg-9">
                      <div class="box box-primary">        
                          <div class="box-header">
                                    <h3 class="box-title">SET ACTION POINTS</h3>
                                </div><!-- /.box-header -->                            
                          <div style="overflow: hidden;" class="box-body">                                           
                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>                                                                       
                                                    <table class="table table-hover">
                                                        <tr style="background-color:#fafafa">
                                                            <th>
                                                                &nbsp;
                                                            </th>
                                                            <th>
                                                                <span class="text-light-blue"> Selected Action(s)</span>
                                                             
                                                            </th>                                                            
                                                            <asp:Repeater runat="server" ID="repTab_header">
                                                                <ItemTemplate>                                                                    
                                                                    <th runat="server"  visible='<%#Eval("visiblestate") %>'> <span class="text-light-blue"> <%# Eval("header") %></span>
                                                                    
                                                                    </th>
                                                                </ItemTemplate>                                                                        
                                                            </asp:Repeater>  
                                                        </tr>                                                         
                                                        <asp:Repeater runat="server" ID="repTab_ActionNames" OnItemDataBound="repTab_ActionNames_ItemDataBound">
                                                            <ItemTemplate>
                                                        <tr>
                                                            <td><asp:CheckBox ID="chk_Action" Checked="true" Visible="false" runat="server" OnCheckedChanged="chk_Action_CheckedChanged" />
                                                                <asp:HiddenField ID="hdn_CampaignType" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "id")%>'/>
                                                            </td>
                                                            <td>
                                                                <img src='<%=SessionState.WebsiteURLAdmin+ "images/campaign_types/campaign_type_"%><%#DataBinder.Eval(Container.DataItem, "id")%>.png' />
                                                                <br />
                                                                <%#DataBinder.Eval(Container.DataItem, "action")%>
                                                                
                                                            </td>                                                            
                                                            <asp:Repeater runat="server" ID="repTab_content">
                                                                <ItemTemplate>                                                                    
                                                                    <td runat="server"  visible='<%#DataBinder.Eval(Container.DataItem, "col_visiblestate")%>' >
                                                                        <asp:TextBox ID="txtRewards" runat="server" Columns="10"  visible='<%#DataBinder.Eval(Container.DataItem, "visiblestate")%>' OnTextChanged="txtRewards_TextChanged" Text='<%#DataBinder.Eval(Container.DataItem, "data")%>' />                                                                                                                                        
                                                                    </td>
                                                                </ItemTemplate>                                                                        
                                                           </asp:Repeater>                                                             
                                                            
                                                        </tr>             
                                                            </ItemTemplate>
                                                        </asp:Repeater>                              
                                                    </table>                                                                       
                                                                <div>&nbsp;</div>   
                                     <div runat="server" id="divAllowOnePost" style="overflow: hidden;" class="portlet-body pan" visible="false" >
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:CheckBox runat="server" ID="chkAllowMoreThanOnePost" /><label for="txtReward_1" class="control-label">
             &nbsp; <span class="text-light-blue">Reward user for one post only</span>* <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="User may add more than one posts/tweet for the same campaign. If the checkbox is selected user will be rewarded only for one/first post that he/she adds"></i></label> 
                                            </div>                                                    
                                                                                        
                                        </div>                                        
                                    </div>    
                                    <div runat="server" id="divShowPoints" visible="false">
                                    <div class="portlet-header">
                                        <div class="caption"> <span class="text-light-blue">Do you want the points calculation to be displayed to user on 'Offers' Page</span></div>
                                    </div>
                                     <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:DropDownList ID="drpDisplayRewardDetails" runat="server">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                            </div>                                                                                                                                          
                                        </div>                                        
                                    </div>    
                                        </div>     
                                    <div>
                                    <div class="portlet-header">
                                        <div class="caption"> <span class="text-light-blue">Is it compulsory for user to perform all actions <br />(applicalbe when campaign has multiple actions)</span></div>
                                    </div>
                                     <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:DropDownList ID="drpAllActionsComp" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>                                                            
                                                        </asp:DropDownList>
                                            </div>                                                                                                                                          
                                        </div>                                        
                                    </div>    
                                        </div>                                                                  
                                            </ContentTemplate>
                                                        </asp:UpdatePanel> 
                                            
                                                    <div class="form-group">
                                                        <div class="col-md-offset-3 col-md-8">
                                                            <asp:Label runat="server" ID="lbl_RewardError" ForeColor="Red" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">&nbsp;</div>
                                                                                 
                                                    <div class="form-actions text-right pal">                                                            
                                                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Back" ID="btnBack" OnClick="btnBack_Click" />                                                            
                                                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Update & Next" ID="btn_Update" OnClick="btn_Update_Click"/>                                                                                                                        
                                                    </div>   
                                        </div>
                        </div>
                  </div>
                            </div>                    
                </section><!-- /.content -->
           
      <!--BEGIN CONTENT-->
        
     <!--END CONTENT-->      
     <script src="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.css"%>" />
    <script type="text/javascript">
         $(window).load(function () {
             $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip();
         });
    </script>
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
       
         $(document).ready(function () {             
             $('.waitstatus').fadeOut(5000, function () {
                 $(this).html(""); //reset label after fadeout
             });
         });

     </script>
     <style type="text/css">
.my_status_notification
{
    background-color:#006699;
    min-height:40px;
    width:30%;
    margin:0 auto;
    text-align:center;
    line-height:50px;
    color:#fff;
    font-size:18px;
    box-shadow: 10px 10px 5px #888888;
}
     </style>
</asp:Content>
