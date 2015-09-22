<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="help.aspx.cs" Inherits="brands_help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content">   
                    <div class="row">
                        <div class="col-lg-3 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3>
                                        Step 1
                                    </h3>
                                    
                                    <p>
                                      <h2>Register Your Social Media Accounts With Us</h2><br />
                                        <div style="margin-right:30px;">                                       
                          <p style="margin-top:20px;text-align:center"> <span class="label label-primary" style="font-size:100% !important">Settings</span><br /><span class="text-red"><span class="glyphicon glyphicon-arrow-down"></span></span><br /><span class="label label-success" style="font-size:100% !important">Social Media</span></p>
                                           </a>
                         </div>
                                    </p>
                                </div>
                                <div class="icon">
                                    
                                </div>
                                <a href="#" class="small-box-footer">
                                    More info <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div><!-- ./col -->
                        <div class="col-lg-3 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <h3>
                                        Step 2
                                    </h3>
                                    
                                    <p>
                                      <h2>Buy Brandyy Points</h2><br />
                                        <div style="margin-right:30px;">                                     
                          <p style="margin-top:20px;text-align:center"> <span class="label label-primary" style="font-size:100% !important">Accounts</span><br /><span class="text-red"><span class="glyphicon glyphicon-arrow-down"></span></span><br /><span class="label label-success" style="font-size:100% !important">Buy Brandyy Points</span></p>
                                        
                                           </a>
                         </div>
                                    </p>
                                </div>
                                <div class="icon">
                                    
                                </div>
                                <a href="#" class="small-box-footer">
                                    More info <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div><!-- ./col -->
                        <div class="col-lg-3 col-xs-6">
                            <!-- small box -->
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h3>
                                        Step 3
                                    </h3>
                                    
                                    <p>
                                      <h2>Create Campaigns</h2><br />
                                        <div style="margin-right:30px;">                                       
                          <p style="margin-top:20px;text-align:center"> <span class="label label-primary" style="font-size:100% !important;">Marketing</span><br /><span class="text-red"><span class="glyphicon glyphicon-arrow-down"></span></span><br /><span class="label label-success" style="font-size:100% !important">Create Campaigns</span></p>
                                           </a>
                         </div>
                                    </p>
                                </div>
                                <div class="icon">
                                    
                                </div>
                                <a href="#" class="small-box-footer">
                                    More info <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div><!-- ./col -->                        
                    </div>                       
                    <div class="row">
                                            <div class="col-md-12">
                                                <div class="box box-primary">
                                                    <div class="box-header">
                                                        <i class="fa fa-fw fa-question-circle"></i>
                                                        <h3 class="box-title" id="accordion">FAQs</h3>
                                                    </div><!-- /.box-header -->
                                                    
                                                    <div class="box-body">

                                           <ul class = "trackAccordion">
	<li class = "proTrack">
	<a href="#" name="tabIndex0">THE THREE STEP PROCESS</a>
	</li>

	<li>
	<a href="#1" name="tabIndex1"><span class = "accoTrack"> REGISTERING SOCIAL MEDIAS </span> </a>
		<ul class = "tracks" id = "cert_Track">
		<li>Register Social Media Pages</li>
		<li>Register Websites</li>
		</ul>
	</li>

	<li> <!-- element which hold Individual class in accordian -->

	<a href="#2">SETTINGS</a>
	<ul class = "settings trackAccordion nestedElemAccor" >	
		
			<li class = "nestedElems">
				<a href="#5" class="begNestElem" name="tabIndex2_0">How to update brand logo and name</a> 
					<ul class="tracks" >
					<li>Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                                                                        sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
                                                                        quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</li>
					
					</ul>
			</li>

			<li class = "nestedElems"> 
			<a href="#4" class="intNestElem" name="tabIndex2_1">How to Update Users Profile/Roles/Permissions</a>
					<ul class="tracks">
					<li>Put some content</li>
					<li>Put some content</li>					
					</ul>
			</li>

			<li class = "nestedElems">
					<a href="#6" class="advNestElem" name="tabIndex2_2">How can I change my password</a> 
					<ul class="tracks">
					<li>Put some content</li>
					<li>Put some content</li>
					</ul>
			</li>
		
	</ul>
	</li> <!-- END element which hold Individual class in accordian -->

	<li>
	<a href="#">REWARDS</a>
		<ul class = "rewards trackAccordion nestedElemAccor" >	
         <li class = "nestedElems">
            <a href="#7" class="advNestElem" name="tabIndex3_0">Verifying campaign activitie</a> 
					<ul class="tracks">
					<li>Put some content</li>
					<li>Put some content</li>
					</ul>
		</li>		
		<li class = "nestedElems">
            <a href="#7" class="advNestElem" name="tabIndex3_1">Where can we get the list of pending activities</a> 
					<ul class="tracks">
					<li>Put some content</li>
					<li>Put some content</li>
					</ul>
		</li>
		</ul>
	</li>


	<li>
	<a href="#4">ACCOUNTS</a> 
	<ul class = "tracks" id = "more_Track">
	<li>How to Manage Your Wallet</li>
	<li>What are transactions</li>	
	</ul>
	</li>
</ul>
         
                                                        </div>


                                                </div><!-- /.box -->
                                            </div>
                                        </div>

                </section>               
             </ContentTemplate>     
       </asp:UpdatePanel>
     <script src="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "css/jquery-ui.css"%>" />
     
<script type="text/javascript">

    $(document).ready(function () {
        if (location.hash) {

            var tabIndex = window.location.hash.replace("#", ""); //parseInt(window.location.hash.substring(1));
            tabIndex.split("_");
            
            $("ul.trackAccordion").accordion({
                collapsible: true,
                active: parseInt(tabIndex[0]),
                heightStyle: "content"                
            });
            if (tabIndex.length > 1) {
                $("ul.nestedElemAccor").accordion({
                    collapsible: true,
                    active: parseInt(tabIndex[2]),
                    heightStyle: "content"
                });
            }

            var aTag = $("a[name='tabIndex" + tabIndex + "']");            
            $('html,body').animate({ scrollTop: aTag.offset().top }, 'slow');
            
        }
        else {
            $("ul.trackAccordion").accordion({
                collapsible: true,
                active: false,
                heightStyle: "content"
            });
        }
       
        $('.ui-trackAccordion').bind('accordionactivate', function (event, ui) {
            if (!ui.newHeader.length) { return; }
            ui.newHeader // jQuery object, activated header
            ui.oldHeader // jQuery object, previous header
            ui.newContent // jQuery object, activated content
            ui.oldContent // jQuery object, previous content

            $('html, body').animate({ scrollTop: $(ui.newHeader).offset().top }, 1);

        });
        
    });
</script> 
</asp:Content>
              
