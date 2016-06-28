var readModule = {
  bankDialog: {},
  selectedBank: 1,
  i: -1,
  o: -1,
  delay: 1500,
  recentEPC: '',
  currentFunction: null,
  continueMode: false,
  validatedItems: [],
  init: function(){
    readModule.bankDialog = document.querySelector('#bank-dialog');

    $('#bank-button').click(readModule.showBankDialog);
    $('.close-bank-dialog').click(readModule.closeBankDialog);
    $('.bank-option').click(readModule.onBankSelected);
    $('#read-simple-display').click(readModule.toggleReadMode);
    $('#read-button').click(function(){
      var t = $(this).text();
      $('#read-continue-mode').prop('checked', false);
      readModule.validatedItems = [];

      if (t == 'Read'){
        $(this).text('Stop');
        readModule.toggleControls(true, 'read-button');
        readModule.read();
      }
      else {
        $(this).text('Read');
        readModule.toggleControls(false, 'read-button');
        readModule.stop();
      }
    });
    $('#validate-button').click(function(){
      var t = $(this).text();

      if (t == 'Validate'){
        $(this).text('Stop');
        readModule.toggleControls(true, 'validate-button');
        readModule.validate();
      }
      else{
        $(this).text('Validate');
        readModule.toggleControls(false);
        clearInterval(readModule.i);
        readModule.stop();
      }
    });

    $('#read-continue-mode').click(function(){
      readModule.continueMode = $(this).prop('checked');
      if (readModule.continueMode){
          $('.validated-values').show();
      }
      else {
        $('.validated-values').hide();
        clearInterval(readModule.i);
        readModule.validatedItems = [];
      }
    });

    $('#read-clear-button').click(readModule.clear);
    $('#read-mask-button').click(readModule.mask);

    io.sockets.on('connection', function(socket) {
      socket.on('read_memory_result', function(e,d,v){
        $('.read-epc-result').html(e);

        if (d == ''){
          $('.read-result').attr('class', 'read-result error').html('ERROR');
          $('.read-memory-result').html('');
          readModule.storeValidatedItem(e, 'Invalid', '0000', '0000');
          if (!readModule.continueMode){
            readModule.toggleControls(false);
            $('#validate-button').text('Validate');
            $('#read-button').text('Read');
          }
          return;
        }
        else{
          if (!v && !readModule.continueMode) $('.read-result').attr('class', 'read-result ok').html('SUCCESS');
          $('.read-memory-result').html(d);
        }

        if (v){
          var epc = e.substring(4);
          var keyIndex = parseInt(epc.substring(10, 12), 16);
          var key = keys[keyIndex].replace(/[^A-Z0-9]/, '');
          var combo = key + d;
          var comboInBytes = core.hexToChars(combo);
          var hash1 = sha1(Buffer.from(comboInBytes)).toUpperCase();
          var accessPassword = hash1.substring(16, 24);
          var hash2 = md5(hash1 + d);
          var validation = hash2.substring(18, 22).toUpperCase();

          readModule.action('read_memory', true, accessPassword);
        }
        else {
          var temp = '';

          for(var i = 0, index = 0; i < d.length; i+= 16) {
            var sub = core.pad(d.substring(16 * index, 16 * (index + 1)), 16, '0');
            temp += '<tr><td class="aleft"><strong>Block ' + ++index + '</strong></td>';

            for(var j = 0; j < sub.length; j+= 4){
              temp += '<td>' + sub.substring(j, j + 4) + '</td>';
            }
          }

          $('.read-result').attr('class', 'read-result ok').html('SUCCESS');
          $('#read-memory-values').html(temp);

          clearTimeout(readModule.o);

          if ($('#read-continue-mode').prop('checked')){
            if (readModule.i == -1) readModule.i = setInterval(readModule.currentFunction, readModule.delay);
            readModule.storeValidatedItem(e, 'Valid', d.substring(0, 4), d.substring(16, 20));
          }
          else {
            clearInterval(readModule.i);
            readModule.toggleControls(false);
            $('#validate-button').text('Validate');
            $('#read-button').text('Read');
          }
        }
      });
    });
  },
  storeValidatedItem: function(e,s,b1,b2){
    var exist = false;

    for(var i = 0; i < readModule.validatedItems.length; i++){
      var item = readModule.validatedItems[i];

      if (item.epc == e) {
        if (item.status == 'Invalid' && s == 'Valid'){
          readModule.validatedItems[i].status = s;
          readModule.validatedItems[i].block1 = b1;
          readModule.validatedItems[i].block2 = b2;
        }
        exist = true;
        break;
      }
    }

    if (!exist) readModule.validatedItems.push({ epc: e, status: s, block1: b1, block2: b2 });

    var temp = '';

    for(var i = 0; i < readModule.validatedItems.length; i++){
      var item = readModule.validatedItems[i];
      temp += '<tr><td class="aleft">' + item.epc + '</td><td class="' + item.status.toLowerCase() + '">' + item.status + '</td><td>' + item.block1 + '</td><td>' + item.block2 + '</td></tr>';
    }

    $('#read-memory-validated-values').html(temp);
  },
  toggleControls: function(e,ex){
    $('#bank-button').prop('disabled', e);
    $('#read-button').prop('disabled', e);
    $('#validate-button').prop('disabled', e);
    $('#read-clear-button').prop('disabled', e);
    $('#read-mask-button').prop('disabled', e);
    $('#read-continue-mode').prop('disabled', e);

    $('#read-offset').prop('disabled', e);
    $('#read-length').prop('disabled', e);
    $('#read-password').prop('disabled', e);
    $('#read-power-gain').prop('disabled', e);
    $('#read-operation-time').prop('disabled', e);

    if (ex) $('#' + ex).prop('disabled', false);
  },
  stop: function(){
    core.simpleAction('read_stop');
    clearInterval(readModule.i);
    clearTimeout(readModule.o);
    readModule.toggleControls(false);
    $('#validate-button').text('Validate');
    $('#read-button').text('Read');
  },
  clear: function(){
    core.simpleAction('read_clear');

    $('.read-epc-result').html('');
    $('.read-memory-result').html('');
    $('.read-result').attr('class', 'read-result').html('');
    $('#read-memory-values').html('');
    $('#read-memory-validated-values').html('');

    readModule.validatedItems = [];
  },
  mask: function(){
    core.simpleAction('read_mask');
  },
  validate: function(){
    readModule.currentFunction = readModule.validate;
    readModule.action('read_memory', true);
  },
  read: function(){
    readModule.currentFunction = readModule.read;
    readModule.action('read_memory', false);
  },
  action: function(a,v,p){
    if (!v) {
      $('.read-epc-result').html('');
      $('.read-memory-result').html('');
      $('.read-result').attr('class', 'read-result').html('');
      $('#read-memory-values').html('');
    }

    var time = parseInt($('#read-operation-time').val());

    if (time != 0 && !readModule.continueMode){
      setTimeout(readModule.stop, time);
    }

    var offset = v ? 0 : parseInt($('#read-offset').val());
    var length = v ? 6 : parseInt($('#read-length').val());
    var password = p || $('#read-password').val();
    var powerGain = parseInt($('#read-power-gain').val());
    var operationTime = parseInt($('#read-operation-time').val());
    var bank = v ? (p ? 3 : 2) : parseInt($('.bank-option:checked').val());

    io.emit(a, { offset: offset, length: length, password: password, powerGain: powerGain, operationTime: operationTime, bank: bank, validate: v });
  },
  showBankDialog: function(){ readModule.bankDialog.showModal(); },
  closeBankDialog: function(){ readModule.bankDialog.close(); },
  onBankSelected: function(){
    var t = $('.bank-option:checked');
    readModule.selectedBank = t.val();
    readModule.closeBankDialog();
    $('#bank-button').text('Bank: ' + t.prev().html());
  },
  toggleReadMode: function(){
    var t = $('#read-simple-display');
    var checked = t.prop('checked');

    if (checked) {
      $('.read-memory-result').show();
      $('.memory-values').hide();
    } else {
      $('.read-memory-result').hide();
      $('.memory-values').show();
    }
  }
};
