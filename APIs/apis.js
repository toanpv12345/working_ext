var express = require('express');
var core = require('./core');
var httpStatusCode = require('./common/httpStatusCode');
var message = require('./common/message');
var logger = require('./common/logger');

var apis = express();

module.exports = apis;

apis.post('/login', function (req, res) {
	logger.logInfo('login');
	var userName = req.body.userName;
	var passWord = req.body.passWord;
	var otp = req.body.otp;
	var sourceId = req.body.sourceId;
	var key = req.body.key;

	if(isUserAndPassWordValid(userName, passWord)){
		core.login(userName, passWord, otp, sourceId, key, function(userInfo){
			if(userInfo != null){
				return res.status(httpStatusCode.OK).json(userInfo);
			}
			return res.status(httpStatusCode.BAD_REQUEST).send(message.USER_OR_PASSWORD_INVALID);
		});
	}
	return res.status(httpStatusCode.BAD_REQUEST).send(message.USER_OR_PASSWORD_INVALID);
});

apis.post('/register', function (req, res) {
	var userName = req.body.userName;
	var passWord = req.body.passWord;
	var sourceId = req.body.sourceId;
	var key = req.body.key;

	if(isUserAndPassWordValid(userName, passWord)){

	}
});

apis.post('/topup', function (req, res) {
	var userName = req.body.userName;
	var userId = req.body.userId;
	var money = req.body.money;
	var serviceId = req.body.serviceId;
	var sourceId = req.body.sourceId;
	var key = req.body.key;

	if(isUserAndPassWordValid(userName, passWord)){

	}
});

apis.post('/charge', function (req, res) {
	var userName = req.body.userName;
	var userId = req.body.userId;
	var money = req.body.money;
	var serviceId = req.body.serviceId;
	var sourceId = req.body.sourceId;
	var key = req.body.key;

	if(isUserAndPassWordValid(userName, passWord)){

	}
});

apis.post('/getUserInfo', function (req, res) {
	var userName = req.body.userName;
	var sourceId = req.body.sourceId;
	var key = req.body.key;
});

function isUserAndPassWordValid(userName, pass){
	if(userName == null || userName == undefined || userName == '' || userName.length < 3)
		return false;

	if(pass == null || pass == undefined || pass == '' || pass.length < 6)
		return false;

	return true;
}