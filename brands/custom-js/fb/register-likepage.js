// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {

    console.log('statusChangeCallback');
    console.log(response);
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        testAPI();
    } else if (response.status === 'not_authorized') {
        // The person is logged into Facebook, but not your app.
        document.getElementById('status').innerHTML = 'Please log ' +
          'in using Facebook account.';
    } else {
        // The person is not logged into Facebook, so we're not sure if
        // they are logged into this app or not.
        document.getElementById('status').innerHTML = 'Please log ' +
          'in using Facebook account.';
    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '705570672870076',
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.2' // use version 2.1
    });

    // Now that we've initialized the JavaScript SDK, we call 
    // FB.getLoginStatus().  This function gets the state of the
    // person visiting this page and can return one of three states to
    // the callback you provide.  They can be:
    //
    // 1. Logged into your app ('connected')
    // 2. Logged into Facebook, but not your app ('not_authorized')
    // 3. Not logged into Facebook and can't tell if they are logged into
    //    your app or not.
    //
    // These three cases are handled in the callback function.

    /*
    FB.getLoginStatus(function (response) {
    statusChangeCallback(response);
    });
    */

};
var d1;
var s1;
var id1;
// Load the SDK asynchronously
(function (d, s, id) {
    var js, js2, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
    

    js2 = d.createElement(s); js2.id = id;
    js2.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&appId=705570672870076&version=v2.0";
    fjs.parentNode.insertBefore(js2, fjs);  


    

} (document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function testAPI() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', function (response) {
        console.log('Successful registration for: ' + response.name);
        document.getElementById('status').innerHTML =
          'Registering in, ' + response.name + '.., please wait';
        
        if (response.verified = false) {
            alert("You have not completed your facebook verification process");
            return;
        }

        GetPhoto(response);
    });
}
function GetPhoto(response) {


    FB.api(
    "/me/picture",
    {
        "redirect": false,
        "height": "200",
        "type": "normal",
        "width": "200"
    },
    function (response1) {
        if (response1 && !response1.error) {

            GetNoOfFriends(response1, response);            
        }
    });

}
function GetNoOfFriends(response1,response) {


    FB.api(
    "/me/friends",
    function (response2) {
        if (response2 && !response2.error) {

            var friends_ids = ""; var and_op = "";
            // loop friend list
            if (response2.data) {                
                $.each(response2.data, function (index, friend) {
                    friends_ids += and_op + friend.id;
                    and_op = ",";
                    //alert(friend.name + ' has id:' + friend.id);
                });
            }
            
            var friends;
            if (typeof response2.summary == 'undefined') {
                friends = 0;
            }
            else {
                friends = response2.summary.total_count;
            }

            var access_token = FB.getAuthResponse()['accessToken'];

            /* handle the result */
            // alert(response1.data.url);
            var _FBLoginDetails = {
                Name: response.name,
                FName: response.first_name,
                LName: response.last_name,
                ID: response.id,
                Gender: response.gender,
                Email: response.email,
                ProfileImageUrl: response1.data.url,
                NoOfFriends: friends,
                FriendsList: friends_ids,
                ProfileUrl: response.link,
                AccessToken: access_token
            };
            
           PageMethods.RegisterUser(_FBLoginDetails, UpdateLoginSuccess);
        }
    }
);

}
function RegisterToDB(response) {
    var _FBLoginDetails = {
        Name: response.name,
        FName: response.first_name,
        LName: response.last_name,
        ID: response.id,
        Gender: response.gender,
        Email: response.email
    };
    PageMethods.RegisterUser(_FBLoginDetails, UpdateLoginSuccess);
}
function UpdateLoginSuccess(response) {

    debugger
    if (response.Message == "ALREADYREGISTERED") {
        window.location.href = PageRedirectUrl + response.LoginSuccessRedirectHomePage;
    }
    else if (response.Message == "SUCCESS") {
        GiveLikePagePermission();
    }
    else {
        document.getElementById('status').innerHTML =
          response.Message;
    }
}


function GiveLikePagePermission() {

    $("#facebookregisterbtn").hide();

    $("#facebooklikebtn").show();

    FB.Event.subscribe('edge.create', page_like_or_unlike_callback);
    /*FB.Event.subscribe('edge.create', function(href, widget) {
                                    alert("Like button clicked");
                                       });*/
}

var page_like_or_unlike_callback = function(url, html_element) {
 var flag=1;
  PageMethods.RegisterLikeEvent(flag, UpdateLikeSuccess);
}

function UpdateLikeSuccess(response)
{
//alert(response);
   window.location.href = PageRedirectUrl + response;
}
function testAPI1() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', function (response) {

        console.log('Successful login for: ' + response.name);
        document.getElementById('status').innerHTML =
          'Thanks for logging in, ' + response.name + '!';
    });
}