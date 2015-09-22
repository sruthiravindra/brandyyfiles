<%@ Page Language="C#" MasterPageFile="~/brands/brandsMasterPage.master"  AutoEventWireup="true" CodeFile="activity-verification.aspx.cs" Inherits="admin_activity_verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Verify Activity                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li><a href="<%=SessionState.WebsiteURLBrand %>activity-verification-overview.aspx"> Activity Verification Overview</a></li> 
                        <li><a href="<%=SessionState.WebsiteURLBrand %>activity-verification-list.aspx"> Activity Verification List</a></li> 
                        <li class="active">Verify Activity</li>                        
                    </ol>
                </section> 
    <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        <a href='<%= SessionState.WebsiteURLBrand + "activity-verification-list.aspx" %>' class="btn btn-primary" style="display:<%=(SessionState.EditId == 0) ? "none" : "" %>">Back</a>
                        <a href='<%= SessionState.WebsiteURLBrand + "activity-verification.aspx" %>' class="btn btn-primary" style="display:<%=(SessionState.EditId == 0) ? "" : "none" %>">Pause</a>
                        &nbsp;&nbsp;<a href='<%= SessionState.WebsiteURLBrand + "activity-verification-list.aspx" %>' class="btn btn-primary" style="display:<%=(SessionState.EditId == 0) ? "" : "none" %>" >Stop</a>                        
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_0"><i class="fa fa-fw fa-question-circle"></i> Help: Verifying Campaign Activities</a></li>                        
                    </ol>
                </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>  
                <!-- Main content -->
                <section class="content">                  
       <!--BEGIN CONTENT-->
                <div>
                    <div class="page-content">
                    <div id="tab-general">
                      <div runat="server" id="div_MainContent">
                        <div class="row mbl">
                            <div class="col-lg-4">
                                <div class="panel">
                                    <div class="box box-primary" style="margin-bottom:0px;height: 450px;">
                                        <asp:Repeater ID="RepTab" runat="server" OnItemDataBound="RepTab_ItemDataBound">
                                        <ItemTemplate>
                                        <div class="profile" style="margin-left:15px;">
                                            <div style="margin-bottom: 15px" class="row">
                                                <div class="col-xs-12 col-sm-11" >
                                                    <div style="text-align:center;margin-bottom:30px;">
                                                        <div class="caption">
                                                            <div class="pull-left image">
                                                                <img src="<%#Eval("profile_image_url")%>"  style="width:80px;" class="img-responsive img-circle" alt="User Image" />
                                                                <img src="<%=SessionState.WesiteImagesLoadURL%>socialmedia/1_verified_icon.png" title="facebook verified" />
                                                                <img src="<%=SessionState.WesiteImagesLoadURL%>socialmedia/2_verified_icon.png" title="twitter verified" />
                                                            </div>
                                                            <h3><%#Eval("reg_name")%></h3>
                                                            <p><small>(<a href="#"><%#Eval("reg_email")%> </a>)</small></p>
                                                        </div>                                                     
                                                    </div>
                                                    <p style="display:none">
                                                        <strong class="mrs">Registered Medias : </strong><br />
                                                        <asp:Label ID="lblMedias" runat="server" />
                                                        <div id="divMedias" runat="server" style="display:none"></div> 
                                                   </p>     
                                                      </div>
                                               
                                              
                                            
                                                      <div class="col-xs-12 col-sm-11" >
                                                       <ul class="todo-list ui-sortable">
                                                            <% if(Convert.ToString(medias).Contains("facebook")) { %>
                                       <li >
                               <i class="fa fa-facebook-square"></i>
                                             <span class="text">Friends: </span><small class="label label-primary" ><%#Eval("fbFrnds")%></small>&nbsp;
                                             <span class="text">Likes: </span><small class="label label-primary" ><%#Eval("fblikes")%></small>
                                        </li>
                                                             <% } %>
                                                                 <% if(Convert.ToString(medias).Contains("twitter")) { %>
                                                            <li>
                               <i class="fa fa-tumblr-square"></i>
                                     <span class="text">Friends: </span><small class="label label-info"><%#Eval("twFrnds")%></small>&nbsp;
                                             <span class="text">Likes: </span><small class="label label-info"><%#Eval("twLikes")%></small>

                                        </li>
                                                             <% } %>
                                                           <% if(Convert.ToString(medias).Contains("instagram")) { %>

                                                                   <li >
                               <i class="fa  fa-instagram"></i>
                                   <span class="text">Friends: </span><small class="label label-default"><%#Eval("instaFrnds")%></small>&nbsp;
                                             <span class="text">Likes: </span><small class="label label-default"><%#Eval("instalikes")%></small>

                                        </li>
                                                           <% } %>

                                    </ul>
                                                         </div>
                                            </div>
                                            <div class="row text-center divider" style="margin-right:-5px;margin-top:0px;">
                                                <div style="text-align:left;margin-left:15px;">
                                                    <strong class="mrs">Activities Status : </strong>
                                                </div>
                                     
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2 class="btn btn-warning btn-sm" style="cursor:default">
                                                        <strong><%#Eval("total_activity_pending") %></strong> Pending</h2>                                                   
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2 class="btn btn-primary btn-sm" style="cursor:default">
                                                        <strong><%#Eval("total_activity_approved") %></strong> Approved</h2>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2 class="btn btn-danger btn-sm" style="cursor:default">
                                                        <strong><%#Eval("total_activity_rejected") %></strong> Rejected</h2>                                                   
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2 class="btn btn-success btn-sm" style="cursor:default">
                                                        <strong><%#Eval("total_activity_rewarded") %></strong> Rewarded</h2>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2 class="btn btn-success btn-sm" style="cursor:default">
                                                        <strong><%#Eval("total_activity") %> Total</strong></h2>                                                    
                                                </div>
                                            </div>
                                              
                                        </div>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                
                                <div class="box box-primary" style="overflow-y: auto;height:450px;">
                                    <div class="portlet-header">
                                        <div class="caption">
                                            <h3 style="margin-left:15px;">Current Activity</h3></div>
                                    </div>
                                    <div style="overflow: hidden;margin-left:15px" class="portlet-body" >
                                        <div class="form-body pal">
                                           <div class="form-group">
                                               <div class="row">
                                               
                                                   <asp:Repeater ID="RepTab_FBCurrentActivity" runat="server">
                                                <ItemTemplate>                                         
                                                   <div  class="col-md-12">
                                                         <ul class="todo-list ui-sortable" style="  margin-right: 15px;">
                                                             <li style="border-left: 2px solid #00c0ef;">
                                                                 <img src="<%#Eval("sm_img_link") %>" /><br />
                                                    <%#Eval("sm_desc") %>&nbsp;<a href="<%#Eval("link") %>" target="_blank"><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/<%=current_media_id %>.png" style="width:20px;"/></a>
                                                             </li>
                                                             </ul>
                                                    <small class="text-muted pull-left">Created On: <i class="fa fa-clock-o"></i> <%#Eval("created_on") %></small>
                                                       <small class="text-muted pull-left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Updated On: <i class="fa fa-clock-o"></i> <%#Eval("updated_time") %></small>
                                                    <br />    
                                                    </div>
                                                    <div  class="col-md-8">
                                                    <span style="font-weight:bold;font-size:15px;display:none">Reward :</span> <span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %><br /></span>
                                                        <br />
                                                      <span  class="label label-default" style="font-size:20px;background-color:#f3f4f5; color:#000"><%#Convert.ToInt64(Eval("reward_amount")) %> BP</span>
                                                          </br>              <span><small>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">Default User : </span></strong><%#Convert.ToInt64(Eval("reward_per_user")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">No. Of Friends : </span></strong><%#Convert.ToInt64(Eval("no_of_friends")) %> x <%#Convert.ToInt64(Eval("reward_on_friend")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">No. Of Likes : </span></strong><%#Convert.ToInt64(Eval("no_of_likes")) %> x <%#Convert.ToInt64(Eval("reward_on_likes")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">No. Of Shares: </span></strong><%#Convert.ToInt64(Eval("no_of_shares")) %> x <%#Convert.ToInt64(Eval("reward_on_shares")) %> bp</span>
                                                                        </small></span>                                                                          
                                                    </div>
                                                    <div  class="col-md-4">
                                                    <div class="col-xs-12 col-sm-12">                                                                                                            
                                                        <button class="btn btn-orange btn-block">
                                                            <h2>
                                                            <strong><%#Convert.ToInt64(Eval("verification_score")) %>/10</strong></h2>
                                                            <span class="fa"></span>&nbsp; Verification Score
                                                        </button>
                                                        <p>&nbsp;</p>
                                                    <p><span style="font-weight:bold;font-size:15px;">Status :</span> <span class='verification_status_<%#Eval("verification_status") %>' ><%#Eval("verification_status_str")%></span></p>                                                    
                                                    <p><%#Eval("verification_log") %></p>
                                                    </div>
                                                    </div>
                                                    
                                                </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater ID="RepTab_GCurrentActivity" runat="server">
                                                <ItemTemplate>                                         
                                                   <div  class="col-md-12">
                                                  <small class="text-muted pull-left">Created On: <i class="fa fa-clock-o"></i> <%#Eval("created_on") %></small>
                                                       <small class="text-muted pull-left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Updated On: <i class="fa fa-clock-o"></i> <%#Eval("updated_time") %></small>
                                                    <br /><br />                                                                                                      
                                                    </div>
                                                    <div  class="col-md-8">
                                                   <span style="font-weight:bold;font-size:15px;"> Reward : </span> <span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %><br /></span>
                                                        <span ><h1><%#Convert.ToInt64(Eval("reward_amount"))%> BP</h1>                                                                        
                                                                          </span>
                                                                        <span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">Default User :</span></strong> <%#Convert.ToInt64(Eval("reward_per_user")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">No. Of Friends :</span> </strong><%#Convert.ToInt64(Eval("no_of_friends") )%> x <%#Convert.ToInt64(Eval("reward_on_friend")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">No. Of Likes :</span></strong> <%#Convert.ToInt64(Eval("no_of_likes")) %> x <%#Convert.ToInt64(Eval("reward_on_likes")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br /><strong><span style="font-weight:bold;font-size:14px;">No. Of Shares :</span></strong> <%#Convert.ToInt64(Eval("no_of_shares")) %> x <%#Convert.ToInt64(Eval("reward_on_shares")) %> bp</span>
                                                                        </span>                                                                          
                                                    </div>
                                                    <div  class="col-md-4" >
                                                    <div class="col-xs-12 col-sm-12">                                                                                                            
                                                        <button class="btn btn-orange btn-block">
                                                            <h2>
                                                            <strong><%#Eval("verification_score") %></strong></h2>
                                                            <span class="fa"></span>&nbsp; Verification Score
                                                        </button>
                                                    <p>&nbsp;</p>
                                                    <p><span style="font-weight:bold;font-size:15px;">Verification :</span> <span class='verification_status_<%#Eval("verification_status") %>' ><%#Eval("verification_status_str")%></span></p>                                                    
                                                    <p><%#Eval("verification_log") %></p>

                                                    </div>
                                                    </div>                                                    
                                                </ItemTemplate>
                                                </asp:Repeater>
                                                   <div style="clear:both;margin-bottom:30px;"></div>
                                                <div class="row" id="divApproveReject" runat="server" >
                                                        <div class="col-xs-12 col-sm-4" style="margin-left:15px;">                                                                                                       
                                                                <asp:LinkButton ID="lnkbtnApprove" runat="server" OnClick="lnkbtnApprove_Click" CommandArgument='<%=current_activity_id %>' class="btn btn-primary" Width="200px">APPROVE</asp:LinkButton>                    
                                                        </div>
                                                        <div class="col-xs-12 col-sm-4" style="  margin-left: -35px;">                              
                                                            <asp:LinkButton ID="lnkbtnReject" runat="server" OnClick="lnkbtnReject_Click" CommandArgument='<%=current_activity_id %>' class="btn btn-primary" Width="200px">REJECT</asp:LinkButton>
                                                        </div>                                                        
                                                    </div>
                                              </div> 
                                            </div>
                                        </div>  
                                                                            
                                       
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                        <div class="row mbl">
                            <div class="box box-primary">
                                <div class="col-lg-12">
                                <div class="form-body pal">
                                           <div class="form-group">   
                                               <div  class="col-md-12" style="padding-left: 0px;margin-bottom:20px;margin-top:20px;">
                                                   <asp:Repeater ID="RepTab_CampaignDetails" runat="server">
                                                <ItemTemplate>                                         
                                                <div class="todo-title">
                                                    <strong><span style="font-weight:bold;font-size:15px;">Campaign Details:</span> </strong> 
                                                       <ul class="todo-list ui-sortable" style="  margin-top: 10px;margin-bottom:10px;">
                                                             <li style="border-left: 2px solid #00c0ef;"><h3><%#Eval("campaign_name") %><img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" style="width:50px;" /></h3></li></ul>                                                 
                                                   <p>    <strong><span style="font-weight:bold;font-size:15px;">Objective :</span></strong> <%#Eval("campaign_objective_text") %> </p>
                                                    <p>
                                                        <strong><span style="font-weight:bold;font-size:15px;">Campaign Schedule :</span></strong> <%#Eval("campaign_execution_period") %></p>
                                                    <p>
                                                       <span style="font-weight:bold;font-size:15px;"> Reward to : </span><%#Eval("reward_whom_val")%></p>                                                  
                                                    <p>
                                                       <span style="font-weight:bold;font-size:15px;"> Max branddy points per activity : </span><%# Convert.ToInt64(Eval("max_brandyy_points"))%> BP</p>                                                  
                                                </div>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                               </div> 
                                          <div class="row" >
                                                        <div class="col-xs-12 col-sm-4" >                              <asp:LinkButton runat="server" ID="btn_PrevActivity" OnClick="btn_PrevActivity_Click" CommandArgument="<%#SessionState._PaginationSession.start_row%>" class="btn btn-primary" Width="200px">Prev Activity</asp:LinkButton>
                                                    </div>
                                                        
                                                        <div class="col-xs-12 col-sm-4" style="margin-left:-30px;">                              <asp:LinkButton runat="server" ID="btn_NextActivity" OnClick="btn_NextActivity_Click" CommandArgument="<%#SessionState._PaginationSession.start_row%>" class="btn btn-primary" Width="200px">Next Activity</asp:LinkButton>
                                                    </div>
                                                   </div>                                      
                                           </div>
                                        </div>
                            </div>
                            </div>
                        </div>
                        <div style="margin-bottom: 15px" class="row">
                                                <div class="panel panel-blue">
                                               <h4 style="margin-left:15px;">    <span class="text-green">Previous Activities</span></h4>  
                                                    <div class="panel-body">
                                                        <table class="table table-hover">
                                                            <thead>
                                                            <tr>                       
                                                                  <th><span style="font-weight:bold;font-size:15px;">Action</span></th>      
                                                                  <th><span style="font-weight:bold;font-size:15px;">Brand</span></th>                                     
                                                                  <th><span style="font-weight:bold;font-size:15px;">Performed On</span></th>
                                                                  <th><span style="font-weight:bold;font-size:15px;">Activity Score</span></th>                                                                  
                                                                  <th><span style="font-weight:bold;font-size:15px;">Reward</span></th>
                                                                  <th><span style="font-weight:bold;font-size:15px;">Status</span></th>                                                                  
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
                                                                          <span><%#Convert.ToInt64(Eval("verification_score")) %>/10&nbsp;&nbsp;</span>                                                                          
                                                                      </td>
                                                                      <td>
                                                                          <%#Convert.ToInt64(Eval("reward_amount")) %> BP
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><br />  <span style="font-weight:bold;font-size:14px;">Default User : </span><%#Convert.ToInt64(Eval("reward_per_user")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><br />  <span style="font-weight:bold;font-size:14px;">No. Of Friends : </span><%#Convert.ToInt64(Eval("no_of_friends")) %> x <%#Convert.ToInt64(Eval("reward_on_friend")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><br />  <span style="font-weight:bold;font-size:14px;">No. Of Likes : </span> <%#Convert.ToInt64(Eval("no_of_likes")) %> x <%#Convert.ToInt64(Eval("reward_on_likes")) %> bp</span>
                                                                          <span style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><br /> <span style="font-weight:bold;font-size:14px;">No. Of Shares : </span><%#Convert.ToInt64(Eval("no_of_shares")) %> x <%#Convert.ToInt64(Eval("reward_on_shares")) %> bp</span> 
                                                                      </td>
                                                                      <td>
                                                                          <span class='verification_status_<%#Eval("verification_status") %>' ><%#Eval("verification_status_str")%></span>
                                                                          <span><br /><%#Eval("verification_log") %></span><br />
                                                                          <span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %></span>
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
                      <div runat="server" id="div_NoContent" visible="false" class="row"><p><span>No activities pending for verification</span></p></div> 
                    </div>
                            
                </div>
                </div>
                <!--END CONTENT-->                          
           </section>    
 <style type="text/css">
    .verification_status_0 {
        background-color:#f39c12;color:#fff;padding: .2em .6em .3em;
    }
    .verification_status_1 {
        background-color:#00a65a;color:#fff;padding: .2em .6em .3em;
    }
    .verification_status_2 {
        background-color:#f56954;color:#fff;padding: .2em .6em .3em;       
    }
    .verification_status_3 {
        background-color:#f1521c;color:#fff;padding: .2em .6em .3em;
    } 
    .reward_status_ {
        color:#fff
    }  
        .reward_status_0 {
          color:#fff
    }   
    .reward_status_1 {
        background-color:#00a65a;color:#fff;padding: .2em .6em .3em;    
    }
    .reward_status_2 {
        background-color:#f56954;color:#fff;padding: .2em .6em .3em;           
    }
    .reward_status_3 {
       
        background-color:#f1521c;color:#fff;padding: .2em .6em .3em;
    }
</style>            
      
      </ContentTemplate>
     
       </asp:UpdatePanel>
</asp:Content>

