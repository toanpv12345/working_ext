var inventoryModule = {
  items: [],
  totalTags: 0,
  updateTotalTags: function(){
    $('.total-tags').html(inventoryModule.totalTags);
  },
  init: function(){
    $('#inventory-button').prop('disabled', false);
    $('#clear-button').prop('disabled', false);
    $('#mask-button').prop('disabled', false);

    $('#inventory-button').click(function(){ inventoryModule.inventory(); });
    $('#clear-button').click(function(){
      core.simpleAction('inventory_clear');
      totalTags = 0;
      inventoryModule.updateTotalTags();
    });
    $('#mask-button').click(function(){ core.simpleAction('inventory_mask'); });

    io.sockets.on('connection', function(socket) {
      socket.on('inventory_start', function(data){
        $('#inventory-button').text('Inventory');
        $('#clear-button').prop('disabled', false);
        $('#mask-button').prop('disabled', false);
      });
      socket.on('inventory_stop', function(data){
        $('#inventory-button').text('Stop');
        $('#clear-button').prop('disabled', true);
        $('#mask-button').prop('disabled', true);
      });
      socket.on('inventory_result', function(data) {
        inventoryModule.items = JSON.parse(data);
        inventoryModule.rebuildTable();
      });
    });
  },
  rebuildTable: function(){
    var temp = '';
    inventoryModule.totalTags = 0;

    for(var i in inventoryModule.items){
      var tag = inventoryModule.items[i].mTag;
      var count = inventoryModule.items[i].mCount;

      inventoryModule.totalTags += 1;

      if ($('#display-pc').prop('checked')) tag = tag.substring(4);

      temp += '<tr><td class="mdl-data-table__cell--non-numeric">' + tag + '</td><td>' + count + '</td></tr>';
    }

    $('#epc-list').html(temp);
    inventoryModule.updateTotalTags();
  },
  inventory: function(){
    io.emit('inventory', {
      start: $('#inventory-button').text() == 'Inventory',
      displayPC: $('#display-pc').prop('checked'),
      continueMode: $('#continue-mode').prop('checked'),
      reportRssi: $('#report-rssi').prop('checked'),
      powerGain: $('#power-gain').val(),
      operationTime: $('#operation-time').val()
    });
  }
};
