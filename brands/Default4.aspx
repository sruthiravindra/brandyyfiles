<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="brands_Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
         <link href="<%=SessionState.WebsiteURLBrand+ "css/StyleSheet1.css"%>" rel="stylesheet" type="text/css" />
            <link href="<%=SessionState.WebsiteURLBrand+ "css/AdminLTE.css"%>" rel="stylesheet" type="text/css" />
           <link href="<%=SessionState.WebsiteURLBrand+ "css/ionicons.min.css"%>" rel="stylesheet" type="text/css" />
     <link href="<%=SessionState.WebsiteURLBrand+ "css/font-awesome.min.css"%>" rel="stylesheet" type="text/css" />
      <link href="<%=SessionState.WebsiteURLBrand+ "css/bootstrap.min.css"%>" rel="stylesheet" type="text/css" />

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <div id="container" >
     <div id="fancybox-wrap" style="width: 695px; height: auto; display: block;">
     <div id="fancybox-content" style="border-width: 10px; width: 675px; height: auto;">
         <div style="width:auto;height:auto;overflow: hidden;position:relative;">
         <div id="data" style="color:#000000;background-color:#fff;width:100%;height:100%">
         <div class="wrapp">
<h1 style="color: #fff">&nbsp;</h1>
             <table>
                 <tr>
                     <td >
                         <div style="margin-right:30px;">
                                       <a href='<%=SessionState.WebsiteURLBrand+ "socialmedias.aspx"%>' onclick='window.close();' target='_blank' style="cursor:pointer">
                                          <div class="stepBox" >
                                     <p class="box-title" style="cursor:pointer"><br />Step1 : <br />Register Your Social Media Accounts With Us</p> </div>
                          <p style="margin-top:20px;text-align:center"> <span class="label label-primary" style="font-size:100% !important">Settings</span><br /><span class="text-red"><span class="glyphicon glyphicon-arrow-down"></span></span><br /><span class="label label-success" style="font-size:100% !important">Social Media</span></p>
                                           </a>
                         </div>
                     </td>
                     <td>
                         <img src='<%=SessionState.WebsiteURLBrand + "img/a1.png" %>'  style="margin-right:30px;margin-top:-100px;"/>
                     </td>
                     <td >
                          <div style="margin-right:30px;">
                              <a href='<%=SessionState.WebsiteURLBrand+ "brandyypoints-package.aspx"%>' onclick='window.close();' target='_blank'  style="cursor:pointer">
                       <div class="stepBox" style="  background-color: #ffc000 !important;">
                                     <p class="box-title" style="cursor:pointer"><br />Step2 :<br /> Buy Brandyy Points</p> </div>
                               <p style="margin-top:20px;text-align:center"> <span class="label label-primary" style="font-size:100% !important">Accounts</span><br /><span class="text-red"><span class="glyphicon glyphicon-arrow-down"></span></span><br /><span class="label label-success" style="font-size:100% !important">Buy Brandyy Points</span></p>
                                                           </a>
                              </div>
                     </td>
                     <td>
                         <img src='<%=SessionState.WebsiteURLBrand + "img/a2.png" %>' style="margin-right:30px;margin-top:-100px;" />
                     </td>
                     <td>
                          <div style="margin-right:30px;">
                                        <a href='<%=SessionState.WebsiteURLBrand+ "brand-create-campaign.aspx"%>' onclick='window.close();' target='_blank'  style="cursor:pointer">
                        <div class="stepBox" style="  background-color: #2ed7a1 !important;">
                                     <p class="box-title" style="cursor:pointer"><br />Step3 :<br /> Create a Campaign</p> </div>
                               <p style="margin-top:20px;text-align:center"> <span class="label label-primary" style="font-size:100% !important;">Marketing</span><br /><span class="text-red"><span class="glyphicon glyphicon-arrow-down"></span></span><br /><span class="label label-success" style="font-size:100% !important">Create Campaigns</span></p>
                                            </a>
                              </div>
                     </td>
                 </tr>
             </table>

                 <div id="fancybox-close" style="display: inline;text-decoration:none;" >
                                           <asp:CheckBox ID="chkSubs" runat="server" OnCheckedChanged="chkSubs_CheckedChanged" AutoPostBack="true"/> <span style="color:#3c8dbc">don’t show this popup again</span>
                 </div>
</div>                                                                                                                                 </div>
   </div>
</div>
        </div>
              </div>
    </form>
</body>
</html>
