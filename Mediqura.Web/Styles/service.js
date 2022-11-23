
//+++++++++++++++++++++++++++++++++++ COMMON AJAX CALL FUNCTIONS ++++++++++++++++++++++++++++++++++++++++++++//

function callServiceToFetchData(url, callBackFunctionName){
	var xmlHttp = GetXmlHttpObject();
	if (xmlHttp != null) {
		try {
			xmlHttp.onreadystatechange = function() {
			if(xmlHttp.readyState == 4) {
				if(xmlHttp.responseText != null){
					callBackFunctionName(xmlHttp.responseText);
				}else{
					alert("Error");
				}
			}
		};
		xmlHttp.open("GET", url, true);
		xmlHttp.send(null);
	}
	catch(error) {}
	}
}

//Returns the XMLHttpReqest object
function GetXmlHttpObject() {
	var xmlHttp=null;
	try {
		xmlHttp=new XMLHttpRequest();
	}
	catch (e) {
		try {
			xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
		}
		catch (e) {
			xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
		}
	}
	return xmlHttp;
}