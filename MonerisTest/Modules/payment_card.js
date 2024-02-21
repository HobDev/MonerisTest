
    export function tokenize()
    {
        var monFrameRef = document.getElementById('monerisFrame').contentWindow;
    monFrameRef.postMessage('tokenize','https://gatewayqa.moneris.com/HPPtoken/index.php');
    //change link according to table above
    return false;
        }
    var respMsg = function(e)
    {
        var respData = eval("(" + e.data + ")");
    document.getElementById("monerisResponse").innerHTML = e.origin + " SENT " + " - " +
    respData.responseCode + "-" + respData.dataKey + "-" + respData.errorMessage;
    document.getElementById("monerisFrame").style.display = 'none';
        }
    window.onload = function()
    {
        if (window.addEventListener)
    {
        window.addEventListener("message", respMsg, false);
        }
    else
    {
        if (window.attachEvent)
    {
        window.attachEvent("onmessage", respMsg);
        }
        }
        }
