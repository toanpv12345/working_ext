const electron = require('electron');
const {app} = electron;
const {BrowserWindow} = electron;
var path = require('path');

var fs = require('fs');

let win;

function init() {
  loadKeyFile();
  createWindow();
}

function createWindow() {
  win = new BrowserWindow({ width: 880, height: 960, resizable: false });
  win.loadURL('file://' + __dirname + '/windows/main/main.html');
  win.webContents.openDevTools();
  win.setMenu(null);
  win.on('closed', () => {
    win = null;
  });
}

function loadKeyFile() {
  fs.readFile(path.join(__dirname, 'keyfile'), 'utf-8', function(e, d){
    if (e) throw e;
    global.keys = d.split('\n');
  });
}

app.on('ready', init);
app.on('window-all-closed', () => { if (process.platform !== 'darwin') { app.quit();  }});
app.on('activate', () => { if (win === null) { createWindow(); }});
