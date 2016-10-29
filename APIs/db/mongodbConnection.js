/**
 * Created by ToanPV on 10/28/2016.
 */
var MongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var logger = require('../common/logger');

var mongodb = null;

MongoClient.connect(config.dbconfigs.MONGODB_URL, function(error, db){
	assert(null, error);
	logger.logInfo('Connected successfully to MongoDB');
	mongodb = db;
});

module.exports.insertDocument = function(collection, data, callback) {
	assert.notEqual(mongodb, null);
	// Get the documents collection
	var coll = mongodb.collection(collection);
	// Insert some documents
	coll.insertOne(data, function(err, result) {
		assert.equal(err, null);
		assert.equal(1, result.result.n);
		assert.equal(1, result.ops.length);
		logger.logInfo('Inserted:' + data + ' into the ' + collection);
		callback(result);
	});
}

module.exports.findDocument = function(collection, query, callback) {
	// Get the documents collection
	var coll = mongodb.collection(collection);
	// Find some documents
	coll.find(query).toArray(function(err, docs) {
		assert.equal(err, null);
		logger.logInfo('findDocument: ' + docs);
		callback(docs);
	});
}

module.exports.updateDocument = function(collection, query, callback) {
	// Get the documents collection
	var coll = mongodb.collection(collection);
	// Update document where a is 2, set b equal to 1
	//coll.updateOne({ a : 2 }, { $set: { b : 1 } }, function(err, result) {
	//		assert.equal(err, null);
	//		assert.equal(1, result.result.n);
	//		logger.logInfo(result);
	//		callback(result);
	//	});

	coll.updateOne(query, function(err, result) {
		assert.equal(err, null);
		assert.equal(1, result.result.n);
		logger.logInfo('updateDocument: ' + result);
		callback(result);
	});
}

module.exports.removeDocument = function(collection, query, callback) {
	// Get the documents collection
	var coll = mongodb.collection(collection);
	// Insert some documents
	//coll.deleteOne({ a : 3 }, function(err, result) {
	//	assert.equal(err, null);
	//	assert.equal(1, result.result.n);
	//	console.log("Removed the document with the field a equal to 3");
	//	callback(result);
	//});

	coll.deleteOne(query, function(err, result) {
		assert.equal(err, null);
		assert.equal(1, result.result.n);
		logger.logInfo('removeDocument: ' + result);
		callback(result);
	});
}

module.exports.indexCollection = function(collection, query, callback) {
	mongodb.collection(collection).createIndex(query, null, function(err, results) {
			logger.logInfo('indexCollection: ' + result);
			callback();
		}
	);
};
