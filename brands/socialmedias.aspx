<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="socialmedias.aspx.cs" Inherits="brands_socialmedias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
       <link href="<%=SessionState.WebsiteURLBrand+ "css/StyleSheet1.css"%>" rel="stylesheet" type="text/css" />
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Registered Pages                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Registered Pages</li>                        
                    </ol>
                </section>
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Registered social media accounts listing</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#1"><i class="fa fa-fw fa-question-circle"></i> Help: Registering Social Media</a></li>                        
                    </ol>
                </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                 
                <!-- Main content -->
                <section class="content">
                    <div>                                                
                        <div class="box-header" style="text-align:right">
                            <a class="btn btn-primary" href="#" onclick="ShowAddButtons()" id="btnAddNewPage">Add New Page</a>
                            <a class="btn btn-primary" href="#" onclick="HideAddButtons()" id="btnBack" style="display:none">Back</a>
                            </div>                            
                                <div class="box-header" style="display:none">                                  
                                    <h3 class="box-title">                                                                                
                                        <a href="#" onclick="activate('<%=SessionState.WebsiteURLBrand + "activatefb.aspx" %>')"  class="btn btn-default"><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/1.png" alt="" /> Add Page</a> &nbsp;&nbsp;        
                                        <a href="<%=SessionState.WebsiteURLBrand + "activatetw.aspx" %>"  class="btn btn-default"><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/2.png" alt=""/> Add Page</a>&nbsp;&nbsp;
                                        <a href="<%="https://api.instagram.com/oauth/authorize/?client_id=" + System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString() + "&redirect_uri="+System.Configuration.ConfigurationManager.AppSettings["instagram.brands.redirecturi"].ToString()+"&response_type=code" %>"  class="btn btn-default">
                                        <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/3.png" alt=""/> Add Page</a>&nbsp;&nbsp;
                                        <a href="<%=SessionState.WebsiteURLBrand + "activatewebsite.aspx" %>" class="btn btn-default"><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/4.png" alt=""/> Add Page</a>
                                    </h3>                                    
                                </div><br />
                        </div>
                    <div class="box  box-primary" id="tblAddBtns" style="display:none;">    
                        <table  style="margin:15px;" class="table">
                           <tr>
                               <td style="border-top: none;padding-bottom:20px;">
                                    <div class="span2"   style="background: #305fa1;">
      <div class="reveal" >
          <div style="clear:both"></div>
          <div class="imgbox">
  <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/social-fb.png" alt=""  /> 
          </div>
              <span >Facebook</span>
        
        <div class="hidden">
            <div class="caption">
                <div class="centered">
                    <p>Here's a FREE collection of superb image hover effects in both pure CSS3 and jQuery. Bring your images to life with some beautiful animation and transition effects..</p>
                       <a href="#" onclick="activate('<%=SessionState.WebsiteURLBrand + "activatefb.aspx" %>')" class="btn btn-primary">Add Facebook Page</a>
                </div>
            </div>
        </div>
      </div>
  </div>
                               </td> 
                                <td style="border-top: none;padding-bottom:20px;">
                                                                  <div class="span2"   style="background: #1dc6ff;">
      <div class="reveal" >
          <div style="clear:both"></div>
          <div class="imgbox">
  <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/social-tw.png" alt=""  /> 
          </div>
          <span >Twitter</span>
        <div class="hidden">
            <div class="caption">
                <div class="centered">
                    <p>Here's a FREE collection of superb image hover effects in both pure CSS3 and jQuery. Bring your images to life with some beautiful animation and transition effects..</p>
                       <a href="<%=SessionState.WebsiteURLBrand + "activatetw.aspx" %>" class="btn btn-primary">Add twitter Page</a>
                </div>
            </div>
        </div>
      </div>
  </div>
                            </td>
                  <td style="border-top: none;padding-bottom:20px;">
                                                                                              <div class="span2"   style="background: #d2c3b2;">
      <div class="reveal" >
          <div style="clear:both"></div>
          <div class="imgbox">
  <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/social-insta.png" alt=""  /> 
          </div>
          <span >Instagram</span>
        <div class="hidden">
            <div class="caption">
                <div class="centered">
                    <p>Here's a FREE collection of superb image hover effects in both pure CSS3 and jQuery. Bring your images to life with some beautiful animation and transition effects..</p>
                     <a href="<%="https://api.instagram.com/oauth/authorize/?client_id=" + System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString() + "&redirect_uri="+System.Configuration.ConfigurationManager.AppSettings["instagram.brands.redirecturi"].ToString()+"&response_type=code" %>"  class="btn btn-primary">Add Insta Page</a>
                </div>
            </div>
        </div>
      </div>
  </div>
                            </td>
                          <td style="border-top: none;padding-bottom:20px;">
                                 <div class="span2"   style="background: #609dd8;">
      <div class="reveal" >
          <div style="clear:both"></div>
          <div class="imgbox">
  <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/social-web.png" alt=""  /> 
          </div>
          <span >Website</span>
        <div class="hidden">
            <div class="caption">
                <div class="centered">
                    <p>Here's a FREE collection of superb image hover effects in both pure CSS3 and jQuery. Bring your images to life with some beautiful animation and transition effects..</p>
                     <a href="<%=SessionState.WebsiteURLBrand + "activatewebsite.aspx" %>"   class="btn btn-primary">Add Web Page<a/>
                </div>
            </div>
        </div>
      </div>
  </div>
                            </td>
                          </tr>
                           <%-- <tr>
                                <td style="border-top: none" >
                                    &nbsp;
                                </td>
                            </tr>--%>
                        </table>                        
                    </div>
                    
                    <div class="box  box-primary" id="tblPages">    
                            <table class="table">
                                        <tr>
                                            <th style="width: 10px">#</th>
                                            <th style="width: 10px">&nbsp;</th>                                            
                                            <th style="width: 50px">Account <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="" data-original-title="Name of the social media account using which the page has been registered. This is not applicable for websites."></i></th>                                                                                        
                                            <th>Page Url <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="" data-original-title="Url of the registered page"></i></th>                                            
                                            <th style="width: 250px">Authorize <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="" data-original-title="Authorize the account so that we can access the page. This is not applicable for websites."></i></th>
                                        </tr>
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                         <ItemTemplate>
                                    <tr>
                                        <td><%=++Cnt %>.</td>
                                        <td><img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/<%#Eval("id")%>.png" alt=""/></td>
                                        <td><%#Eval("sm_name") %></td>                                        
                                        <td><%#Eval("page_url") %></td>
                                        <td><%#Eval("name") %>
                                            <asp:HiddenField runat="server" ID="hdnSmID" Value='<%#Eval("brand_sm_id") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                        </asp:Repeater>
                            </table>
                     </div>
                                             
                                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                         <ItemTemplate>
                    <div class="box  box-primary">                                          
                                <div class="box-header">
                                    <h3 class="box-title">
                                        
                                        </h3>                                    
                                    <h3 class="box-title" style="text-align:right">
                                        Click on "<strong>Add Page URL</strong>" to configure your <%#sm_text[Convert.ToByte(Eval("id"))] %> 
                                    </h3>   
                                </div><!-- /.box-header -->
                        <div style="margin:5px;"></div>
                        
                                <div class="box-body no-padding">
                                    <table class="table">
                                        <tr>
                                            <th style="width: 10px">#</th>
                                            <th>Pages</th>                                            
                                            <th>&nbsp;</th>                                            
                                            <th style="width: 40px">&nbsp;</th>
                                        </tr>                                        
                                        <asp:Repeater ID="Repeater2" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                <td><%=++Cnt %>.</td>
                                                <td>
                                                    </td>
                                                 <td><%#Eval("page_url") %></td>
                                                <td>
                                                    <asp:Button ID="btn_CreateCampaign" runat="server" role="button" class="btn btn-default" Text="Update Page URL" CommandName="CreatePage"  CommandArgument='<%#Eval("page_id")%>' />      
                                                </td>                                                
                                            </tr>                                             
                                        </ItemTemplate>
                                    </asp:Repeater>
                                        <tr>
                                            <td colspan="4" style="text-align:right"> <asp:Button ID="btnAddPages" runat="server" CssClass="btn btn-primary" Visible='<%#(Eval("brand_id") == DBNull.Value) ? false : true  %>' Text="Add Page URL" OnClick="btnAddPages_Click" CommandArgument='<%#Eval("sm_id") + "," + Eval("brand_sm_id") %>' /></td>
                                        </tr>
                                    </table>
                                </div><!-- /.box-body -->
                            </div>     
                             </ItemTemplate>
                        </asp:Repeater>      
                </section><!-- /.content -->

                <body>

<div id="dialog" style="display:none;" title="Dialog Title">
<iframe frameborder="0" scrolling="no" width="180" height=80" src="" id="ifrmLoad">
</iframe>
</div>

<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
<script>
    function activate(pUrl) {        
        var ifrm = document.getElementById("ifrmLoad");
        ifrm.src = pUrl;        
        $("#dialog").dialog("open");
    }
    $( "#dialog_trigger" ).click(function() {
        $( "#dialog" ).dialog( "open" );
    });
    $("#dialog").dialog({
        autoOpen: false,
        position: 'center' ,
        title: 'Activate',
        draggable: false,
        width : 250,
        height : 160, 
        resizable : true,
        modal : true,
    });
   
    function ShowAddButtons() {        
        document.getElementById("tblAddBtns").style.display = "";
        document.getElementById("btnBack").style.display = "";
        document.getElementById("tblPages").style.display = "none";
        document.getElementById("btnAddNewPage").style.display = "none";
        
    }
    function HideAddButtons() {
        document.getElementById("tblAddBtns").style.display = "none";
        document.getElementById("btnBack").style.display = "none";
        document.getElementById("tblPages").style.display = "";
        document.getElementById("btnAddNewPage").style.display = "";

    }
    </script>
 
             </ContentTemplate>     
       </asp:UpdatePanel>
</asp:Content>
