function doMonerisSubmit() {
    var monFrameRef = document.getElementById('monerisFrame').contentWindow;
    monFrameRef.postMessage('tokenize', 'https://esqa.moneris.com/HPPtoken/index.php');
    //change link according to table above
    return false;
}
var respMsg = function (e) {
    var respData = eval("(" + e.data + ")");
   document.getElementById("monerisResponse").innerHTML = " Response Code: " +
       respData.responseCode + " Token: " + respData.dataKey + " Bind: " + respData.bin + " Error Message: " + respData.errorMessage;  
    document.getElementById("monerisFrame").style.display = 'none';

    HybridWebView.SendRawMessageToDotNet(respData.dataKey);
    HybridWebView.SendInvokeMessageToDotNet("CallMeFromScript", [respData.dataKey, respData.bin]);
}
window.onload = function () {
    if (window.addEventListener) {
        window.addEventListener("message", respMsg, false);
    }
    else {
        if (window.attachEvent) {
            window.attachEvent("onmessage", respMsg);
        }
    }
}