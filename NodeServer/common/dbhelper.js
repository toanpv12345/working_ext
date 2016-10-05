/**
 * Created by ToanPV on 9/26/2016.
 */
var mssql = require('mssql');
var mongodb = require('mongodb');
var mongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var config = require('../common/config');
var logger = require('../common/utils');

var oracledb = require('oracledb');

var mongoDB = null;
var mssqlConnection = null;
var oracleConnection = null;

mongoClient.connect(config.dbconfigs.MONGODB_URL, function (err, db) {
	assert.equal(null, err);
	logger.logInfo('Connected to mongoDB.');
	mongoDB = db;
});

mssqlConnection = new mssql.Connection(config.dbconfigs.MSSQL_CONFIG, function (err) {
	if (err) {
		mssqlConnection = null;
		logger.logError('Connect to MSSQL error');
	}
	else {
		logger.logInfo('Connected to MSSQL');
	}
});

//oracledb.getConnection(config.dbconfigs.ORACLEDB_CONFIG, function (err, connection) {
//	if (err) {
//		logger.logError('Connect to Oracle: ' + err.message);
//		return;
//	}
//
//	logger.logInfo('Connected to Oracle');
//	oracleConnection = connection;
//	connection.execute("select * from mediation_owner.tcoc_connection_server where toll_id = 500", function (err, result) {
//		if (err) {
//			console.error(err.message);
//			return;
//		}
//		console.log(result.rows);
//	});
//});


var checkOTPAccount = function (userName, cb) {
	if (mongoDB != null && userName != null && userName != '') {
		var collection = mongoDB.collection('OTPAccount');
		collection.find({UserName: {$eq: userName}}, {_id: 0}).toArray(function (err, docs) {
			if (err) {
				cb(null);
				return;
			}
			logger.logInfo('mongo query result: ' + JSON.stringify(docs));
			if (docs.length > 0)
				cb(docs[0]);
			else
				cb(null);
		});
	}
}

var checkAccount = function (userName, cb) {
	if (mssqlConnection != null && userName != null && userName != '') {
		var request = new mssql.Request(mssqlConnection);
		request.query("SELECT * FROM [VETC.FE.Report].[dbo].[Account] where AccountName = '" + userName + "'", function (err, data) {
			if (err) {
				logger.logInfo('mssql query error: ' + err);
				cb(null);
			}
			logger.logInfo('mssql query: ' + JSON.stringify(data));

			if(data.length > 0)
				cb(data[0]);
			else
				cb(null);
		});
	}
}

module.exports.checkAccount = checkAccount;
module.exports.checkOTPAccount = checkOTPAccount;