var express = require('express');
var router = express.Router();
var assert = require('assert');
var dbhelper = require('../database/dbhelper');
var httpStatusCode = require('http-status-codes');
var messageStr = require('../message');

var mssql = require('mssql');

module.exports = router;

/* GET users listing. */
router.get('/login', function (req, res, next) {
	var userName = req.query.userName;
	var key = req.query.key;
	var hardKey = req.query.hardKey;

	if(userName == undefined || userName == '' || key == undefined || key == '' || hardKey == undefined || hardKey == ''){
		res.status(httpStatusCode.BAD_REQUEST).send(messageStr.USER_NAME_OR_PASS_INVALID);
		return;
	}

	dbhelper.getOTPAccount(userName, key, function(data){
		if(data != null){
			console.log('AccountDB: ' + JSON.stringify(data));
			if(data.UserName == userName){
				res.status(httpStatusCode.OK).send({"UserId":data.UserId,"UserName":data.UserName,"OTPStatus":data.OTPStatus, "TimeServer": Date.now()});
				return;
			}
		}
		res.status(httpStatusCode.BAD_REQUEST).send(messageStr.USER_NAME_OR_PASS_INVALID);
	});
});

router.get('/mssql', function(req, res){
	console.log('mssql');
	var sql = require("mssql");

	// config for your database
	var config = {
		user: 'sa',
		password: 'Vetc@123456',
		server: 'localhost',
		database: 'VETC.FE.Report'
	};

	// connect to your database
	sql.connect(config, function (err) {
		if (err)
			console.log(err);

		// create Request object
		var request = new sql.Request();

		// query to the database and get the records
		request.query('select * from [VETC.FE.Report].[dbo].[Account]', function (err, recordset) {

			if (err) console.log(err)

			// send records as a response
			res.send(recordset);

		});
	});
});

router.get('/mongodb', function(req, res){
	console.log('mongodb');

	var url = 'mongodb://localhost:27017/MyMongoDB';
	var MongoClient = require('mongodb').MongoClient;

	MongoClient.connect(url, function(err, db) {
		assert.equal(null, err);
		console.log("Connected correctly to server.");
		findDocuments(db, 'Accounts', function(db_res) {
			res.send(db_res)
			db.close();
		});
		//db.close();
	});
});

var getClientIp = function(req){
	return req.ip;
}

var verifyCaptcha = function(captcha, verify){
	if(captcha == null || captcha == '' || verify == null || verify == '') {
		return false;
	}
	return true;
}

var findDocuments = function (db, collectionName, callback) {
	// Get the documents collection
	var collection = db.collection(collectionName);
	// Find some documents
	collection.find({}, {_id:false}).toArray(function (err, docs) {
		assert.equal(err, null);
		console.log(docs)
		callback(docs);
	});
}

