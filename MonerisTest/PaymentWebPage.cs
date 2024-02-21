namespace MonerisTest;

public class PaymentWebPage : ContentPage
{
	public PaymentWebPage()
	{
		Content = new WebView
		{
			Source= new HtmlWebViewSource
			{
				Html= @"<html>
                       <head>
<title> Outer Frame - Merchant Page</title>
<script>
function doMonerisSubmit()
{
var monFrameRef = document.getElementById('monerisFrame').contentWindow;
monFrameRef.postMessage('tokenize','https://gatewayqa.moneris.com/HPPtoken/index.php');
//change link according to table above
return false;
}
var respMsg = function(e)
{
var respData = eval(""("" + e.data + "")"");
document.getElementById(""monerisResponse"").innerHTML = e.origin + "" SENT "" + "" - "" +
respData.responseCode + ""-"" + respData.dataKey + ""-"" + respData.errorMessage;
document.getElementById(""monerisFrame"").style.display = 'none';
}
window.onload = function()
{
if (window.addEventListener)
{
window.addEventListener (""message"", respMsg, false);
}
else
{
if (window.attachEvent)
{
window.attachEvent(""onmessage"", respMsg);
}
}
}
</script>
</head>
<body>
<div>This is the outer page</div>
<div id=monerisResponse></div>
<iframe id=monerisFrame
src=https://gatewayqa.moneris.com/HPPtoken/index.php?id=htFTMQ8J63EYNZS&pmmsg=true&css_
body=background:green;&css_textbox=border-width:2px;&css_textbox_pan=width:140px;&enable_
exp=1&css_textbox_exp=width:40px;&enable_cvd=1&css_textbox_cvd=width:40px&enable_exp_
formatting=1&enable_cc_formatting=1 frameborder='0' width=""200px"" height=""200px""></iframe>
<input type=button onClick=doMonerisSubmit() value=""submit iframe"">
</body> "
            }
		};
	}
}