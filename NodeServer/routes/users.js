var express = require('express');
var router = express.Router();
var dbhelper = require('../common/dbhelper');
var httpStatusCode = require('http-status-codes');
var messageStr = require('../common/message');
var account = require('../common/account');

//var addon = require('../build/Release/addon');

module.exports = router;

/* GET users listing. */
router.get('/login', function (req, res, next) {
	var userName = req.query.userName;
	var key = req.query.key;
	var hardKey = req.query.hardKey;

	var otpAccount = new account.OTPAccount(121212, 'accountName', 'nickName', 1, 1, '11313113', '12212gggeg');
	console.log('otpAcc: ' + JSON.stringify(otpAccount));

	//console.log('addon: ' + addon.hello());
	if(userName == undefined || userName == '' || key == undefined || key == '' || hardKey == undefined || hardKey == ''){
		return res.status(httpStatusCode.BAD_REQUEST).send(messageStr.USER_NAME_OR_PASS_INVALID);
	}

	dbhelper.checkOTPAccount(userName, function(data){
		if(data != null){
			if(data.UserName == userName && data.PassWord == key){
				return res.status(httpStatusCode.OK).send({"UserId":data.UserId,"UserName":data.UserName,"OTPStatus":data.OTPStatus, "TimeServer": Date.now()});
			}
			return res.status(httpStatusCode.BAD_REQUEST).send({code:messageStr.USER_NAME_OR_PASS_INVALID, message: messageStr.getErrorText(messageStr.USER_NAME_OR_PASS_INVALID)});
		}

		// Check Account in AccountDB
		dbhelper.checkAccount(userName, function(data){
			if(data != null) {
				if(data.UserName == userName && data.PassWord == key){
					return res.status(httpStatusCode.OK).send({"UserId":data.AccountID,"UserName":data.AccountName,"OTPStatus":0, "TimeServer": Date.now()});
				}
				return res.status(httpStatusCode.BAD_REQUEST).send(
					{
						code: messageStr.USER_NAME_OR_PASS_INVALID,
						message: messageStr.getErrorText(messageStr.USER_NAME_OR_PASS_INVALID)
					});
			}
		});
		return res.status(httpStatusCode.BAD_REQUEST).send({code: messageStr.USER_NOT_EXISTED, message: messageStr.getErrorText(messageStr.USER_NOT_EXISTED)});
	});
});

