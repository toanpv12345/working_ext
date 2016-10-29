/**
 * Created by ToanPV on 10/20/2016.
 */
var fs = require('fs');

function logInfo(log){
	console.log(log);
}

function logError(log){
	console.log(log);
}

module.exports.logInfo = logInfo;
module.exports.logError = logError;
