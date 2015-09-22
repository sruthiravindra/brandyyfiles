// JavaScript Document

function numbersonly(myfield, e, dec)
{   
        var key;
        var keychar;
        if (window.event)
            key = window.event.keyCode;
        else if (e)
            key = e.which;
        else
            return true;
        keychar = String.fromCharCode(key);

        // control keys
        if ((key==null) || (key==0) || (key==8) || 
            (key==9) || (key==13) || (key==27) )
        return true;

        // numbers
        else if ((("0123456789").indexOf(keychar) > -1))
        {
            
            return true;
            
        }
        // decimal point jump
        else if (dec && (keychar == "."))
        {
            myfield.form.elements[dec].focus();
            return false;
        }
        else
            return false;
        
 }
 function submitButton(e)
 {
	if(e.keyCode==13)
	{
		loginCheck();
	}
 }
 
 
 function numbersonlyDot(myfield, e, dec)
{   
       var dot=myfield.value.indexOf(".");		
        var key;
        var keychar;
        if (window.event)
            key = window.event.keyCode;
        else if (e)
            key = e.which;
        else
            return true;
        keychar = String.fromCharCode(key);
		if(key==46 && dot>=0)
		{
			return false;
		}
        // control keys
        if ((key==null) || (key==0) || (key==8) || 
            (key==9) || (key==13) || (key==27) )
        return true;

        // numbers
        else if ((("0123456789.").indexOf(keychar) > -1))
        {
            
            return true;
            
        }
        // decimal point jump
        else if (dec && (keychar == "."))
        {
            myfield.form.elements[dec].focus();
            return false;
        }
        else
            return false;        
 }
function nothingType(myfield, e, dec)
{   
        var key;
        var keychar;
        if (window.event)
            key = window.event.keyCode;
        else if (e)
            key = e.which;
        else
            return true;
        keychar = String.fromCharCode(key);

        // control keys
        if ((key==null) || (key==0) || (key==8) || 
            (key==9) || (key==13) || (key==27) )
        return true;

        // numbers
        else if ((("0123456789").indexOf(keychar) > -1))
        {
            
            return false;
            
        }
        // decimal point jump
        else if (dec && (keychar == "."))
        {
            myfield.form.elements[dec].focus();
            return false;
        }
        else
            return false;
        
 }
