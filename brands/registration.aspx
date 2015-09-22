<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="brands_registration" %>

<!DOCTYPE html>

<html style="background:#fff;">
    <head>
        <meta charset="UTF-8">
        <title>AdminLTE | Registration Page</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="<%=SessionState.WebsiteURLBrand %>css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="<%=SessionState.WebsiteURLBrand %>css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="<%=SessionState.WebsiteURLBrand %>css/AdminLTE.css" rel="stylesheet" type="text/css" />
        <link href="<%=SessionState.WebsiteURLBrand %>css/StyleSheet1.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
    <body style="background:#fff;">
           <div class="loginHeader">
                     <img src="<%=SessionState.WebsiteURL %>images/logo.png" style="width:385px" />
       </div>
        <div class="form-box" id="login-box" style="margin-top:15px;">
            <div class="header" style="background: #0060b6;box-shadow:none">Registration</div>
            <form runat="server" id="form1">      
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                         <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>          
                <div class="body bg-gray" >
                    
                    <div class="form-group" style="margin-bottom:0px;">
                        <asp:TextBox runat="server" ID="txtbrandname" CssClass="form-control" placeholder="Name"/>                           <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="*"
                            ValidationGroup="save" ControlToValidate="txtbrandname" InitialValue="" ForeColor="Red" />
                    </div>
                    <div class="form-group" style="margin:0px;">
                        <asp:TextBox runat="server" ID="txtbrandusername" CssClass="form-control" placeholder="Email"/>                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ValidationGroup="save" ControlToValidate="txtbrandusername" InitialValue="" ForeColor="Red" />
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ValidationGroup="save" ControlToValidate="txtbrandusername"  ForeColor="Red" ErrorMessage="Invalid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group" style="margin:0px;">
                        <asp:TextBox runat="server" TextMode="Password" ID="txtbranduserpswd1" CssClass="form-control" placeholder="Password"/>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ValidationGroup="save" ControlToValidate="txtbranduserpswd1" InitialValue="" ForeColor="Red" />
                    </div>
                    <div class="form-group" style="margin:0px;">
                        <asp:TextBox runat="server" TextMode="Password" ID="txtbranduserpswd2" CssClass="form-control" placeholder="Retype Password" />
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ValidationGroup="save" ControlToValidate="txtbranduserpswd2" InitialValue="" ForeColor="Red" />
                                      <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn’t match"  ValidationGroup="save" ControlToCompare="txtbranduserpswd1" ControlToValidate="txtbranduserpswd2" ForeColor="Red"></asp:CompareValidator>
                    </div>
                         <div class="form-group" style="margin:0px;">
                             <table>
                                 <tr>
                                     <td><span style="color:#3c8dbc;">Brand Logo </span></td>
                                     <td> <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="Smaller" BorderStyle="NotSet" Font-Strikeout="False" /></td>
                                 </tr>
                             </table>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="save" ControlToValidate="FileUpload1" ForeColor="Red" />
                             </div>
                </div>
                                    
                <div class="footer" style="background:#eaeaec;">                    
                    <asp:Button runat="server" CssClass="btn bg-brandyy-blue btn-block" Text="Sign up" ID="btnRegister"    ValidationGroup="save" OnClick="btnRegister_Click"/>                    

                    <a href="<%=SessionState.WebsiteURLBrand %>" class="text-center">I already have a membership</a><br /><br />
                    
                    <div class="alert alert-danger alert-dismissable" id="divAlert" runat="server" visible="false">
                                        <i class="fa fa-ban"></i>
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <b>Alert!</b> <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                                    </div>
                </div>
                                    </ContentTemplate>
                                           <Triggers>
                                      <asp:PostBackTrigger ControlID="btnRegister" />
                                  </Triggers>
                             </asp:UpdatePanel>
            </form>
            
        </div>
             <div class="loginfotter">
            <p>© Copyright 2015 Brandyy. All rights reserved</p>
        </div>

        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="<%=SessionState.WebsiteURLBrand %>js/bootstrap.min.js" type="text/javascript"></script>

    </body>
</html>
