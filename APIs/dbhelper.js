/**
 * Created by ToanPV on 10/20/2016.
 */

var logger = require('./common/logger');
var mongodbConnection = require('./db/mongodbConnection');
var sqlConnection = require('./db/sqlConnection');

module.exports.checkSourceId = function(sourceId, key, ip, callback){
	var isOK = false;
	if(sourceId <= 0 || key == '' || ip == '') {
		isOK = false;
	}

	mongodbConnection.findDocument('ServiceConnection', {}, function(result){

	});
}
