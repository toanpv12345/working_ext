/**
 * Created by ToanPV on 10/28/2016.
 */
var mssql = require('mssql');
var config = require('./common/config');
var logger = require('./common/logger');

var sqldb = null;
sqldb = new mssql.Connection(config.dbconfigs.MSSQL_CONFIG, function (err) {
	if (err) {
		sqldb = null;
		logger.logError('Connect to MSSQL error');
	}
	else {
		logger.logInfo('Connected to MSSQL');
	}
});