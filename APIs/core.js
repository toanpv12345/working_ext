/**
 * Created by ToanPV on 10/20/2016.
 */

var dbhelper = require('./dbhelper');

function login(userName, passWord, otp, sourceId, key, ip, callback){
	checkSourceId(sourceId, key, ip, function(isValid){
		if(isValid){

		}
	});
}

function register(userName, passWord, sourceId, key, ip, callback){

}

function topup(userName, userId, money, serviceId, sourceId, key, ip, callback){

}

function charge(userName, userId, money, serviceId, sourceId, key, ip, callback){

}

function getUserInfo(userName, sourceId, key, ip, callback){

}

function checkSourceId(sourceId, key, ip, callback){

	callback(true);
}

module.exports.login = login;
module.exports.register = register;
module.exports.topup = topup;
module.exports.charge = charge;
module.exports.getUserInfo = getUserInfo;