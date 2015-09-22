<%@ Page Language="C#" MasterPageFile="~/brands/brandsMasterPage.master" AutoEventWireup="true" CodeFile="activity-listing.aspx.cs" Inherits="brands_activity_listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                 
       <!--BEGIN CONTENT-->
                <div class="page-content">
                    
                    <div id="tab-general">
                        <div class="row mbl">
                            <div class="col-lg-4">
                                <div class="panel">
                                    <div class="box box-primary" style="margin-bottom:0px;">
                                        <asp:Repeater ID="RepTab" runat="server" OnItemDataBound="RepTab_ItemDataBound">
                                        <ItemTemplate>
                                        <div class="profile" style="margin-left:10px;">
                                            <div style="margin-bottom: 15px" class="row">
                                                <div class="col-xs-12 col-sm-8">
                                                    <h2 style="font-size: 25px;"><%#Eval("reg_name")%>
                                                        </h2>
                                                    <p>
                                                        <strong></strong></p>
                                                    <p>
                                                        <strong><a href="#"><%#Eval("reg_email")%> </a></strong>
                                                    </p>
                                                    <%--<p>
                                                        <strong>Registered With: <img alt="" src="<%=SessionState.WesiteImagesLoadURL%>img/socialmedia/<%#Eval("register_with")%>.png"><a class="link" href="<%#Eval("register_with")%>"></a></strong>
                                                    </p>--%>
                                                    <p>
                                                        <strong class="mrs">Registered Medias : </strong><br />
                                                        <asp:Label ID="lblMedias" runat="server" />
                                                        <div id="divMedias" runat="server"></div> 
                                                   </p>     
                                                </div>
                                                <div class="col-xs-12 col-sm-4 text-center">
                                                    <figure><img src="<%#Eval("profile_image_url")%>" alt="" style="display: inline-block;margin-top:10px; margin-left:-10px;" class="img-responsive img-circle"/>
                                                    <figcaption><img src="<%=SessionState.WesiteImagesLoadURL%>socialmedia/1_verified_icon.png" title="facebook verified" />
                                                        <img src="<%=SessionState.WesiteImagesLoadURL%>socialmedia/2_verified_icon.png" title="twitter verified" /></figcaption>
                                                    <%--<figcaption class="ratings"><p><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star-o"></span></a></p></figcaption>--%>
                                                </figure>
                                                </div>
                                            </div>
                                            <div class="row text-center divider" style="margin-right:-5px;">
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_pending") %></strong></h2>
                                                    <p>
                                                        <small>Activities</small>
                                                    </p>
                                                    <button class="btn btn-warning btn-sm">
                                                        <span class="fa"></span>&nbsp; Pending
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_approved") %></strong></h2>
                                                    <p>
                                                        <small>Activities</small>
                                                    </p>
                                                    <button class="btn btn-success btn-sm">
                                                        <span class="fa"></span>&nbsp; Approved
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_rejected") %></strong></h2>
                                                    <p>
                                                        <small>Activties</small>
                                                    </p>
                                                    <button class="btn btn-danger btn-sm">
                                                        <span class="fa"></span>&nbsp; Rejected
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_rewarded") %></strong></h2>
                                                    <p>
                                                        <small>Activties</small>
                                                    </p>
                                                    <button class="btn btn-primary btn-sm">
                                                        <span class="fa"></span>&nbsp; Rewarded
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity") %></strong></h2>
                                                    <p>
                                                        <small>Activties</small>
                                                    </p>
                                                    <button class="btn btn-info btn-sm">
                                                        <span class="fa"></span>&nbsp;&nbsp;&nbsp;&nbsp; Total &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </button>
                                                </div>
                                            </div>
                                                                                               <br />
                                        </div>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                
                                <div class="box box-primary">
                                    <div class="portlet-header">
                                        <div class="caption">
                                            <h3 style="margin-left:10px;">Current Activity</h3></div>
                                    </div>
                                    <div style="overflow: hidden;margin-left:10px" class="portlet-body" >
                                        <div class="form-body pal">
                                           <div class="form-group">                                               
                                               <div class="row">                                               
                                                   <asp:Repeater ID="RepTab_FBCurrentActivity" runat="server">
                                                <ItemTemplate>                                         
                                                   <div  class="col-md-12">
                                                    <img src="<%#Eval("sm_img_link") %>" /><br />
                                                    <%#Eval("sm_desc") %>&nbsp;<a href="<%#Eval("link") %>" target="_blank"><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/<%=current_media_id %>.png" style="width:20px;"/></a> <br /><br />
                                                      <span class="text-light-blue">Created On :</span> <%#Eval("created_on") %> <br />  <span class="text-light-blue">Updated On : </span><%#Eval("updated_time") %> <br /><br />                                                                                                      
                                                    </div>
                                                    <div  class="col-md-8">
                                                      <span class="text-light-blue">Reward : </span><small class="label label-<%#( (Convert.ToByte( Eval("reward_status") )== 1) ? "success" : ( (Convert.ToByte( Eval("reward_status") )== 3) ? "danger" : "" ) ) %>"> <%#Eval("reward_status_str") %></small>
                                                        <span ><h1><%# Convert.ToInt64(Eval("reward_amount")) %> BP</h1>                                                                        
                                                                          </span>
                                                                        <span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />  <span class="text-light-blue">Default User :</span> <%# Convert.ToInt64(Eval("reward_per_user")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />  <span class="text-light-blue">No. Of Friends : </span> <%#Convert.ToInt64(Eval("no_of_friends")) %> x <%#Convert.ToInt64(Eval("reward_on_friend")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />  <span class="text-light-blue">No. Of Likes : </span><%#Convert.ToInt64(Eval("no_of_likes")) %> x <%#Convert.ToInt64(Eval("reward_on_likes"))%> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br />  <span class="text-light-blue">No. Of Shares :</span> <%#Convert.ToInt64(Eval("no_of_shares")) %> x <%#Convert.ToInt64(Eval("reward_on_shares"))%> bp</span>
                                                                        </span>                                                                          
                                                    </div>
                                                    <div  class="col-md-4">
                                                    <div class="col-xs-12 col-sm-12">                                                                                                            
                                                        <button class="btn btn-orange btn-block">
                                                            <h2>
                                                            <strong><%#Eval("verification_score") %>/10</strong></h2>
                                                            <span class="fa"></span>&nbsp; Verification Score
                                                        </button>
                                                        <p>&nbsp;</p>
                                                    <p>  <span class="text-light-blue">Verification : </span><small class="label label-<%#(Convert.ToByte( Eval("verification_status") )== 1) ? "info" : "danger" %>"> <%#Eval("verification_status_str")%></small></p>                                                    
                                                    <p><%#Eval("verification_log") %></p>

                                                    </div>
                                                    </div>                                                    
                                                </ItemTemplate>
                                                </asp:Repeater>
                                                   <asp:Repeater ID="RepTab_GCurrentActivity" runat="server">
                                                <ItemTemplate>                                         
                                                   <div  class="col-md-12">
                                                     <span class="text-light-blue"> Performed On : </span><%#Eval("created_on") %> <br /><br />                                                                                                      
                                                    </div>
                                                    <div  class="col-md-8">
                                                      <span class="text-light-blue"> Reward : </span><small class="label label-<%#( (Convert.ToByte( Eval("reward_status") )== 1) ? "success" : ( (Convert.ToByte( Eval("reward_status") )== 3) ? "danger" : "" ) ) %>"> <%#Eval("reward_status_str") %></small>
                                                        
                                                        <span ><h1><%# Convert.ToInt64(Eval("reward_amount")) %> BP</h1>                                                                        
                                                                          </span>
                                                                        <span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />  <span class="text-light-blue"> Default User : </span><%#Convert.ToInt64(Eval("reward_per_user")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />  <span class="text-light-blue"> No. Of Friends : </span><%#Convert.ToInt64(Eval("no_of_friends")) %> x <%#Convert.ToInt64(Eval("reward_on_friend")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />  <span class="text-light-blue"> No. Of Likes : </span><%#Convert.ToInt64(Eval("no_of_likes")) %> x <%#Convert.ToInt64(Eval("reward_on_likes")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br />  <span class="text-light-blue"> No. Of Shares : </span><%#Convert.ToInt64(Eval("no_of_shares")) %> x <%#Convert.ToInt64(Eval("reward_on_shares")) %> bp</span>
                                                                        </span>                                                                          
                                                    </div>
                                                    <div  class="col-md-4">
                                                    <div class="col-xs-12 col-sm-12">                                                                                                            
                                                        <button class="btn btn-orange btn-block">
                                                            <h2>
                                                            <strong><%#Eval("verification_score") %>/10</strong></h2>
                                                            <span class="fa"></span>&nbsp; Verification Score
                                                        </button>
                                                    <p>&nbsp;</p>
                                                    <p>  <span class="text-light-blue"> Verification : </span><small class="label label-<%#(Convert.ToByte( Eval("verification_status") )== 1) ? "info" : "danger" %>"> <%#Eval("verification_status_str")%></small></p>                                                    
                                                    <p><%#Eval("verification_log") %></p>

                                                    </div>
                                                    </div>                                                    
                                                </ItemTemplate>
                                                </asp:Repeater>
                                              </div> 
                                            </div>
                                        </div>  
                                        <div class="form-body pal">
                                           <div class="form-group">   
                                               <div  class="col-md-12" style="padding-left:0px;">
                                                   <asp:Repeater ID="RepTab_CampaignDetails" runat="server">
                                                <ItemTemplate>                                         
                                                <div class="todo-title">
                                                    <span class="label label-danger" style="<%#(Convert.ToByte(Eval("campaign_status")) == 1 ? "display:none" : "")%>">Not Active</span>
                                                        <strong>  <span class="text-light-blue">Campaign :</span></strong> <h3><%#Eval("campaign_name") %> <img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" style="width:50px;" /></h3><br /><br />                                                    
                                                    <strong>  <span class="text-light-blue">Objective :</span></strong> <%#Eval("campaign_objective_text") %> <br /><br /> 
                                                    <p>
                                                        <strong>  <span class="text-light-blue">Campaign Schedule :</span></strong> <%#Eval("campaign_execution_period") %></p>
                                                    <p>
                                                          <span class="text-light-blue">Reward to :</span> <strong><%#Eval("reward_whom_val")%></strong></p>                                                                                                        
                                                </div>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                               </div>                                               
                                           </div>
                                        </div>                                    
                                       
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                        <div style="margin-bottom: 15px" class="row">
                                                <div class="panel panel-blue">
                                                    <div class="panel-heading"><h4>  <span class="text-green">Previous Activities</span></h4></div>
                                                    <div class="panel-body">
                                                        
                                                        <table class="table table-hover">
                                                            <thead>
                                                            <tr>    
                                                                  <th>Action</th> 
                                                                  <th>Activity Score</th>                                                                  
                                                                  <th>Reward</th>
                                                              </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepTab_PreviousActivities" runat="server">
                                                                <ItemTemplate>
                                                                    <tr style="display:<%#( Convert.ToInt64(Eval("activity_id"))==current_activity_id)?"none":""%>">
                                                                      <td>
                                                                          <div class="box <%#( Convert.ToByte( Eval("verification_status") ) == 3 ) ? "box-danger" : ( Convert.ToByte( Eval("verification_status") ) == 2 ) ? "box-success" : "box-info" %>">
                                                                    <div class="box-body chat" id="chat-box" style="overflow: hidden; width: auto;">
                                                                    <!-- chat item -->
                                                                    <div class="item">
                                                                        <img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" alt="user image" class="online">
                                                                        <p class="message">
                                                                            <asp:LinkButton runat="server" CssClass="name" ID="lnk_User" OnClick="lnk_User_Click" CommandArgument='<%#Eval("activity_id")%>'>
                                                                                <small class="text-muted pull-right"><i class="fa fa-clock-o"></i> <%#Eval("created_on") %></small>
                                                                                <%#Eval("name") %>
                                                                            </asp:LinkButton>        
                                                                            <%#Eval("campaign_type_desc") %>
                                                                        </p>                                         
                                        
                                                                    </div><!-- /.item -->                                    
                                                                </div>
                                                            </div>
                                                                      </td>                                                                      
                                                                      <td>
                                                                          <small class="label label-default"> <%#Eval("verification_score") %> / 10</small><br />
                                                                          <small class="label label-<%#(Convert.ToByte( Eval("verification_status") )== 1) ? "info" : "danger" %>"> <%#Eval("verification_status_str")%></small><br />
                                                                          <small class="label label-danger" style="display:<%#( Convert.ToString( Eval("verification_log") ).Trim() == "" ) ? "none" : "" %>"> <%#Eval("verification_log") %></small><br />                                                                          
                                                                      </td>
                                                                      <td>
                                                                          <small class="label label-default"> <%#Convert.ToInt64(Eval("reward_amount")) %> BP</small><br />
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />  <span class="text-light-blue">Default User :</span> <%#Convert.ToInt64(Eval("reward_per_user")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />  <span class="text-light-blue">No. Of Friends : </span><%#Convert.ToInt64(Eval("no_of_friends")) %> x <%#Convert.ToInt64(Eval("reward_on_friend"))%> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />  <span class="text-light-blue">No. Of Likes :</span> <%#Convert.ToInt64(Eval("no_of_likes")) %> x <%#Convert.ToInt64(Eval("reward_on_likes")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br />  <span class="text-light-blue">No. Of Shares :</span> <%#Convert.ToInt64(Eval("no_of_shares")) %> x <%#Convert.ToInt64(Eval("reward_on_shares")) %> bp</span>
                                                                          
                                                                          <small class="label label-<%#( (Convert.ToByte( Eval("reward_status") )== 1) ? "success" : ( (Convert.ToByte( Eval("reward_status") )== 3) ? "danger" : "" ) ) %>"> <%#Eval("reward_status_str") %></small>                                                                          
                                                                      </td>                                                                      
                                                                  </tr>                                                                  
                                                                  </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                    </div>
                            
                </div>
                <!--END CONTENT-->                          
               
 <style type="text/css">
    .verification_status_0 {
        background-color:#dc6767;color:#fff
    }
    .verification_status_1 {
        background-color:#f2994b;color:#fff
    }
    .verification_status_2 {
        background-color:#5cb85c;color:#fff        
    }
    .verification_status_3 {
        background-color:#d9534f;color:#fff
    } 
    .reward_status_ {
        ;color:#fff
    }
    .reward_status_0 {
        color:#fff
    }     
    .reward_status_1 {
        background-color:#5cb85c;color:#fff     
    }
    .reward_status_2 {
        background-color:#f2994b;color:#fff             
    }
    .reward_status_3 {
       
        background-color:#dc6767;color:#fff
    }
    img[src=""] {
  display:none;
}
</style>       
                
                <script type="text/javascript" src="<%=SessionState.WebsiteURLBrand+ "script/jquery-1.10.2.min.js"%>"></script>     
                <script type="text/javascript" src="<%=SessionState.WebsiteURLBrand+ "script/jquery-ui.js"%>"></script>     
<script type="text/javascript">
                    $(window).load(function () {
                        $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip();
                    });
</script>
                
      </ContentTemplate>
     
       </asp:UpdatePanel>
</asp:Content>

 