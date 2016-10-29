/**
 * Created by ToanPV on 10/24/2016.
 */

var http = require('http');

var options = {
	hostname: 'api.local',
	port: 80,
	path: '',
	method: 'POST',
	headers: {
		'Content-Type': 'application/json'
	}
};

process.argv.forEach(function (val, index, array) {
	if(val == 'login'){
		login();
	}
	else if(val == 'post') {
		post();
	}
});

function login(){
	console.log('login called');
	var postData = JSON.stringify({
		'userName': 'userName',
		'passWord': 'passWord',
		'otp':'otp',
		'sourceId':1212,
		'key':'dwdwdwdwdw'
	});
	options.path = '/api';

	var req = http.request(options, function (res) {
		console.log('STATUS: ' + res.statusCode);
		res.on('data', function (chunk) {
			console.log('BODY: ' + chunk);
		});
		res.on('end', function () {
			console.log('No more data in response.')
		})
	});

	req.on('error', function (e) {
		console.log('problem with request: ' + e.message);
	});
	req.write(postData);
	req.end();
}

function post(){
	console.log('post called');
	options.path = '/api/post';

	var postData = JSON.stringify({
		'userName': 'userName',
		'passWord': 'passWord',
		'otp':'otp',
		'sourceId':1212,
		'key':'dwdwdwdwdw'
	});

	var req = http.request(options, function (res) {
		console.log('STATUS: ' + res.statusCode);
		res.on('data', function (chunk) {
			console.log('BODY: ' + chunk);
		});
		res.on('end', function () {
			console.log('No more data in response.')
		})
	});

	req.on('error', function (e) {
		console.log('problem with request: ' + e.message);
	});
	req.write(postData);
	req.end();
}
