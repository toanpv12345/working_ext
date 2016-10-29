/**
 * Created by ToanPV on 10/24/2016.
 */

function getRequestIP(req){
	var ip = req.ip ||
		req.headers['x-forwarded-for'] ||
		req.connection.remoteAddress ||
		req.socket.remoteAddress ||
		req.connection.socket.remoteAddress;
	return ip;
}

module.exports.getRequestIP = getRequestIP;