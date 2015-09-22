<%@ Page Language="C#" AutoEventWireup="true" CodeFile="brand-create-campaign-popup-1.aspx.cs" Inherits="brands_brand_create_campaign_popup_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background:white;">
<head id="Head1" runat="server">
         <link href="<%=SessionState.WebsiteURLBrand+ "css/StyleSheet1.css"%>" rel="stylesheet" type="text/css" />
            <link href="<%=SessionState.WebsiteURLBrand+ "css/AdminLTE.css"%>" rel="stylesheet" type="text/css" />
           <link href="<%=SessionState.WebsiteURLBrand+ "css/ionicons.min.css"%>" rel="stylesheet" type="text/css" />
     <link href="<%=SessionState.WebsiteURLBrand+ "css/font-awesome.min.css"%>" rel="stylesheet" type="text/css" />
      <link href="<%=SessionState.WebsiteURLBrand+ "css/bootstrap.min.css"%>" rel="stylesheet" type="text/css" />
    <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <script type="text/javascript">    window.jQuery || document.write('<script src="<%=SessionState.WebsiteURLBrand+ "js/jquery-1.11.0.min.js"%>"><\/script>')</script> 

    <title></title>
</head>
<body style="background:white;">
    
    <form id="form1" runat="server" style="background:white;">
        <asp:ScriptManager ID="ScriptManager2" runat="server"  EnablePageMethods="true"/>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
    <section class="content" style="background:white;">
        <div class="row">            
            <table class="table">
                <tr>
                    <td style="width:40%;vertical-align:top;border:1px solid white;">
                        <div class="box box-success" style="height: 350px;vertical-align:top">
                                <h3 style="font-size:16px" class="text-black">&nbsp;Choose Campaign Objective</h3>  
                                    <table class="table table-hover" style="text-align:left;">                                                        
                                       <asp:Repeater ID="Repeater1" runat="server">
                                         <ItemTemplate>
                                           <tr>                                                                                
                                            <td style="width:5%;text-align:center"><img src='<%=SessionState.WebsiteURLAdmin+ "images/campaign_types/campaign_type_"%><%#Eval("id")%>.png' /></td>
                                            <td style="width:95%;cursor:pointer" title="<%#Eval("name") %>">
                                                <asp:LinkButton runat="server" ID="lnkShow" OnClick="lnkShow_Click" CommandArgument='<%#Eval("id") %>'><%#Eval("name") %></asp:LinkButton>
                                            </td>
                                           </tr>                                                                        
                                         </ItemTemplate>
                                       </asp:Repeater>
                                    </table>
                            </div>
                    </td>
                    <td style="width:60%;border:1px solid white;" id="rightcol">
                        <div class="box box-info campaign_type_box campaign_type_box" style="height: 350px;">
                            <table class="table" style="text-align:left;">
                                <tr><td>                                                                
                                <div id="ucc1" runat="server"></div> 
                              </td></table>
                            </div>
                    </td>
                </tr>
            </table>
           
        </div>
    </section>
<script type="text/javascript">
    $(document).ready(function () {
        $(".campaign_type_box").hide();
        $("#rightcol").hide();
    });

    function ShowRightCol() {
        $("#rightcol").show();
        $(".campaign_type_box").show();
    }
</script>
         </ContentTemplate>
                            </asp:UpdatePanel>
    </form>
                               
</body>
</html>
