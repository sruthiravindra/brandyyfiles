<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>brandyy - Admin | Log in</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="<%=SessionState.WebsiteURLAdmin %>css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="<%=SessionState.WebsiteURLAdmin %>css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="<%=SessionState.WebsiteURLAdmin %>css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
    <body class="bg-black">

        <div class="form-box" id="login-box">
            <div class="header">brandyy - Admin Sign In</div>
            <form runat="server" id="form1">
                <div class="body bg-gray">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtadminusername" CssClass="form-control" placeholder="Admin Email"/> 
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" TextMode="Password" ID="txtadminuserpswd1" CssClass="form-control" placeholder="Password"/>
                    </div>          
                    <div class="form-group">
                        <input type="checkbox" name="remember_me"/> Remember me
                    </div>
                </div>
                <div class="footer">                                                               
                    <asp:Button runat="server" CssClass="btn bg-light-blue btn-block" Text="Sign me in" ID="btnLogin" OnClick="btnLogin_Click"/> 
                    
                    <p> <asp:LinkButton ID="lnkForgotPass" runat="server" OnClick="lnkForgotPass_Click">Forgot password?</asp:LinkButton></p>
                    <div id="divMail" runat="server" visible="false" style="margin-bottom:5px;">
    <asp:TextBox ID="txtRegEmail" runat="server" Width="230px" placeholder="Enter registration email"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtRegEmail" ValidationGroup="sendMail" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:Button ID="btnSend" runat="server" Text="Send Mail" class="btn btn-danger btn-sm" OnClick="btnSend_Click" ValidationGroup="sendMail" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="Invalid Email" ControlToValidate="txtRegEmail" ValidationGroup="sendMail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic"  ForeColor="Red">    </asp:RegularExpressionValidator>
                    </div>
                    
                    <br /><br />
                    <div class="alert alert-danger alert-dismissable" id="divAlert" runat="server" visible="false">
                                        <i class="fa fa-ban"></i>
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <b>Alert!</b> <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                                    </div>
                </div>
            </form>

            
        </div>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="<%=SessionState.WebsiteURLAdmin %>js/bootstrap.min.js" type="text/javascript"></script>        

    </body>
</html>