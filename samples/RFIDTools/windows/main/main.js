var keys = require('electron').remote.getGlobal('keys');
var io;
var sha1 = require('sha1');
var md5 = require('md5');

window.$ = require('jquery');

var core = {
  init: function(){
    var port = $('#listen-port').val();
    io = require('socket.io').listen(port);

    inventoryModule.init();
    readModule.init();
  },
  pad: function(n,w,z){
    z = z || '0';
    n = n + '';
    return n.length >= w ? n : n + new Array(w - n.length + 1).join(z);
  },
  hexToChars: function(c){
    var a = new Array();
  	for (var i = 0; i < c.length; i += 2)
    	a.push(parseInt(c.substring(i, i + 2), 16));
  	return a;
  },
  simpleAction: function(a) {
    io.emit(a, {});
  }
};

$(function(){
  core.init();
});
