<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_8.ascx.cs" Inherits="brands_uc_create_campaign_8" %>


<div class="form-group">
    <table style="width:100%">     
        <tr><td><asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select One Or More Action(s)" ForeColor="Red" /></td></tr>   
        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_1" Text="&nbsp;Do you want user to visit any website?" Checked="true" />                                
                <label for="txtReward_1" class="control-label"><i class="fa fa-info-circle"></i></label>
            </td>
        </tr>
        <tr>
            <td>                
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
<div class="form-group">
    
    <div  class="col-md-6">                                                                                            
        <asp:DropDownList ID="DrpPages1" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DrpPages1_SelectedIndexChanged">
        </asp:DropDownList> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="DrpPages1" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
        <asp:HiddenField ID="hiddenPageURL1" runat="server" />
        <asp:Label ID="lblNoPages1" runat="server" Visible="false" BorderStyle="Dotted">You have not configured any websites. Please click on <a href="<%=SessionState.WebsiteURLBrand %>brand-create-page.aspx">ADD PAGES</a></asp:Label>
    </div>
</div>
                        </ContentTemplate>
     </asp:UpdatePanel>
                <div class="form-group">&nbsp;</div>
                </td>
        </tr>

        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_3" Text="&nbsp;Do you want user to view any document?" Checked="true" />
                <label for="txtReward_1" class="control-label"><i class="fa fa-info-circle"></i></label>
        </td>
            </tr>
        <tr>
            <td>              
            <div class="form-group">&nbsp;</div>
             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
            <div class="form-group">                    
                <div  class="col-md-12">                                                                                            
                    <asp:FileUpload ID="fileDoc" runat="server"></asp:FileUpload>
                    <asp:LinkButton runat="server" ForeColor="Blue" ID="lnkUploadDoc" OnClick="lnkUploadDoc_Click">Upload Document</asp:LinkButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=""
                                             ControlToValidate="fileDoc" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                    <asp:Label ID="Label1" runat="server" Visible="false" BorderStyle="Dotted">&nbsp;</asp:Label>        
                    <asp:Label ID="lblDocName" runat="server" Visible="true"><br />Selected File Name : <%=doc_name %>
                       
                    </asp:Label>            
                    
                </div>
            </div>
                                    </ContentTemplate>
                 <Triggers>
                    <asp:PostBackTrigger ControlID="lnkUploadDoc" />
                </Triggers> 
                 </asp:UpdatePanel>
                <div class="form-group">&nbsp;</div>
                </td>
            </tr>

        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_2" Text="&nbsp;Do you want user to watch a video?" Checked="true" />
                
                <label for="txtReward_1" class="control-label"><i class="fa fa-info-circle"></i></label>
            </td>
            </tr>
        <tr>
            <td>
            <div class="form-group">&nbsp;</div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                    <ContentTemplate>
            <div class="form-group">                
                <div  class="col-md-12">         
                <asp:DropDownList ID="drpVideoSource" runat="server" OnSelectedIndexChanged="drpVideoSource_SelectedIndexChanged" AutoPostBack="true">
                   <asp:ListItem Value="1">Upload File</asp:ListItem>
                   <asp:ListItem Value="2">Youtube Link</asp:ListItem>
                   <asp:ListItem Value="3">Live Stream Link</asp:ListItem>
                </asp:DropDownList>
                    </div>
                <div  class="col-md-12">  
                    <div>
                        <div runat="server" id="tr_video_1" Visible="true"> 
                            <asp:FileUpload ID="fileVideo" runat="server"></asp:FileUpload>
                            <asp:LinkButton runat="server" ForeColor="Blue" ID="lnkUploadVideo" OnClick="lnkUploadVideo_Click">Upload Video</asp:LinkButton>
                            <asp:Label ID="Label2" runat="server" Visible="true"><br />
                                <div >
                                <video controls="controls" onended="videoEnded()" width="320" height="240" <%=(video_name.Trim()=="")?"style='display:none'":""%>>
  <source src="<%=SessionState.WebsiteURL %>brands/campaigns/14/<%=SessionState._Campaign.brand_id %>/<%=SessionState._Campaign.campaign_id %>/<%=video_name %>" type="video/mp4" />
  Your browser does not support the video tag.
</video>
                                    <a href="#" onclick="playvideo()" id="play" >play</a>
                                </div>
                                
 
                            </asp:Label> 
                            
                        </div>
                        <div runat="server" id="tr_video_2" Visible="false">                                                                                            
                            <asp:TextBox runat="server" ID="txtVideoLink" Columns="50"></asp:TextBox><asp:LinkButton runat="server" ID="lnkLoadVideo2" OnClick="lnkLoadVideo2_Click">[Load]</asp:LinkButton>
                            <iframe width="420" height="315" <%=(txtVideoLink.Text.Trim()=="")?"src1":"src"%>="<%=txtVideoLink.Text.ToString().Replace("https","http").Replace("http://www.youtube.com/watch?v=", "http://www.youtube.com/embed/") %>?vq=hd1080&autohide=1&controls=0&fs=0&modestbranding=1&rel=0&showinfo=0&start=40&end=50"></iframe>
                        </div>
                        <div runat="server" id="tr_video_3" Visible="false">                                                                                            
                            <asp:TextBox ID="txtVideoLiveStreamLink" runat="server" Columns="50"></asp:TextBox>
                        </div>
                    </div>                
                </div>
            </div>
                            </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkUploadVideo" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="form-group">&nbsp;</div>
                </td>
            </tr>

        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_4" Text="&nbsp;Do you want user to view photos?" Checked="true" />
                <label for="txtReward_1" class="control-label"><i class="fa fa-info-circle"></i></label>
            </td>
            </tr>
        <tr>
            <td>
             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
            <div class="form-group">                
                <div  class="col-md-12">                                                                                            
                    <asp:FileUpload ID="fileSlideShow" runat="server"></asp:FileUpload>
                    <asp:LinkButton runat="server" ForeColor="Blue" ID="lnkUploadToGallery" OnClick="lnkUploadToGallery_Click">Upload To Gallery</asp:LinkButton>        
                </div>
                <div  class="col-md-12">  
                    <!-- start slider -->
                    <div class="flexslider">
                      <ul class="slides">
                        <asp:Repeater runat="server" ID="repSlideShow">
                            <ItemTemplate>
                            <li>
                              <img class="thumbnail" src="<%# DataBinder.Eval(Container.DataItem,"Value") %>" title="<%# (DataBinder.Eval(Container.DataItem,"Text").ToString()).Split('.')[0].ToString() %>" alt="<%# (DataBinder.Eval(Container.DataItem,"Text").ToString()).Split('.')[0].ToString() %>" />
                                <asp:LinkButton runat="server" ID="btn_RemovePhoto" OnClick="btn_RemovePhoto_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Value") %>'>[Delete]</asp:LinkButton>                    
                            </li>
                            </ItemTemplate>
                        </asp:Repeater>            
                      </ul>
                    </div>               
                    <!-- end slider -->                                                                                   
                </div>
            </div>
                                    </ContentTemplate>
                 <Triggers>
                    <asp:PostBackTrigger ControlID="lnkUploadToGallery" />
                </Triggers> 
                 </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td>
                <div>&nbsp;</div>                           
                                          <div class="form-actions text-right pal">
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                                                                     
                                           <asp:Button runat="server" Text="Back" ValidationGroup="save" ID="btn_Back" OnClick="btn_Back_Click" CssClass="btn btn-primary" />
                                           <asp:Button runat="server" Text="Next" ValidationGroup="save" ID="btn_Next" OnClick="btn_Next_Click" CssClass="btn btn-primary" />
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>
                                          </div>
            </td>
        </tr>
    </table>
    
</div>
      

  <!-- FlexSlider -->
	<link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "styles/flexslider.css"%>" type="text/css" media="screen" />
  <script defer src="<%=SessionState.WebsiteURLBrand+ "js/jquery.flexslider.js"%>"></script>
<script type="text/javascript">
    $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide",
            animationLoop: false,
            itemWidth: 210,
            itemMargin: 5
        });
    });

    function playvideo() {        
        $('video')[0].play();        
    }

    function videoEnded() {
        
    }
        

</script>
 <!-- FlexSlider End -->
<script defer src="<%=SessionState.WebsiteURLBrand+ "js/modernizr-2.6.2-respond-1.1.0.min.js"%>"></script>

