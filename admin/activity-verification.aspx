<%@ Page Language="C#" MasterPageFile="~/admin/adminMasterPage.master"  AutoEventWireup="true" CodeFile="activity-verification.aspx.cs" Inherits="admin_activity_verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                 
       <!--BEGIN CONTENT-->
                <div class="page-content">
                    
                    <div id="tab-general">
                      <div runat="server" id="div_MainContent">
                        <div class="row mbl">
                            <div class="col-lg-4">
                                <div class="panel">
                                    <div class="box box-primary">
                                        <asp:Repeater ID="RepTab" runat="server" OnItemDataBound="RepTab_ItemDataBound">
                                        <ItemTemplate>
                                        <div class="profile">
                                            <div style="margin-bottom: 15px" class="row">
                                                <div class="col-xs-12 col-sm-8">
                                                    <h2><%#Eval("reg_name")%>
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
                                                        <strong class="mrs">Registered Medias: </strong><br />
                                                        <asp:Label ID="lblMedias" runat="server" />
                                                        <div id="divMedias" runat="server"></div> 
                                                   </p>     
                                                </div>
                                                <div class="col-xs-12 col-sm-4 text-center">
                                                    <figure><img src="<%#Eval("profile_image_url")%>" alt="" style="display: inline-block" class="img-responsive img-circle"/>
                                                    <figcaption><img src="<%=SessionState.WesiteImagesLoadURL%>socialmedia/1_verified_icon.png" title="facebook verified" />
                                                        <img src="<%=SessionState.WesiteImagesLoadURL%>socialmedia/2_verified_icon.png" title="twitter verified" /></figcaption>
                                                    <%--<figcaption class="ratings"><p><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star"></span></a><a href="#"><span class="fa fa-star-o"></span></a></p></figcaption>--%>
                                                </figure>
                                                </div>
                                            </div>
                                            <div class="row text-center divider">
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_pending") %></strong></h2>
                                                    <p>
                                                        <small>Activities</small>
                                                    </p>
                                                    <button class="btn btn-blue btn-block">
                                                        <span class="fa"></span>&nbsp; Pending
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_approved") %></strong></h2>
                                                    <p>
                                                        <small>Activities</small>
                                                    </p>
                                                    <button class="btn btn-blue btn-block">
                                                        <span class="fa"></span>&nbsp; Approved
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_rejected") %></strong></h2>
                                                    <p>
                                                        <small>Activties</small>
                                                    </p>
                                                    <button class="btn btn-orange btn-block">
                                                        <span class="fa"></span>&nbsp; Rejected
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_rewarded") %></strong></h2>
                                                    <p>
                                                        <small>Activties</small>
                                                    </p>
                                                    <button class="btn btn-yellow btn-block">
                                                        <span class="fa"></span>&nbsp; Rewarded
                                                    </button>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity") %></strong></h2>
                                                    <p>
                                                        <small>Activties</small>
                                                    </p>
                                                    <button class="btn btn-yellow btn-block">
                                                        <span class="fa"></span>&nbsp; Total
                                                    </button>
                                                </div>
                                            </div>
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
                                            Current Activity</div>
                                    </div>
                                    <div style="overflow: hidden;" class="portlet-body">
                                        <div class="form-body pal">
                                           <div class="form-group">
                                               <div class="row">
                                               
                                                   <asp:Repeater ID="RepTab_FBCurrentActivity" runat="server">
                                                <ItemTemplate>                                         
                                                   <div  class="col-md-12">
                                                    <img src="<%#Eval("sm_img_link") %>" /><br />
                                                    <%#Eval("sm_desc") %>&nbsp;<a href="<%#Eval("link") %>" target="_blank"><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/<%=current_media_id %>.png" style="width:20px;"/></a> <br /><br />
                                                    Created On: <%#Eval("created_on") %> &nbsp;&nbsp;Updated On: <%#Eval("updated_time") %> <br /><br />                                                                                                      
                                                    </div>
                                                    <div  class="col-md-8">
                                                    Reward: <span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %><br /></span>
                                                        <span ><h1><%#Eval("reward_amount") %> BP</h1>                                                                        
                                                                          </span>
                                                                        <span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />Default User: <%#Eval("reward_per_user") %> p</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />No. Of Friends: <%#Eval("no_of_friends") %>x<%#Eval("reward_on_friend") %> p</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />No. Of Likes: <%#Eval("no_of_likes") %>x<%#Eval("reward_on_likes") %> p</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br />No. Of Shares: <%#Eval("no_of_shares") %>x<%#Eval("reward_on_shares") %> p</span>
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
                                                    <p>Verification: <span class='verification_status_<%#Eval("verification_status") %>' ><%#Eval("verification_status_str")%></span></p>                                                    
                                                    <p><%#Eval("verification_log") %></p>
                                                    </div>
                                                    </div>
                                                    
                                                </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater ID="RepTab_GCurrentActivity" runat="server">
                                                <ItemTemplate>                                         
                                                   <div  class="col-md-12">
                                                    Performed On: <%#Eval("created_on") %> <br /><br />                                                                                                      
                                                    </div>
                                                    <div  class="col-md-8">
                                                    Reward: <span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %><br /></span>
                                                        <span ><h1><%#Eval("reward_amount") %> BP</h1>                                                                        
                                                                          </span>
                                                                        <span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />Default User: <%#Eval("reward_per_user") %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />No. Of Friends: <%#Eval("no_of_friends") %>x<%#Eval("reward_on_friend") %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />No. Of Likes: <%#Eval("no_of_likes") %>x<%#Eval("reward_on_likes") %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br />No. Of Shares: <%#Eval("no_of_shares") %>x<%#Eval("reward_on_shares") %> bp</span>
                                                                        </span>                                                                          
                                                    </div>
                                                    <div  class="col-md-4">
                                                    <div class="col-xs-12 col-sm-12">                                                                                                            
                                                        <button class="btn btn-orange btn-block">
                                                            <h2>
                                                            <strong><%#Eval("verification_score") %></strong></h2>
                                                            <span class="fa"></span>&nbsp; Verification Score
                                                        </button>
                                                    <p>&nbsp;</p>
                                                    <p>Verification: <span class='verification_status_<%#Eval("verification_status") %>' ><%#Eval("verification_status_str")%></span></p>                                                    
                                                    <p><%#Eval("verification_log") %></p>

                                                    </div>
                                                    </div>                                                    
                                                </ItemTemplate>
                                                </asp:Repeater>
                                                <div class="row" id="divApproveReject" runat="server">
                                                        <div  class="col-md-12"><br /><br />&nbsp;</div>
                                                        
                                                        <div class="col-xs-12 col-sm-4">                                                                                                       
                                                            <button class="btn btn-blue btn-block">
                                                                <span class="fa"></span>&nbsp;
                                                                <asp:LinkButton ID="lnkbtnApprove" runat="server" OnClick="lnkbtnApprove_Click" CommandArgument='<%=current_activity_id %>'>APPROVE</asp:LinkButton>                                                                 
                                                            </button>
                                                        </div>
                                                        <div class="col-xs-12 col-sm-4">                                                                                                       
                                                        <button class="btn btn-blue btn-block">
                                                            <span class="fa"></span>&nbsp; 
                                                            <asp:LinkButton ID="lnkbtnReject" runat="server" OnClick="lnkbtnReject_Click" CommandArgument='<%=current_activity_id %>'>REJECT</asp:LinkButton>
                                                        </button>
                                                        </div>                                                        
                                                    </div>
                                              </div> 
                                            </div>
                                        </div>  
                                        <div class="form-body pal">
                                           <div class="form-group">   
                                               <div  class="col-md-12">
                                                   <asp:Repeater ID="RepTab_CampaignDetails" runat="server">
                                                <ItemTemplate>                                         
                                                <div class="todo-title">
                                                    <strong>Campaign:</strong> <h3><%#Eval("campaign_name") %> <img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" style="width:50px;" /></h3><br /><br />                                                    
                                                    <strong>Objective:</strong> <%#Eval("campaign_objective_text") %> <br /><br /> 
                                                    <p>
                                                        <strong>Campaign Schedule:</strong> <%#Eval("campaign_execution_period") %></p>
                                                    <p>
                                                        Reward to : <strong><%#Eval("reward_whom_val")%></strong></p>                                                  
                                                </div>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                               </div> 
                                               <div class="row">
                                                        <div  class="col-md-12"><br /><br />&nbsp;</div>
                                                        <div class="col-xs-12 col-sm-4">                                                                                                       
                                                        <button class="btn btn-block">
                                                            <span class="fa"></span><asp:LinkButton runat="server" ID="btn_PrevActivity" OnClick="btn_PrevActivity_Click" CommandArgument="<%#SessionState._PaginationSession.start_row%>">Prev Activity</asp:LinkButton>
                                                        </button>
                                                    </div>
                                                        
                                                        <div class="col-xs-12 col-sm-4">                                                                                                       
                                                        <button class="btn btn-block">
                                                            <span class="fa"></span><asp:LinkButton runat="server" ID="btn_NextActivity" OnClick="btn_NextActivity_Click" CommandArgument="<%#SessionState._PaginationSession.start_row%>">Next Activity</asp:LinkButton>
                                                        </button>
                                                    </div>
                                                    </div>                                              
                                           </div>
                                        </div>                                    
                                       
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                        <div style="margin-bottom: 15px" class="row">
                                                <div class="panel panel-blue">
                                                    <div class="panel-heading">Previous Activities</div>
                                                    <div class="panel-body">
                                                        <table class="table table-hover">
                                                            <thead>
                                                            <tr>                       
                                                                  <th>Action</th>      
                                                                  <th>Brand</th>                                     
                                                                  <th>Performed On</th>
                                                                  <th>Activity Score</th>                                                                  
                                                                  <th>Reward</th>
                                                              </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="RepTab_PreviousActivities" runat="server">
                                                                <ItemTemplate>
                                                                  <tr style="display:<%#( Convert.ToInt64(Eval("activity_id"))==current_activity_id)?"none":""%>"> 
                                                                      <td><span><asp:LinkButton runat="server" ID="lnk_User" OnClick="lnk_User_Click" CommandArgument='<%#Eval("activity_id")%>'><%#Eval("campaign_type_desc") %></asp:LinkButton></span></td>
                                                                      <td><img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" style="width:50px;" /></td>
                                                                      <td><span class="time"><%#Eval("created_on") %></span></td>
                                                                      <td>
                                                                          <span><%#Eval("verification_score") %>&nbsp;&nbsp;</span><span class='verification_status_<%#Eval("verification_status") %>' ><%#Eval("verification_status_str")%></span>
                                                                          <span><br /><%#Eval("verification_log") %></span>
                                                                      </td>
                                                                      <td>
                                                                          <%#Eval("reward_amount") %> BP
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />Default User: <%#Eval("reward_per_user") %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />No. Of Friends: <%#Eval("no_of_friends") %>x<%#Eval("reward_on_friend") %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />No. Of Likes: <%#Eval("no_of_likes") %>x<%#Eval("reward_on_likes") %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br />No. Of Shares: <%#Eval("no_of_shares") %>x<%#Eval("reward_on_shares") %> bp</span>                                                                          
                                                                          
                                                                      </td>
                                                                      <td><span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %></span></td>
                                                                  </tr>
                                                                  </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                      </div>  
                      <div runat="server" id="div_NoContent" visible="false" class="row"><p><span>No activities pending for verification</span></p></div> 
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
        color:#fff
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
</style>            
      
      </ContentTemplate>
     
       </asp:UpdatePanel>
</asp:Content>

