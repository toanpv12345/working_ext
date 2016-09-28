/**
 * Created by ToanPV on 9/26/2016.
 */
var mssql = require('mssql');
var mongodb = require('mongodb');
var mongoClient = require('mongodb').MongoClient;
var assert = require('assert');

var mongoUrl = 'mongodb://localhost:27017/MyMongoDB';
var mongoCallback = null;

var config = require('./config');

var connectToMsSql = function(config, callback){
	if(config != null && config != undefined && callback != null){
		mssql.connect(config, function (err) {
			if (err) {
				console.log(err);
				callback(err);
			}
			else{
				var str = 'Connected to mssql';
				console.log(str);
				callback(str);
			}
		});
	}
}

var connectToMongoDB = function(url, callback){
	if(url != null && url != undefined && callback != null) {
		mongoUrl = url;
		mongoCallback = callback;
	}
	else{
		console.log('invalid params');
	}
};

var connectDB = function(config, url, callback){
	connectToMsSql(config, function(err){callback(err);});
	connectToMongoDB(url, function(err){callback(err);});
};

var getOTPAccount = function(userName, cb){
	mongoClient.connect(config.dbconfigs.MONGODB_URL, function(err, db) {
		assert.equal(null, err);
		console.log('Connected to mongo server.');
		var collection = db.collection('OTPAccount');
		// Find some documents
		collection.find({UserName:{$eq:userName}}, {_id:0}).toArray(function (err, docs) {
			if(err) {
				cb(null);
				return;
			}
			console.log(docs);
			if(docs.length > 0)
				cb(docs[0]);
			else
				cb(null);
		});
		db.close();
	});
};

module.exports.connectDB = connectDB;
module.exports.getOTPAccount = getOTPAccount;