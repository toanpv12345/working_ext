/**
 * Created by ToanPV on 10/5/2016.
 */

function OTPAccount(accountId, accountName, nickName, otpStatus, otpType, mobile, hardKey) {
	this.AccountId = accountId;
	this.AccountName = accountName;
	this.NickName = nickName;
	this.OtpStatus = otpStatus;
	this.OtpType = otpType;
	this.Mobile = mobile;
	this.Hardkey = hardKey;
}

function replacer(key,value) {
	if (key=="privateProperty1") return undefined;
	else if (key=="privateProperty2") return undefined;
	else return value;
}

OTPAccount.prototype.toJSON = function(){

};
module.exports.OTPAccount = OTPAccount;