/**
 * Created by ToanPV on 9/28/2016.
 */

var errorCodes = {};

exports.getErrorText = function(errorCode){
	if (errorCodes.hasOwnProperty(errorCode)) {
		return errorCodes[errorCode];
	} else {
		throw new Error("Status code does not exist: " + errorCode);
	}
}

errorCodes[exports.USER_NAME_OR_PASS_INVALID = -1] = 'Tên tài khoản hoặc mật khẩu không đúng!';
errorCodes[exports.USER_NOT_EXISTED = -2] = 'Tài khoản không tồn tại!';