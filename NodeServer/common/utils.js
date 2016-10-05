/**
 * Created by ToanPV on 9/30/2016.
 */

exports.getClientIP = function(clientResquest){
	return clientResquest.ip;
}

exports.isCaptchaValid = function(captchaText, captchaVerify){

}

exports.logInfo = function(log){
	console.log(log);
}

exports.logError = function(log){
	console.log(log);
}

exports.errorMessage = function(code, message){
	var res = {
		code: code,
		message: message
	};
	return res;
}
